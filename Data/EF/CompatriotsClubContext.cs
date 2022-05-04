using CompatriotsClub.Entities;
using Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CompatriotsClub.Data
{
    public partial class CompatriotsClubContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>
    , AppUserRoles
    , IdentityUserLogin<Guid>
    , IdentityRoleClaim<Guid>
    , IdentityUserToken<Guid>>
    {
        public CompatriotsClubContext(DbContextOptions options)
            : base(options)
        {
        }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressMember> AddressMembers { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<ContactMembers> ContactMembers { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<PositionMember> RoleMembers { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<AppUserRoles> AppUserRoles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Feel> Feel { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Old
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StayingAddress).IsRequired();

                entity.Property(e => e.Ward)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AddressMember>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.AddressId })
                    .HasName("PK__Address___AC6189B778B5E6D6");

                entity.ToTable("AddressMember");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.AddressMembers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address_M__Addre__4BAC3F29");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.AddressMembers)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address_M__Membe__4CA06362");
            });

            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.Property(p => p.Description).IsRequired();
            });

            modelBuilder.Entity<AppUserRoles>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(true);

                entity.Property(e => e.Description).IsUnicode(true);
            });

            modelBuilder.Entity<ContactMembers>(entity =>
            {

                entity.ToTable("ContactMember");

                entity.HasKey(ur => new { ur.MemberId, ur.RoleId });

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.ContactMembers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contact_R1__asdasd__534D60F1");


                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactMembers)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contact_M__Conta__534D60F1");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ContactMembers)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contact_M__Membe__5441852A");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("Family");


                entity.Property(e => e.IdMember)
                    .HasMaxLength(450)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.IdMember).HasColumnName("idMember");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Idcard)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDCard");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(450)
                   .IsUnicode(false);

                entity.Property(e => e.Addres)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);



                entity.Property(e => e.Word)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.IdAccount).IsRequired(false);
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Member__GroupId__5629CD9C");
            });

            modelBuilder.Entity<MemberUser>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.UserId })
                    .HasName("PK__MemberUS__DD88C7DC1D75B605");

                entity.ToTable("MemberUSer");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberUsers)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberUSe__Membe__66603565");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MemberUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberUSe__UserI__6754599E");
            });

            modelBuilder.Entity<Position>(entity =>
           {
               entity.ToTable("Position");

               entity.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(true);

               entity.Property(e => e.PositionType)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(true);

               entity.Property(e => e.Description).IsUnicode(true);

           });

            modelBuilder.Entity<PositionMember>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.PositionId })
                    .HasName("PK__Role_Mem__B45FE7F9811444D9");

                entity.ToTable("PositionMember");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.RoleMembers)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Role_Memb__Membe__5812160E");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.PositionMembers)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Role_Memb__RoleI__59063A47");
            });
            #endregion

            #region social
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new ConmentConfiguration());
            modelBuilder.ApplyConfiguration(new FeelConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
