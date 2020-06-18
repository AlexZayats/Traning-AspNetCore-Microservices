﻿using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Basket.Domain.Entities;

namespace Traning.AspNetCore.Microservices.Basket.Application
{
    public interface IBasketDbContext
    {
        DbSet<CustomerBasket> CustomerBaskets { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
