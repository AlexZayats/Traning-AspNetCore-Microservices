﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Traning.AspNetCore.Microservices.Basket.API.Infrastructure;

namespace Traning.AspNetCore.Microservices.Basket.API.Migrations
{
    [DbContext(typeof(BasketDbContext))]
    [Migration("20200617121831_FixBasketProductReleation")]
    partial class FixBasketProductReleation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Traning.AspNetCore.Microservices.Basket.Domain.Entities.CustomerBasket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerBaskets");
                });

            modelBuilder.Entity("Traning.AspNetCore.Microservices.Basket.Domain.Entities.CustomerBasketProduct", b =>
                {
                    b.Property<Guid>("BasketId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BasketId", "ProductId");

                    b.ToTable("CustomerBasketProduct");
                });

            modelBuilder.Entity("Traning.AspNetCore.Microservices.Basket.Domain.Entities.CustomerBasketProduct", b =>
                {
                    b.HasOne("Traning.AspNetCore.Microservices.Basket.Domain.Entities.CustomerBasket", null)
                        .WithMany("Products")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
