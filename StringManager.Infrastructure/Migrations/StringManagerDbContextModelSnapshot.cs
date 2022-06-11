﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StringManager.Infrastructure.Persistence;

#nullable disable

namespace StringManager.Infrastructure.Migrations
{
    [DbContext(typeof(StringManagerDbContext))]
    partial class StringManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccessGroupUser", b =>
                {
                    b.Property<Guid>("AccessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccessId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AccessGroupUser");
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.AccessGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("AccessGroup", (string)null);
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.Folder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Folder", (string)null);
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.FolderAccessGroupRight", b =>
                {
                    b.Property<Guid>("FolderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccessGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessRights")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FolderId", "AccessGroupId");

                    b.HasIndex("AccessGroupId");

                    b.ToTable("FolderAccessGroupRight", (string)null);
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("AccessGroupUser", b =>
                {
                    b.HasOne("StringManager.Domain.Objects.Entity.AccessGroup", null)
                        .WithMany()
                        .HasForeignKey("AccessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StringManager.Domain.Objects.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.AccessGroup", b =>
                {
                    b.HasOne("StringManager.Domain.Objects.Entity.AccessGroup", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.OwnsOne("StringManager.Domain.Objects.Value.ObjectName", "Name", b1 =>
                        {
                            b1.Property<Guid>("AccessGroupId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("AccessGroupId");

                            b1.ToTable("AccessGroup");

                            b1.WithOwner()
                                .HasForeignKey("AccessGroupId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.Folder", b =>
                {
                    b.HasOne("StringManager.Domain.Objects.Entity.Folder", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.OwnsOne("StringManager.Domain.Objects.Value.ObjectName", "Name", b1 =>
                        {
                            b1.Property<Guid>("FolderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("FolderId");

                            b1.ToTable("Folder");

                            b1.WithOwner()
                                .HasForeignKey("FolderId");
                        });

                    b.OwnsOne("StringManager.Domain.Objects.Value.FolderDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("FolderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("FolderId");

                            b1.ToTable("Folder");

                            b1.WithOwner()
                                .HasForeignKey("FolderId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.FolderAccessGroupRight", b =>
                {
                    b.HasOne("StringManager.Domain.Objects.Entity.AccessGroup", "AccessGroup")
                        .WithMany("AccessibleFolders")
                        .HasForeignKey("AccessGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StringManager.Domain.Objects.Entity.Folder", "Folder")
                        .WithMany("AccessGroupRights")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessGroup");

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.User", b =>
                {
                    b.OwnsOne("StringManager.Domain.Objects.Value.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("StringManager.Domain.Objects.Value.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("HashedValue")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("HashedPassword");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.AccessGroup", b =>
                {
                    b.Navigation("AccessibleFolders");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("StringManager.Domain.Objects.Entity.Folder", b =>
                {
                    b.Navigation("AccessGroupRights");

                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
