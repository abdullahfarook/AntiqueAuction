using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AntiqueAuction.Core.Models;
using AntiqueAuction.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace AntiqueAuction.Infrastructure
{
    // Framework dependent Implementation of Unit of work
    public class AntiqueAuctionDbContext : DbContext
    {
        private readonly IEventBus _eventBus;
        public AntiqueAuctionDbContext(){}

        public AntiqueAuctionDbContext(DbContextOptions<AntiqueAuctionDbContext> options, IEventBus eventBus) : base(options)
        {
            _eventBus = eventBus;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<BidHistory> ItemsHistory { get; set; } 
        public DbSet<AutoBid> AutoBids { get; set; } 
        public DbSet<BidHistory> ItemHistories { get; set; } 
        public DbSet<User> User { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(Entity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(Entity.Id))
                    .ValueGeneratedNever();
            }
            // This Converter will perform the conversion to and from Json to the desired type
            modelBuilder.Entity<User>().Property(e => e.Address).HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { IgnoreNullValues = true}),
                v => JsonSerializer.Deserialize<Address>(v, new JsonSerializerOptions { IgnoreNullValues = true}));
            // This Converter will perform the conversion to and from Json to the desired type
            modelBuilder.Entity<AutoBid>()
                .HasOne(s => s.User)
                .WithMany(ta => ta.AutoBids)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BidHistory>()
                .HasOne(s => s.User)
                .WithMany(ta => ta.ItemHistories)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            var asyncDomainEvents = new List<IEvent>();
            var entries = ChangeTracker.Entries().ToList();
            foreach (var entry in entries)
            {
                if (entry.Entity is Entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
                        entry.Property("UpdatedOn").CurrentValue = DateTime.UtcNow;
                    }

                    if (entry.State == EntityState.Modified)
                        entry.Property("UpdatedOn").CurrentValue = DateTime.UtcNow;
                }

                if (!(entry.Entity is AggregateRoot entity)) continue;

                if (!entity.DomainEvents().Any()) continue;

                asyncDomainEvents.AddRange(entity.DomainEvents());
                entity.ClearDomainEvents();
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            await _eventBus.Publish(asyncDomainEvents, cancellationToken);
            return result;
        }
    }
}
