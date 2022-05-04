﻿// <auto-generated />
using System;
using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(CompatriotsClubContext))]
    partial class CompatriotsClubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_CI_AS")
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CompatriotsClub.Data.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .HasMaxLength(450)
                        .IsUnicode(false)
                        .HasColumnType("varchar(450)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StayingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.AddressMember", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "AddressId")
                        .HasName("PK__Address___AC6189B778B5E6D6");

                    b.HasIndex("AddressId");

                    b.ToTable("AddressMember", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool?>("ActiveAccount")
                        .HasColumnType("bit");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.AppUserRoles", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoleId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.ContactMembers", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "RoleId");

                    b.HasIndex("ContactId");

                    b.HasIndex("RoleId");

                    b.ToTable("ContactMember", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.Contacts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Contact", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.Family", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdMember")
                        .HasMaxLength(450)
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Family", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdMember")
                        .HasColumnType("int")
                        .HasColumnName("idMember");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("CompatriotsClub.Data.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Addres")
                        .HasMaxLength(450)
                        .IsUnicode(false)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(450)
                        .IsUnicode(false)
                        .HasColumnType("varchar(450)");

                    b.Property<int>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("IdAccount")
                        .HasColumnType("int");

                    b.Property<string>("Idcard")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("IDCard");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Word")
                        .HasMaxLength(450)
                        .IsUnicode(false)
                        .HasColumnType("varchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Member", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.MemberUser", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MemberId", "UserId")
                        .HasName("PK__MemberUS__DD88C7DC1D75B605");

                    b.HasIndex("UserId");

                    b.ToTable("MemberUSer", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("CompatriotsClub.Data.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PositionType")
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.PositionMember", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "PositionId")
                        .HasName("PK__Role_Mem__B45FE7F9811444D9");

                    b.HasIndex("PositionId");

                    b.ToTable("PositionMember", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.RolePermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("CompatriotsClub.Data.UserPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(8712));

                    b.Property<DateTime>("DateMoodified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(8973));

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Feel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Feel");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(7430));

                    b.Property<long?>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(2912));

                    b.Property<DateTime>("DateMoodified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CompatriotsClub.Data.AddressMember", b =>
                {
                    b.HasOne("CompatriotsClub.Data.Address", "Address")
                        .WithMany("AddressMembers")
                        .HasForeignKey("AddressId")
                        .IsRequired()
                        .HasConstraintName("FK__Address_M__Addre__4BAC3F29");

                    b.HasOne("CompatriotsClub.Data.Member", "Member")
                        .WithMany("AddressMembers")
                        .HasForeignKey("MemberId")
                        .IsRequired()
                        .HasConstraintName("FK__Address_M__Membe__4CA06362");

                    b.Navigation("Address");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("CompatriotsClub.Data.AppUserRoles", b =>
                {
                    b.HasOne("CompatriotsClub.Data.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompatriotsClub.Data.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId1");

                    b.HasOne("CompatriotsClub.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompatriotsClub.Data.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId1");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompatriotsClub.Data.ContactMembers", b =>
                {
                    b.HasOne("CompatriotsClub.Data.Contacts", "Contact")
                        .WithMany("ContactMembers")
                        .HasForeignKey("ContactId")
                        .IsRequired()
                        .HasConstraintName("FK__Contact_M__Conta__534D60F1");

                    b.HasOne("CompatriotsClub.Data.Member", "Member")
                        .WithMany("ContactMembers")
                        .HasForeignKey("MemberId")
                        .IsRequired()
                        .HasConstraintName("FK__Contact_M__Membe__5441852A");

                    b.HasOne("CompatriotsClub.Data.Position", "Position")
                        .WithMany("ContactMembers")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__Contact_R1__asdasd__534D60F1");

                    b.Navigation("Contact");

                    b.Navigation("Member");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("CompatriotsClub.Data.Member", b =>
                {
                    b.HasOne("CompatriotsClub.Data.Group", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId")
                        .IsRequired()
                        .HasConstraintName("FK__Member__GroupId__5629CD9C");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("CompatriotsClub.Data.MemberUser", b =>
                {
                    b.HasOne("CompatriotsClub.Data.Member", "Member")
                        .WithMany("MemberUsers")
                        .HasForeignKey("MemberId")
                        .IsRequired()
                        .HasConstraintName("FK__MemberUSe__Membe__66603565");

                    b.HasOne("CompatriotsClub.Data.AppUser", "User")
                        .WithMany("MemberUsers")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__MemberUSe__UserI__6754599E");

                    b.Navigation("Member");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompatriotsClub.Data.PositionMember", b =>
                {
                    b.HasOne("CompatriotsClub.Data.Member", "Member")
                        .WithMany("RoleMembers")
                        .HasForeignKey("MemberId")
                        .IsRequired()
                        .HasConstraintName("FK__Role_Memb__Membe__5812160E");

                    b.HasOne("CompatriotsClub.Data.Position", "Position")
                        .WithMany("PositionMembers")
                        .HasForeignKey("PositionId")
                        .IsRequired()
                        .HasConstraintName("FK__Role_Memb__RoleI__59063A47");

                    b.Navigation("Member");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("CompatriotsClub.Data.RolePermission", b =>
                {
                    b.HasOne("CompatriotsClub.Data.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompatriotsClub.Data.AppRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CompatriotsClub.Data.UserPermission", b =>
                {
                    b.HasOne("CompatriotsClub.Data.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompatriotsClub.Data.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Comment", b =>
                {
                    b.HasOne("CompatriotsClub.Entities.Post", "Post")
                        .WithMany("Conments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CompatriotsClub.Data.AppUser", "User")
                        .WithMany("Conments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Feel", b =>
                {
                    b.HasOne("CompatriotsClub.Entities.Post", "Post")
                        .WithMany("Feel")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CompatriotsClub.Data.AppUser", "User")
                        .WithMany("Feel")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Image", b =>
                {
                    b.HasOne("CompatriotsClub.Entities.Post", "Post")
                        .WithMany("Images")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Post", b =>
                {
                    b.HasOne("CompatriotsClub.Data.AppUser", "AppUser")
                        .WithMany("Albums")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("CompatriotsClub.Data.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CompatriotsClub.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CompatriotsClub.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CompatriotsClub.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompatriotsClub.Data.Address", b =>
                {
                    b.Navigation("AddressMembers");
                });

            modelBuilder.Entity("CompatriotsClub.Data.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CompatriotsClub.Data.AppUser", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("Conments");

                    b.Navigation("Feel");

                    b.Navigation("MemberUsers");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CompatriotsClub.Data.Contacts", b =>
                {
                    b.Navigation("ContactMembers");
                });

            modelBuilder.Entity("CompatriotsClub.Data.Group", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("CompatriotsClub.Data.Member", b =>
                {
                    b.Navigation("AddressMembers");

                    b.Navigation("ContactMembers");

                    b.Navigation("MemberUsers");

                    b.Navigation("RoleMembers");
                });

            modelBuilder.Entity("CompatriotsClub.Data.Permission", b =>
                {
                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("CompatriotsClub.Data.Position", b =>
                {
                    b.Navigation("ContactMembers");

                    b.Navigation("PositionMembers");
                });

            modelBuilder.Entity("CompatriotsClub.Entities.Post", b =>
                {
                    b.Navigation("Conments");

                    b.Navigation("Feel");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
