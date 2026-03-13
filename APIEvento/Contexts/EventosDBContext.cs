using System;
using System.Collections.Generic;
using APIEvento.Domains;
using Microsoft.EntityFrameworkCore;

namespace APIEvento.Contexts;

public partial class EventosDBContext : DbContext
{
    public EventosDBContext()
    {
    }

    public EventosDBContext(DbContextOptions<EventosDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Especialidade> Especialidade { get; set; }

    public virtual DbSet<Evento> Evento { get; set; }

    public virtual DbSet<Inscricao> Inscricao { get; set; }

    public virtual DbSet<Log_AlteracaoProduto> Log_AlteracaoProduto { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EventosDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.EspecialidadeID).HasName("PK__Especial__8829C3594AF9A5B7");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.EventoId).HasName("PK__Evento__1EEB5921DC112C3B");

            entity.Property(e => e.Local)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inscricao>(entity =>
        {
            entity.HasKey(e => e.InscricaoId).HasName("PK__Inscrica__CD089DAE6E2BF2DC");

            entity.HasIndex(e => new { e.EventoId, e.UsuarioId }, "UQ_Inscricao").IsUnique();

            entity.HasOne(d => d.Evento).WithMany(p => p.Inscricao)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscricao__Event__5629CD9C");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Inscricao)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscricao__Usuar__571DF1D5");
        });

        modelBuilder.Entity<Log_AlteracaoProduto>(entity =>
        {
            entity.HasKey(e => e.Log_AlteracaoEventoID).HasName("PK__Log_Alte__33AABE7F2FB40A14");

            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.DataAnterior).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Evento).WithMany(p => p.Log_AlteracaoProduto)
                .HasForeignKey(d => d.EventoID)
                .HasConstraintName("FK__Log_Alter__Event__59FA5E80");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioID).HasName("PK__TipoUsua__7F22C702E6567A73");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8CA5AD80F");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534862C0CA4").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);

            entity.HasOne(d => d.Especialidade).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.EspecialidadeID)
                .HasConstraintName("FK__Usuario__Especia__5070F446");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoUsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__TipoUsu__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
