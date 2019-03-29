﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Untitled.Models;

namespace Project_Untitled.Migrations
{
    [DbContext(typeof(AppIdentityDbContext))]
    partial class AppIdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Project_Untitled.Models.Clips", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName");

                    b.Property<string>("FileStatus")
                        .IsRequired();

                    b.Property<int>("ProfileId");

                    b.Property<DateTime>("UploadAt");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Clips");
                });

            modelBuilder.Entity("Project_Untitled.Models.Following", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FollowerId");

                    b.Property<string>("FollowingId");

                    b.Property<int>("ProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Followings");
                });

            modelBuilder.Entity("Project_Untitled.Models.Liked", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClipId");

                    b.Property<int>("FileId");

                    b.Property<string>("LikeByUserId");

                    b.HasKey("Id");

                    b.HasIndex("ClipId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Project_Untitled.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("ReadAt");

                    b.Property<string>("RecipentId");

                    b.Property<string>("RecipentStatus")
                        .IsRequired();

                    b.Property<string>("RecipentUserName");

                    b.Property<string>("SenderId");

                    b.Property<string>("SenderStatus")
                        .IsRequired();

                    b.Property<string>("SenderUserName");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Project_Untitled.Models.Notifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CommentOnYourPostDevice");

                    b.Property<bool>("CommentOnYourPostEmail");

                    b.Property<bool>("DesktopNotification");

                    b.Property<bool>("LikeOnYourPostDevice");

                    b.Property<bool>("LikeOnYourPostEmail");

                    b.Property<bool>("NewFeatureDevice");

                    b.Property<bool>("NewFeatureEmail");

                    b.Property<bool>("NewFollowerDevice");

                    b.Property<bool>("NewFollowerEmail");

                    b.Property<bool>("NewMessageDevice");

                    b.Property<bool>("NewMessageEmail");

                    b.Property<bool>("NewPostByFollowedUserDevice");

                    b.Property<bool>("NewPostByFollowedUserEmail");

                    b.Property<bool>("NewsLetterDevice");

                    b.Property<bool>("NewsLetterEmail");

                    b.Property<bool>("RepostOfPostDevice");

                    b.Property<bool>("RepostOfPostEmail");

                    b.Property<bool>("SuggestedContentDevice");

                    b.Property<bool>("SuggestedContentEmail");

                    b.Property<bool>("SurveyAndFeedbackDevice");

                    b.Property<bool>("SurveyAndFeedbackEmail");

                    b.Property<bool>("TipsAndOffersDevice");

                    b.Property<bool>("TipsAndOffersEmail");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Project_Untitled.Models.UserHandler", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdentityId");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project_Untitled.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Followers");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Project_Untitled.Models.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllowMessages");

                    b.Property<string>("Biography");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Facebook");

                    b.Property<string>("Gender");

                    b.Property<string>("HeaderImage");

                    b.Property<string>("Instagram");

                    b.Property<string>("Livestream");

                    b.Property<string>("Location");

                    b.Property<string>("Mixer");

                    b.Property<string>("Name");

                    b.Property<string>("Periscope");

                    b.Property<string>("ProfileImage");

                    b.Property<string>("Reddit");

                    b.Property<string>("Spotify");

                    b.Property<string>("Tumblr");

                    b.Property<string>("Twitch");

                    b.Property<string>("Twitter");

                    b.Property<int?>("UserId");

                    b.Property<string>("Wordpress");

                    b.Property<string>("Youtube");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_Untitled.Models.Clips", b =>
                {
                    b.HasOne("Project_Untitled.Models.UserProfile", "UserProfile")
                        .WithMany("Clips")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_Untitled.Models.Following", b =>
                {
                    b.HasOne("Project_Untitled.Models.UserProfile", "UserProfile")
                        .WithMany("Following")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_Untitled.Models.Liked", b =>
                {
                    b.HasOne("Project_Untitled.Models.Clips", "Clips")
                        .WithMany("Likes")
                        .HasForeignKey("ClipId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_Untitled.Models.Notifications", b =>
                {
                    b.HasOne("Project_Untitled.Models.UserHandler")
                        .WithOne("Notifications")
                        .HasForeignKey("Project_Untitled.Models.Notifications", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_Untitled.Models.UserProfile", b =>
                {
                    b.HasOne("Project_Untitled.Models.UserHandler", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Project_Untitled.Models.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_Untitled.Models.UserSettings", b =>
                {
                    b.HasOne("Project_Untitled.Models.UserHandler", "UserHandler")
                        .WithOne("UserSettings")
                        .HasForeignKey("Project_Untitled.Models.UserSettings", "UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
