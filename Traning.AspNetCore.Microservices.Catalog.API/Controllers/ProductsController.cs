﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Catalog.Abstractions.Clients;
using Traning.AspNetCore.Microservices.Catalog.Abstractions.Models;
using Traning.AspNetCore.Microservices.Catalog.Application.CQRS;

namespace Traning.AspNetCore.Microservices.Catalog.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase, IProductsClient
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetProductsAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ProductViewDto[]> GetProductsAsync([FromQuery] Guid[] productIds, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ProductsViewQuery { ProductIds = productIds }, cancellationToken);
            if (result == null)
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
            }
            return result;
        }

        [HttpGet("{productId}", Name = nameof(GetProductAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ProductViewDto> GetProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ProductViewQuery { ProductId = productId }, cancellationToken);
            if (result == null)
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
            }
            return result;
        }

        [HttpPost(Name = nameof(CreateProductAsync))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Guid> CreateProductAsync([FromBody] ProductCreateDto model, CancellationToken cancellationToken = default)
        {
            var command = _mapper.Map<ProductCreateCommand>(model);
            var result = await _mediator.Send(command, cancellationToken);
            Response.Headers["Location"] = Url.RouteUrl(nameof(GetProductAsync), new { productId = result });
            Response.StatusCode = StatusCodes.Status201Created;
            return result;
        }

        [HttpPut("{productId}", Name = nameof(UpdateProductAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task UpdateProductAsync(Guid productId, [FromBody] ProductUpdateDto model, CancellationToken cancellationToken = default)
        {
            var command = _mapper.Map<ProductUpdateCommand>(model);
            command.ProductId = productId;
            await _mediator.Send(command, cancellationToken);
            Response.StatusCode = StatusCodes.Status204NoContent;
        }

        [HttpDelete("{productId}", Name = nameof(DeleteProductAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new ProductDeleteCommand { ProductId = productId }, cancellationToken);
            Response.StatusCode = StatusCodes.Status204NoContent;
        }
    }
}
