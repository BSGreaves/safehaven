using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SafeHaven.MobileAppService.Data;

namespace SafeHaven.MobileAppService.Migrations
{
    [DbContext(typeof(SafeHavenContext))]
    partial class SafeHavenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("SafeHaven.MobileAppService.Models.AccessRight", b =>
                {
                    b.Property<int>("AccessRightID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessorID");

                    b.Property<int>("GrantorID");

                    b.HasKey("AccessRightID");

                    b.HasIndex("AccessorID");

                    b.HasIndex("GrantorID");

                    b.ToTable("AccessRight");
                });

            modelBuilder.Entity("SafeHaven.MobileAppService.Models.Document", b =>
                {
                    b.Property<int>("DocumentID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                    b.Property<int>("DocumentTypeID");

                    b.Property<string>("Notes");

                    b.Property<string>("PhysicalLocation");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UserID");

                    b.HasKey("DocumentID");

                    b.HasIndex("DocumentTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("SafeHaven.MobileAppService.Models.DocumentImage", b =>
                {
                    b.Property<int>("DocumentImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                    b.Property<int>("DocumentID");

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<int>("PageNumber");

                    b.HasKey("DocumentImageID");

                    b.HasIndex("DocumentID");

                    b.ToTable("DocumentImage");
                });

            modelBuilder.Entity("SafeHaven.MobileAppService.Models.DocumentType", b =>
                {
                    b.Property<int>("DocumentTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("DocumentTypeID");

                    b.ToTable("DocumentType");
                });

            modelBuilder.Entity("SafeHaven.MobileAppService.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<int>("ZIP");

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SafeHaven.MobileAppService.Models.AccessRight", b =>
                {
                    b.HasOne("SafeHaven.MobileAppService.Models.User", "Accessor")
                        .WithMany("AccessAllowed")
                        .HasForeignKey("AccessorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SafeHaven.MobileAppService.Models.User", "Grantor")
                        .WithMany("AccessGranted")
                        .HasForeignKey("GrantorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SafeHaven.MobileAppService.Models.Document", b =>
                {
                    b.HasOne("SafeHaven.MobileAppService.Models.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SafeHaven.MobileAppService.Models.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SafeHaven.MobileAppService.Models.DocumentImage", b =>
                {
                    b.HasOne("SafeHaven.MobileAppService.Models.Document", "Document")
                        .WithMany("DocumentImages")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
