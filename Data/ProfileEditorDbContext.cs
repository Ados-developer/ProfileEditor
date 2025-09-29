using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProfileEditor.Models;

namespace ProfileEditor.Data
{
    public class ProfileEditorDbContext : IdentityDbContext
    {
        public ProfileEditorDbContext(DbContextOptions<ProfileEditorDbContext> options) : base(options)
        {
        }
        public DbSet<ProfileModel> Profiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<ProfileModel>(p => p.UserId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
