using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Solucion_Comercio.Models;

public partial class BdcomercioContext : DbContext
{
    public BdcomercioContext()
    {
    }

    public BdcomercioContext(DbContextOptions<BdcomercioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBitacora> TbBitacoras { get; set; }

    public virtual DbSet<TbCompra> TbCompras { get; set; }

    public virtual DbSet<TbEstado> TbEstados { get; set; }

    public virtual DbSet<TbFactura> TbFacturas { get; set; }

    public virtual DbSet<TbInventario> TbInventarios { get; set; }

    public virtual DbSet<TbPendiente> TbPendientes { get; set; }

    public virtual DbSet<TbProducto> TbProductos { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbBitacora>(entity =>
        {
            entity.HasKey(e => e.IdBitacora);

            entity.ToTable("TbBitacora");

            entity.Property(e => e.IdBitacora).HasColumnName("idBitacora");
            entity.Property(e => e.Entrada)
                .HasColumnType("datetime")
                .HasColumnName("entrada");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Salida)
                .HasColumnType("datetime")
                .HasColumnName("salida");
        });

        modelBuilder.Entity<TbCompra>(entity =>
        {
            entity.HasKey(e => e.IdCompra);

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.CantidadCompra).HasColumnName("cantidadCompra");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("date")
                .HasColumnName("fechaCompra");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("nombreUsuario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TbCompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbCompras_TbProductos1");
        });

        modelBuilder.Entity<TbEstado>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.Property(e => e.IdEstado).HasColumnName("id_Estado");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .HasColumnName("nombreEstado");
        });

        modelBuilder.Entity<TbFactura>(entity =>
        {
            entity.HasKey(e => e.IdFactura);

            entity.Property(e => e.IdFactura).HasColumnName("id_Factura");
            entity.Property(e => e.FechaFactura)
                .HasColumnType("date")
                .HasColumnName("fechaFactura");
            entity.Property(e => e.MontoColones).HasColumnName("montoColones");
            entity.Property(e => e.MontoDolares).HasColumnName("montoDolares");
            entity.Property(e => e.MontoTarjeta).HasColumnName("montoTarjeta");
            entity.Property(e => e.MontoTotal).HasColumnName("montoTotal");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(50)
                .HasColumnName("nombreCliente");
            entity.Property(e => e.NombreUsuario).HasColumnName("nombreUsuario");

            entity.HasOne(d => d.NombreUsuarioNavigation).WithMany(p => p.TbFacturas)
                .HasForeignKey(d => d.NombreUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbFacturas_TbUsuarios");
        });

        modelBuilder.Entity<TbInventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario);

            entity.ToTable("TbInventario");

            entity.Property(e => e.IdInventario).HasColumnName("idInventario");
            entity.Property(e => e.Existencia).HasColumnName("existencia");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TbInventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbInventario_TbProductos");
        });

        modelBuilder.Entity<TbPendiente>(entity =>
        {
            entity.HasKey(e => e.IdPendiente);

            entity.Property(e => e.IdPendiente)
                .ValueGeneratedNever()
                .HasColumnName("idPendiente");
            entity.Property(e => e.IdCompra)
                .ValueGeneratedOnAdd()
                .HasColumnName("idCompra");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.TbPendientes)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPendientes_TbCompras");
        });

        modelBuilder.Entity<TbProducto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.CantidadProducto).HasColumnName("cantidadProducto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .HasColumnName("nombreProducto");
            entity.Property(e => e.PrecioProducto).HasColumnName("precioProducto");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .HasColumnName("nombreRol");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.ApellidoIiusuario)
                .HasMaxLength(50)
                .HasColumnName("apellidoIIUsuario");
            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(50)
                .HasColumnName("apellidoUsuario");
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(50)
                .HasColumnName("correoUsuario");
            entity.Property(e => e.EstadoUsuario).HasColumnName("estadoUsuario");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.RolUsuario).HasColumnName("rolUsuario");
            entity.Property(e => e.TelefonoUsuario).HasColumnName("telefonoUsuario");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");

            entity.HasOne(d => d.EstadoUsuarioNavigation).WithMany(p => p.TbUsuarios)
                .HasForeignKey(d => d.EstadoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUsuarios_TbEstados");

            entity.HasOne(d => d.RolUsuarioNavigation).WithMany(p => p.TbUsuarios)
                .HasForeignKey(d => d.RolUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUsuarios_TbRoles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
