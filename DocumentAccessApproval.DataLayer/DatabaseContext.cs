using DocumentAccessApproval.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Document = DocumentAccessApproval.Domain.Models.Document;

namespace DocumentAccessApproval.DataLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Domain.Models.Document> Documents { get; set; }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<AccessRequest> AccessRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
            options.UseInMemoryDatabase(databaseName: "DemoDb");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessRequest>()
                .HasOne(ar => ar.Decision).WithOne(d => d.Request).HasForeignKey<Decision>(d => d.AccessRequestId);

            modelBuilder.Entity<AccessRequest>()
                .HasOne(ar => ar.Document).WithMany(d => d.Requests).HasForeignKey(ar => ar.DocumentId);

            modelBuilder.Entity<AccessRequest>()
                .HasOne(ar => ar.User).WithMany(u => u.Requests).HasForeignKey(ar => ar.UserId);

            modelBuilder.Entity<Decision>()
                .HasOne(d => d.MadeByUser).WithMany(u => u.Decisions).HasForeignKey(d => d.MadeByUserId);


            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), Username = "commonUser", Password="password", UserType = UserType.Common });

            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), Username = "approverUser", Password="password", UserType = UserType.Approver });

            modelBuilder.Entity<Document>().HasData(
                new Document { Id = Guid.NewGuid(), Name = "TestFile.pdf", Content = System.IO.File.ReadAllBytes("ExampleDocument/sample-1.pdf") });
        }
    }
}
