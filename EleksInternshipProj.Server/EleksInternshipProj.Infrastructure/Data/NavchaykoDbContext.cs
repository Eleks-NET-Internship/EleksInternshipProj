using Microsoft.EntityFrameworkCore;

using EleksInternshipProj.Domain.Models;

using Task = EleksInternshipProj.Domain.Models.Task;
using TaskStatus = EleksInternshipProj.Domain.Models.TaskStatus;

namespace EleksInternshipProj.Infrastructure.Data
{
    public class NavchaykoDbContext : DbContext
    {   // Tabs are 4 spaces here, but 6 spaces in OnConfiguring and OnModelCreating??
        public DbSet<User> Users { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserSpace> UserSpaces { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventMarker> EventMarkers { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<TimetableDay> TimetableDays { get; set; }
        public DbSet<EventTimetableDay> EventTimetableDays { get; set; }
        public DbSet<SoloEvent> SoloEvents { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }

        public NavchaykoDbContext(DbContextOptions<NavchaykoDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
              if (!optionsBuilder.IsConfigured)
              {
                    optionsBuilder.UseNpgsql("Host=localhost:5432;Database=Navchayko;Username=postgres;Password=12345678");
              }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>(entity =>
            {
                // Why do we need to specify .IsRequired both here and in User.cs?
                  entity.ToTable("user");
                  entity.HasKey(u => u.Id);
                  entity.HasIndex(u => new { u.Email, u.AuthProvider}).IsUnique();
                  entity.Property(u => u.UserName).IsRequired();
                  entity.Property(u => u.Email).IsRequired();
                  entity.Property(u => u.AuthProvider).IsRequired();
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
                  entity.Property(e => e.StartTime).IsRequired();
                  entity.Property(e => e.EndTime).IsRequired();
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
                  entity.Property(t => t.Name).IsRequired();
                  entity.HasOne(t => t.Space)
                        .WithMany(s => s.Timetables)
                        .HasForeignKey(t => t.SpaceId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Day>(entity =>
            {
                  entity.ToTable("day");
                  entity.HasKey(d => d.Id);
                  entity.Property(d => d.DayName).IsRequired();
            });

            modelBuilder.Entity<TimetableDay>(entity =>
            {
                  entity.ToTable("timetable_day");
                  entity.HasKey(td => td.Id);
                  entity.HasOne(td => td.Timetable)
                        .WithMany(t => t.TimetableDays)
                        .HasForeignKey(td => td.TimetableId)
                        .OnDelete(DeleteBehavior.Cascade);
                  entity.HasOne(td => td.Day)
                        .WithMany(d => d.TimetableDays)
                        .HasForeignKey(td => td.DayId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EventTimetableDay>(entity =>
            {
                  entity.ToTable("event_timetable_day");
                  entity.HasKey(etd => etd.Id);
                  entity.HasOne(etd => etd.Event)
                        .WithMany(e => e.EventTimetableDays)
                        .HasForeignKey(etd => etd.EventId)
                        .OnDelete(DeleteBehavior.Cascade);
                  entity.HasOne(etd => etd.TimetableDay)
                        .WithMany(td => td.EventTimetableDays)
                        .HasForeignKey(etd => etd.TimetableDayId)
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

            modelBuilder.Entity<Task>(entity =>
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
