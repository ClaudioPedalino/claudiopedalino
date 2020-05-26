﻿using Microsoft.EntityFrameworkCore;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Data.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Operation> Operations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Change> Change { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Bill> Bills { get; set; }
        //public DbSet<Coin> Coins { get; set; }
    }
}
