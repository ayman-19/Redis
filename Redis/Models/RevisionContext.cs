using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Redis.Models;

public partial class RevisionContext : DbContext
{
    public RevisionContext() { }

    public RevisionContext(DbContextOptions<RevisionContext> options)
        : base(options) { }

    public virtual DbSet<O> Os { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    //=> optionsBuilder.UseSqlServer("Server=.;Database=Revision;Trusted_Connection=True;TrustServerCertificate=True;");
    //    =>
    //    optionsBuilder.UseSqlServer(
    //        "Server=sqlserver,1433;Database=Revision;User Id=sa;Password=Passw0rd;"
    //    );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<O>(entity =>
        {
            entity.HasNoKey().ToTable("OS");

            entity.Property(e => e.Name).HasDefaultValue(1);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
