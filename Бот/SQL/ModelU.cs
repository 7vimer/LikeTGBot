using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Bot
{
	public partial class ModelU : DbContext
	{
		private static ModelU _context;

		public static ModelU GetContext()
		{
			if (_context == null) _context = new ModelU();
			return _context;
		}
		public ModelU()
			: base("name=ModelU")
		{
		}

		public virtual DbSet<AnketsParams> AnketsParams { get; set; }
		public virtual DbSet<Reports> Reports { get; set; }
		public virtual DbSet<Section> Section { get; set; }
		public virtual DbSet<SectionAnkets> SectionAnkets { get; set; }
		public virtual DbSet<SectionParams> SectionParams { get; set; }
		public virtual DbSet<Users> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Section>()
				.HasMany(e => e.SectionParams)
				.WithRequired(e => e.Section)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SectionAnkets>()
				.HasMany(e => e.AnketsParams)
				.WithOptional(e => e.SectionAnkets)
				.HasForeignKey(e => e.AnketaId);

			modelBuilder.Entity<Users>()
				.Property(e => e.Sex)
				.IsFixedLength();

			modelBuilder.Entity<Users>()
				.Property(e => e.TargetSex)
				.IsFixedLength();

			modelBuilder.Entity<Users>()
				.HasMany(e => e.Reports)
				.WithOptional(e => e.Users)
				.HasForeignKey(e => e.Sender);

			modelBuilder.Entity<Users>()
				.HasMany(e => e.Reports1)
				.WithOptional(e => e.Users1)
				.HasForeignKey(e => e.Target);

			modelBuilder.Entity<Users>()
				.HasMany(e => e.Section)
				.WithOptional(e => e.Users)
				.HasForeignKey(e => e.Creator);

			modelBuilder.Entity<Users>()
				.HasMany(e => e.SectionAnkets)
				.WithOptional(e => e.Users)
				.HasForeignKey(e => e.Creator);
		}
	}
}
