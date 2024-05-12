using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Redis.Models;

public partial class RevisionContext : DbContext
{
    public RevisionContext()
    {
    }

    public RevisionContext(DbContextOptions<RevisionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<O> Os { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Revision;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<O>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OS");

            entity.Property(e => e.Name).HasDefaultValue(1);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
