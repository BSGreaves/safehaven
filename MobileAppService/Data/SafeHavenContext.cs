using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SafeHaven.MobileAppService.Models;

namespace SafeHaven.MobileAppService.Data
{
	public class SafeHavenContext : DbContext
	{
		public SafeHavenContext(DbContextOptions<SafeHavenContext> options)
			: base(options)
		{ }

		public DbSet<User> User { get; set; }
		public DbSet<AccessRight> AccessRight { get; set; }
		public DbSet<Document> Document { get; set; }
		public DbSet<DocumentType> DocumentType { get; set; }
		public DbSet<Image> Image { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Document>()
				.Property(b => b.DateCreated)
				.HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

			modelBuilder.Entity<Image>()
				.Property(b => b.DateCreated)
				.HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
		}
	}
}