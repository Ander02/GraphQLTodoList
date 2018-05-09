﻿// <auto-generated />
using GraphQLTodoList.Infraestructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace GraphQLTodoList.Migrations
{
    [DbContext(typeof(Db))]
    partial class DbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GraphQLTodoList.Domain.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CompletedAt");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("GraphQLTodoList.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GraphQLTodoList.Domain.Task", b =>
                {
                    b.HasOne("GraphQLTodoList.Domain.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
