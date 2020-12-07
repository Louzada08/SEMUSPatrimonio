﻿// <auto-generated />
using System;
using CBP.ResponsavelPatrimonial.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CBP.ResponsavelPatrimonial.API.Migrations
{
    [DbContext(typeof(ResponsavelContext))]
    partial class ResponsavelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CBP.ResponsavelPatrimonial.API.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("char(10)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("char(02)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("char(10)");

                    b.Property<Guid>("ResponsavelId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ResponsavelId")
                        .IsUnique();

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("CBP.ResponsavelPatrimonial.API.Models.Responsavel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Excluido")
                        .HasColumnType("boolean");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Responsavel");
                });

            modelBuilder.Entity("CBP.ResponsavelPatrimonial.API.Models.Endereco", b =>
                {
                    b.HasOne("CBP.ResponsavelPatrimonial.API.Models.Responsavel", "Responsavel")
                        .WithOne("Endereco")
                        .HasForeignKey("CBP.ResponsavelPatrimonial.API.Models.Endereco", "ResponsavelId")
                        .IsRequired();
                });

            modelBuilder.Entity("CBP.ResponsavelPatrimonial.API.Models.Responsavel", b =>
                {
                    b.OwnsOne("CBP.Core.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ResponsavelId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasColumnName("Email")
                                .HasColumnType("varchar(254)");

                            b1.HasKey("ResponsavelId");

                            b1.ToTable("Responsavel");

                            b1.WithOwner()
                                .HasForeignKey("ResponsavelId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
