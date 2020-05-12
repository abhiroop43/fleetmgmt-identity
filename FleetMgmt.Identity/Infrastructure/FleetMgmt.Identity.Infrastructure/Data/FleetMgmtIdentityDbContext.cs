using System;
using FleetMgmt.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Infrastructure.Data
{
    public class FleetMgmtIdentityDbContext : DbContext
    {
        public FleetMgmtIdentityDbContext(DbContextOptions<FleetMgmtIdentityDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<IM_COMPANY> IM_COMPANY { get; set; }
        public virtual DbSet<IM_OU> IM_OU { get; set; }
        public virtual DbSet<IM_GROUPS> IM_GROUPS { get; set; }
        public virtual DbSet<IM_GROUPS_OU> IM_GROUPS_OU { get; set; }
        public virtual DbSet<IM_USERS> IM_USERS { get; set; }
        public virtual DbSet<IM_USERS_GROUPS> IM_USERS_GROUPS { get; set; }
        public virtual DbSet<IM_USER_METADATA> IM_USER_METADATA { get; set; }
        public virtual DbSet<IM_TOKENS_CONTROLLER> IM_TOKENS_CONTROLLER { get; set; }
        public virtual DbSet<IM_TEMPLATE_SETTING> IM_TEMPLATE_SETTING { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("IM");

            modelBuilder.Entity<IM_USERS_GROUPS>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ImUsersGroups)
                    .HasForeignKey(d => d.GROUP_ID);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ImUsersGroups)
                    .HasForeignKey(d => d.USER_NAME);
            });

            modelBuilder.Entity<IM_GROUPS_OU>(entity =>
            {

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ImGroupsOus)
                    .HasForeignKey(d => d.GROUP_ID);

                entity.HasOne(d => d.Ou)
                    .WithMany(p => p.ImGroupsOus)
                    .HasForeignKey(d => d.OU_ID);
            });

            modelBuilder.Entity<IM_OU>(entity =>
            {

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ImOus)
                    .HasForeignKey(d => d.COMPANY_ID);

            });

            // modelBuilder.Entity<IM_COMPANY>()
            //     .HasMany(e => e.ImOus)
            //     .WithOne(c => c.Company)
            //     .HasForeignKey(f => f.COMPANY_ID);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsSubclassOf(typeof(BaseEntity)))
                {
                    RenameAuditColumns(modelBuilder, entityType.ClrType);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        private void RenameAuditColumns(ModelBuilder modelBuilder, Type entityType)
        {
            modelBuilder.Entity(entityType).Property(nameof(BaseEntity.CreatedDate)).HasColumnName("CREATEDDATE");
            modelBuilder.Entity(entityType).Property(nameof(BaseEntity.CreatedBy)).HasColumnName("CREATEDBY");
            modelBuilder.Entity(entityType).Property(nameof(BaseEntity.UpdatedBy)).HasColumnName("UPDATEDBY");
            modelBuilder.Entity(entityType).Property(nameof(BaseEntity.UpdatedDate)).HasColumnName("UPDATEDDATE");
        }
    }
}
