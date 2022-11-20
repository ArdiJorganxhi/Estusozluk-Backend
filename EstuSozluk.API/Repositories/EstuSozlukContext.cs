using System;
using EstuSozluk.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EstuSozluk.API.Repositories
{
    public class EstuSozlukContext : DbContext
    {
        public EstuSozlukContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<User>()
                .HasKey(e => e.userid);

            modelBuilder.Entity<Entry>()
                .HasKey(e => e.entryid);

            modelBuilder.Entity<Entry>()
                .Property(e => e.writedate).HasDefaultValueSql("getDate()");
            
            modelBuilder.Entity<Entry>()
                .Property(e => e.editdate).HasDefaultValueSql("getDate()");
            
            

          

            modelBuilder.Entity<User>()
            .HasMany(e => e.entries)
            .WithOne(t => t.User);

            modelBuilder.Entity<User>()
            .HasOne(e => e.permissions)
            .WithOne(t => t.user)
            .HasForeignKey<Permissions>(q => q.userroleid)
            .HasConstraintName("FK_UserRole")
            .HasPrincipalKey<User>(z => z.userroleId);

            modelBuilder.Entity<Followships>()
                .HasKey(e => new { e.follower, e.followed });

            modelBuilder.Entity<Followships>()
                .HasOne<User>(e => e.User1)
                .WithMany(t => t.Following)
                .HasForeignKey(et => et.follower);

            modelBuilder.Entity<Followships>()
                .HasOne(e => e.User2)
                .WithMany(t => t.Followed)
                .HasForeignKey(et => et.followed);


            modelBuilder.Entity<LikedEntries>()
                .HasKey(e => new { e.likedentryid, e.userid });

            modelBuilder.Entity<DislikedEntries>()
                .HasKey(e => new { e.dislikedentryid, e.userid });


            modelBuilder.Entity<LikedEntries>()
                .HasOne(e => e.user)
                .WithMany(z => z.LikedEntries)
                .HasForeignKey(ez => ez.userid);

            modelBuilder.Entity<LikedEntries>()
                 .HasOne(e => e.entry)
                 .WithMany(z => z.LikedEntries)
                 .HasForeignKey(ez => ez.likedentryid);


            modelBuilder.Entity<DislikedEntries>()
                .HasOne(e => e.user)
                .WithMany(z => z.DislikedEntries)
                .HasForeignKey(ez => ez.userid);

            modelBuilder.Entity<DislikedEntries>()
                 .HasOne(e => e.entry)
                 .WithMany(z => z.DislikedEntries)
                 .HasForeignKey(ez => ez.dislikedentryid);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Followships> Followships { get; set; }
        public DbSet<LikedEntries> LikedEntries { get; set; }
        public DbSet<DislikedEntries> DislikedEntries { get; set; }

    }
}
