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

        public virtual DbSet<T_ChartData> T_ChartData { get; set; }
        public virtual DbSet<T_DataPoint> T_DataPoint { get; set; }
        public virtual DbSet<T_DataValue> T_DataValue { get; set; }
        public virtual DbSet<T_Plc> T_Plc { get; set; }
        public virtual DbSet<T_PlcBrand> T_PlcBrand { get; set; }
        public virtual DbSet<T_SavedChart> T_SavedChart { get; set; }
        public virtual DbSet<T_User> T_User { get; set; }
        public virtual DbSet<T_UserLevel> T_UserLevel { get; set; }
        public virtual DbSet<T_UserPlc> T_UserPlc { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_DataPoint>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_DataPoint>()
                .HasMany(e => e.T_DataValue)
                .WithRequired(e => e.T_DataPoint1)
                .HasForeignKey(e => e.T_DataPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_DataValue>()
                .HasMany(e => e.T_ChartData)
                .WithRequired(e => e.T_DataValue)
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
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<T_User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<T_User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<T_User>()
                .HasMany(e => e.T_SavedChart)
                .WithRequired(e => e.T_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_User>()
                .HasMany(e => e.T_UserPlc)
                .WithRequired(e => e.T_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_UserLevel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<T_UserLevel>()
                .HasMany(e => e.T_User)
                .WithRequired(e => e.T_UserLevel1)
                .HasForeignKey(e => e.T_UserLevel)
                .WillCascadeOnDelete(false);
        }
    }
}
