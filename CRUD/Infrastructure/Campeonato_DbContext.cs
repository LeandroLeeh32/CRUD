using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUD.Infrastructure
{
    public partial class Campeonato_DbContext : DbContext
    {
        public Campeonato_DbContext()
        {
        }

        public Campeonato_DbContext(DbContextOptions<Campeonato_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Jogador> Jogadors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost; Database=Cadastro; user id=postgres; password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogador>(entity =>
            {
                entity.HasKey(e => e.Cpf)
                    .HasName("pk_cpf_jogador");

                entity.ToTable("jogador");

                entity.Property(e => e.Cpf)
                    .HasColumnType("character varying")
                    .HasColumnName("cpf");

                entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");

                entity.Property(e => e.Nome)
                    .HasMaxLength(30)
                    .HasColumnName("nome");

                entity.Property(e => e.NomeMae)
                    .HasMaxLength(30)
                    .HasColumnName("nome_mae");

                entity.Property(e => e.Sobrenome)
                    .HasMaxLength(40)
                    .HasColumnName("sobrenome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
