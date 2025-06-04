using EleksInternshipProj.Domain.Models;
using Microsoft.EntityFrameworkCore;
using TaskModel = EleksInternshipProj.Domain.Models.TaskModel;
using TaskStatus = EleksInternshipProj.Domain.Models.TaskStatus;

namespace EleksInternshipProj.Infrastructure.Data
{
    public class NavchaykoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserSpace> UserSpaces { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventMarker> EventMarkers { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<EventDay> EventDays { get; set; }
        public DbSet<SoloEvent> SoloEvents { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }

        public NavchaykoDbContext(DbContextOptions<NavchaykoDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
              if (!optionsBuilder.IsConfigured)
              {
                    optionsBuilder.UseNpgsql("Host=localhost:5432;Database=navchayko;Username=postgres;Password=12345678");
              }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>(entity =>
            {
                  entity.ToTable("user");
                  entity.HasKey(u => u.Id);
                  entity.HasIndex(u => u.Email).IsUnique();
                  entity.Property(u => u.UserName).IsRequired();
                  entity.Property(u => u.FirstName).IsRequired();
                  entity.Property(u => u.LastName).IsRequired();
                  entity.Property(u => u.Email).IsRequired();
                  entity.Property(u => u.PasswordHash).IsRequired();
                  entity.Property(u => u.PasswordSalt).IsRequired();
            });

            modelBuilder.Entity<Space>(entity =>
            {
                  entity.ToTable("space");
                  entity.HasKey(s => s.Id);
                  entity.Property(s => s.Name).IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                  entity.ToTable("role");
                  entity.HasKey(r => r.Id);
                  entity.Property(r => r.Name).IsRequired();
                  entity.HasIndex(r => r.Name).IsUnique();
            });

            modelBuilder.Entity<UserSpace>(entity =>
            {
                  entity.ToTable("user_space");
                  entity.HasKey(us => us.Id);
                  entity.HasIndex(us => new { us.SpaceId, us.UserId }).IsUnique();
                  entity.HasOne(us => us.User)
                        .WithMany(u => u.UserSpaces)
                        .HasForeignKey(us => us.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                  entity.HasOne(us => us.Space)
                        .WithMany(s => s.UserSpaces)
                        .HasForeignKey(us => us.SpaceId)
                        .OnDelete(DeleteBehavior.Cascade);
                  entity.HasOne(us => us.Role)
                        .WithMany(r => r.UserSpaces)
                        .HasForeignKey(us => us.RoleId);
            });

            modelBuilder.Entity<Marker>(entity =>
            {
                  entity.ToTable("marker");
                  entity.HasKey(m => m.Id);
                  entity.Property(m => m.Name).IsRequired();
                  entity.Property(m => m.Type).IsRequired();
                  entity.HasOne(m => m.Space)
                        .WithMany(s => s.Markers)
                        .HasForeignKey(m => m.SpaceId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                  entity.ToTable("event");
                  entity.HasKey(e => e.Id);
                  entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<EventMarker>(entity =>
            {
                  entity.ToTable("event_marker");
                  entity.HasKey(em => em.Id);
                  entity.HasOne(em => em.Event)
                        .WithMany(e => e.EventMarkers)
                        .HasForeignKey(em => em.EventId)
                        .OnDelete(DeleteBehavior.Cascade);
                  entity.HasOne(em => em.Marker)
                        .WithMany(m => m.EventMarkers)
                        .HasForeignKey(em => em.MarkerId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Timetable>(entity =>
            {
                  entity.ToTable("timetable");
                  entity.HasKey(t => t.Id);
                  entity.HasOne(t => t.Space)
                        .WithOne(s => s.Timetable)
                        .HasForeignKey<Timetable>(t => t.SpaceId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Day>(entity =>
            {
                  entity.ToTable("day");
                  entity.HasKey(d => d.Id);
                  entity.Property(d => d.DayName).IsRequired();
                  entity.HasOne(d => d.Timetable)
                        .WithMany(t => t.Days)
                        .HasForeignKey(d => d.TimetableId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EventDay>(entity =>
            {
                  entity.ToTable("event_day");
                  entity.HasKey(etd => etd.Id);
                  entity.Property(etd => etd.StartTime).IsRequired();
                  entity.Property(etd => etd.EndTime).IsRequired();
                  entity.HasOne(etd => etd.Event)
                        .WithMany(e => e.EventDays)
                        .HasForeignKey(etd => etd.EventId)
                        .OnDelete(DeleteBehavior.Cascade);
                  entity.HasOne(ed => ed.Day)
                        .WithMany(td => td.EventDays)
                        .HasForeignKey(etd => etd.DayId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SoloEvent>(entity =>
            {
                  entity.ToTable("solo_event");
                  entity.HasKey(se => se.Id);
                  entity.Property(se => se.EventTime).IsRequired();
                  entity.HasOne(se => se.Event)
                        .WithMany(e => e.SoloEvents)
                        .HasForeignKey(se => se.EventId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                  entity.ToTable("note");
                  entity.HasKey(n => n.Id);
                  entity.Property(n => n.Title).IsRequired();
                  entity.Property(n => n.Content).IsRequired();
                  entity.HasOne(n => n.Event)
                        .WithMany(e => e.Notes)
                        .HasForeignKey(n => n.EventId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                  entity.ToTable("taskstatus");
                  entity.HasKey(ts => ts.Id);
                  entity.Property(ts => ts.Name).IsRequired();
            });

            modelBuilder.Entity<TaskModel>(entity =>
            {
                  entity.ToTable("task");
                  entity.HasKey(t => t.Id);
                  entity.Property(t => t.Name).IsRequired();
                  entity.Property(t => t.EventTime).IsRequired();
                  entity.Property(t => t.IsDeadline).IsRequired();
                  entity.Property(t => t.Description).IsRequired();
                  entity.HasOne(t => t.Event)
                        .WithMany(e => e.Tasks)
                        .HasForeignKey(t => t.EventId)
                        .OnDelete(DeleteBehavior.Cascade);
                  entity.HasOne(t => t.Status)
                        .WithMany(s => s.Tasks)
                        .HasForeignKey(t => t.StatusId);
            });
        }
    }
}
