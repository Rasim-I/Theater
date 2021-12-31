using System;
using System.Data.Common;
using System.Data.Entity;
using TheaterDAL.Entities;

//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;


namespace TheaterDAL
{
    public class TheaterContext : DbContext
    {

        public TheaterContext() : base("DbConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public virtual DbSet<TicketEntity> Tickets { get; set; }
        //public virtual DbSet<VipTicketEntity> VipTickets { get; set; }
        //public virtual DbSet<CommonTicketEntity> CommonTickets { get; set; }
        //public virtual DbSet<BudgetTicketEntity> BudgetTickets { get; set; }
        public virtual DbSet<AuthorEntity> Authors { get; set; }
        public virtual DbSet<ShowEntity> Shows { get; set; }
        public virtual DbSet<PlaceEntity> Places { get; set; }


        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ShowEntity>()
                .HasMany<TicketEntity>(s => s.TicketList)
                .WithOptional(t=>t.Show)
                .WillCascadeOnDelete();
            //.HasOptional<ShowEntity>(e => e.Show)
            //.WithMany()
        }


    }
}
