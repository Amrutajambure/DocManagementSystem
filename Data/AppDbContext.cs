﻿using DocManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DocManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Document> Documents { get; set; }
    }
}
