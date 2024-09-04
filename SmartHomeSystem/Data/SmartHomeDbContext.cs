using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.Models;

namespace SmartHomeSystem.Data
{
    public class SmartHomeDbContext : IdentityDbContext<Characters>
    {
        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options) : base(options)
        {
        }

        public DbSet<AccessControl> AccessControls { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<ActionSeverity> ActionSeverities { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AutomationRule> AutomationRules { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<EnergyUsage> EnergyUsages { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<SceneAction> SceneActions { get; set; }
        public DbSet<SecuritySystem> SecuritySystems { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<AccessControl>()
                .HasOne(ac => ac.Admin)
                .WithMany(a => a.AllowedAccessControls)
                .HasForeignKey(ac => ac.AdminId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<AccessControl>()
                .HasOne(ac => ac.House)
                .WithMany(h => h.AccessControls)
                .HasForeignKey(ac => ac.HouseId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<AccessControl>()
                .HasOne(ac => ac.AccessLevel)
                .WithMany(al => al.AccessControl)
                .HasForeignKey(ac => ac.AccessLevelId);

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Admin)
                .WithMany(ad => ad.ManagedAlerts)
                .HasForeignKey(a => a.AdminId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Provider)
                .WithMany(p => p.ManagedAlerts)
                .HasForeignKey(a => a.ProviderId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.ActionSeverity)
                .WithMany(a => a.Alerts)
                .HasForeignKey(a => a.ActionSeverityId);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.DeviceType)
                .WithMany(dt => dt.Devices)
                .HasForeignKey(d => d.DeviceTypeId);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Room)
                .WithMany(r => r.Devices)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Admin)
                .WithMany(ad => ad.ManagedDevices)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<EnergyUsage>()
                .HasOne(eu => eu.Device)
                .WithMany(d => d.EnergyUsages)
                .HasForeignKey(eu => eu.DeviceId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<EnergyUsage>()
                .HasOne(eu => eu.Admin)
                .WithMany(a => a.ManagedEnergyUsages)
                .HasForeignKey(eu => eu.AdminId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Notification>()
                .HasOne(o => o.Admin)
                .WithMany(s => s.Notification)
                .HasForeignKey(f => f.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<House>()
                .HasOne(h => h.Admin)
                .WithMany(a => a.ManagedHouses)
                .HasForeignKey(h => h.AdminId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<House>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.House)
                .HasForeignKey(r => r.HouseId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Provider)
                .WithMany(p => p.ManagedAlerts)
                .HasForeignKey(a => a.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<House>()
    .HasOne(h => h.Provider)
    .WithMany(p => p.ManagedHouses)
    .HasForeignKey(h => h.ProviderId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LogEntry>()
                .HasOne(le => le.Device)
                .WithMany(d => d.LogEntries)
                .HasForeignKey(le => le.DeviceId);

            modelBuilder.Entity<LogEntry>()
                .HasOne(le => le.ActionType)
                .WithMany(at => at.LogEntries)
                .HasForeignKey(le => le.ActionTypeId);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.House)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HouseId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Room)
                .HasForeignKey(r => r.RoomTypeId);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Admin)
                .WithMany(a => a.Rooms)
                .HasForeignKey(r => r.AdminId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Scene>()
                .HasOne(s => s.Admin)
                .WithMany(a => a.AccessibleScenes)
                .HasForeignKey(s => s.AdminId);

            modelBuilder.Entity<SceneAction>()
                .HasOne(sa => sa.Scene)
                .WithMany(s => s.SceneActions)
                .HasForeignKey(sa => sa.SceneId);

            modelBuilder.Entity<SceneAction>()
                .HasOne(sa => sa.ActionType)
                .WithMany(at => at.SceneActions)
                .HasForeignKey(sa => sa.ActionTypeId);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.SubscriptionPlan)
                .WithMany(sp => sp.UserSubscriptions)
                .HasForeignKey(us => us.SubscriptionPlanId);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Admin)
                .WithMany(a => a.UserSubscriptions)
                .HasForeignKey(us => us.AdminId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Provider>()
                .HasOne(p => p.User)
                .WithOne(u => u.Provider)
                .HasForeignKey<Provider>(p => p.CharactersId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            // Decimal precision configuration
            modelBuilder.Entity<EnergyUsage>()
               .Property(e => e.Cost)
               .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

            modelBuilder.Entity<SubscriptionPlan>()
                .Property(s => s.MonthlyCost)
                .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

            seedRoles(modelBuilder, "Admin", "update", "read", "delete", "create");
            seedRoles(modelBuilder, "Guest", "read");
            seedRoles(modelBuilder, "Provider", "update", "read", "delete", "create");
        }
        private void seedRoles(ModelBuilder modelBuilder, string roleName, params string[] permission)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            // add claims for the users
            var claims = permission.Select(permission => new IdentityRoleClaim<string>
            {
                Id = Guid.NewGuid().GetHashCode(),
                // Unique identifier
                RoleId = role.Id,
                ClaimType = "permission",
                ClaimValue = permission
            });
            // Seed the role and its claims
            modelBuilder.Entity<IdentityRole>().HasData(role);
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(claims);
        }
    }
}
