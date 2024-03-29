﻿using AuthenticationService.DataClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Data.DataClasses.Contracts;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Data.Context
{
    public class UserContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor, DbContextOptions<UserContext> options) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<UserEntity> UserEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Entity<UserEntity>().HasKey(u => u.Id);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    var now = DateTime.UtcNow;
                    var user = GetCurrentUserId();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.ModifiedOn = now;
                            trackable.ModifiedBy = user;
                            break;

                        case EntityState.Added:
                            trackable.CreatedOn = now;
                            trackable.CreatedBy = user;
                            trackable.ModifiedOn = now;
                            trackable.ModifiedBy = user;
                            break;
                    }
                }
            }
        }

        private string GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var username = string.Empty;
                if (httpContext.User.Identity is ClaimsIdentity identity)
                {
                    username = httpContext.User.Identity.Name;
                }
                return username;

            }
            return "DEFAULT";
        }

    }
}
