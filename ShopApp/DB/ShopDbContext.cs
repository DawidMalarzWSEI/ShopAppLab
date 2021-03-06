using Microsoft.EntityFrameworkCore;


namespace ShopApp.DB
{
    public partial class ShopDbContext : DbContext
    {
        public ShopDbContext()
        {
        }

        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Month> Months { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PermissionState> PermissionStates { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Salary> Salaries { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskState> TaskStates { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database=ShopDb; trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.ImagePath).IsUnicode(false);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.Surename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Position");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Shop");
            });

            modelBuilder.Entity<Month>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MonthName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.PermissionEndDate).HasColumnType("date");

                entity.Property(e => e.PermissionExplanation).IsUnicode(false);

                entity.Property(e => e.PermissionStartDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Employee");

                entity.HasOne(d => d.PermissionStateNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.PermissionState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_PermissionState");
            });

            modelBuilder.Entity<PermissionState>(entity =>
            {
                entity.ToTable("PermissionState");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PositionName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Position_Shop");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salary");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.MonthId).HasColumnName("MonthID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salary_Employee");

                entity.HasOne(d => d.Month)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.MonthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salary_Months");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.TaskDeliveryDate).HasColumnType("date");

                entity.Property(e => e.TaskDescription).IsUnicode(false);

                entity.Property(e => e.TaskStartDate).HasColumnType("date");

                entity.Property(e => e.TaskTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Task_Employee");

                entity.HasOne(d => d.TaskStateNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_TaskState");
            });

            modelBuilder.Entity<TaskState>(entity =>
            {
                entity.ToTable("TaskState");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StateName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
