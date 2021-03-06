﻿using FluentValidation;
using Traning.AspNetCore.Microservices.Catalog.Application.Extensions;

namespace Traning.AspNetCore.Microservices.Catalog.Application.CQRS
{
    public class ProductUpdateCommandValidator : AbstractValidator<ProductUpdateCommand>
    {
        public ProductUpdateCommandValidator(ICatalogDbContext context)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ProductId).ProductExists(context);
        }
    }
}
