using System;
using System.Collections.Generic;
using BiteArcade.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BiteArcade.WebAPI.BdContextJogosContext;

public partial class JogosContext : DbContext
{
    public JogosContext()
    {
    }

    public JogosContext(DbContextOptions<JogosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Jogo> Jogos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Jogos;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F834988BA087B04");

            entity.Property(e => e.IdGenero).ValueGeneratedNever();
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.IdJogo).HasName("PK__Jogo__69E085134CAFD4DA");

            entity.Property(e => e.IdJogo).ValueGeneratedNever();

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Jogos).HasConstraintName("FK_Jogo_Genero");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97D0F3AA54");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
