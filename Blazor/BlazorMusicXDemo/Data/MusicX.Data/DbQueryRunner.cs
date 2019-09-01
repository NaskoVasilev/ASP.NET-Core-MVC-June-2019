﻿namespace MusicX.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MusicX.Data.Common;

    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ApplicationDbContext Context { get; set; }

        public Task RunQueryAsync(string query, params object[] parameters)
        {
            return this.Context.Database.ExecuteSqlRawAsync(query, parameters);
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
