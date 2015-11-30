namespace Trend.Core.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TrendData : DbContext
    {
        public TrendData()
            : base("name=TrendData")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<T_ChartData> T_ChartData { get; set; }
        public virtual DbSet<T_DataPoint> T_DataPoint { get; set; }
        public virtual DbSet<T_DataValue> T_DataValue { get; set; }
        public virtual DbSet<T_Plc> T_Plc { get; set; }
        public virtual DbSet<T_PlcBrand> T_PlcBrand { get; set; }
        public virtual DbSet<T_SavedChart> T_SavedChart { get; set; }
        public virtual DbSet<T_UserPlc> T_UserPlc { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.T_SavedChart)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.T_UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.T_UserPlc)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.T_UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_DataPoint>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_DataPoint>()
                .HasMany(e => e.T_ChartData)
                .WithRequired(e => e.T_DataPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_DataPoint>()
                .HasMany(e => e.T_DataValue)
                .WithRequired(e => e.T_DataPoint1)
                .HasForeignKey(e => e.T_DataPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Plc>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_Plc>()
                .Property(e => e.IpAddress)
                .IsUnicode(false);

            modelBuilder.Entity<T_Plc>()
                .HasMany(e => e.T_DataPoint)
                .WithRequired(e => e.T_Plc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Plc>()
                .HasMany(e => e.T_UserPlc)
                .WithRequired(e => e.T_Plc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_PlcBrand>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_PlcBrand>()
                .HasMany(e => e.T_Plc)
                .WithRequired(e => e.T_PlcBrand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_SavedChart>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_SavedChart>()
                .HasMany(e => e.T_ChartData)
                .WithRequired(e => e.T_SavedChart)
                .WillCascadeOnDelete(true);
        }
    }
}
