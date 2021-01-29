namespace DWA_Assignment1.Migrations
{
    using DWA_Assignment1.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DWA_Assignment1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DWA_Assignment1.Models.ApplicationDbContext context)
        {
            //Create roles
            var roleStore = new RoleStore<IdentityRole>(context);
            var rManager = new RoleManager<IdentityRole>(roleStore);

            if (!context.Roles.Any(r => r.Name == "Club Official"))
            {
                var role = new IdentityRole { Id = "1", Name = "Club Official" };

                rManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Swimmer"))
            {
                var role = new IdentityRole { Id = "2", Name = "Swimmer" };

                rManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Parent"))
            {
                var role = new IdentityRole { Id = "3", Name = "Parent" };

                rManager.Create(role);
            }


            //Create and add users to roles
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            if (!context.Users.Any(u => u.UserName == "Admin@gmail.co.uk"))
            {
                
                var user = new ApplicationUser
                {
                    UserName = "Admin@gmail.co.uk",
                    Email = "Admin@gmail.co.uk",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Gender = "Male",
                    Address = "London",
                    PhoneNumber = "0800750224",
                    DateOfBirth = new DateTime(1980, 5, 21)
                };

                manager.Create(user, "Password123!");

                manager.AddToRole(user.Id, "Club Official");
            }

            if(!context.Users.Any(u => u.UserName == "Shiela@gmail.com"))
            {
                var parent = new ApplicationUser
                {
                    UserName = "Shiela@gmail.com",
                    Email = "Shiela@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Shiela",
                    LastName = "Colins",
                    Gender = "Female",
                    PhoneNumber = "0800001066",
                    Address = "Birmingham",
                    DateOfBirth = new DateTime(1980, 10, 10)
                };

                manager.Create(parent, "Shiela1!");

                manager.AddToRole(parent.Id, "Parent");
            }

            if (!context.Users.Any(u => u.UserName == "Colin@gmail.com"))
            {
                var u1 = new ApplicationUser
                {
                    UserName = "Colin@gmail.com",
                    Email = "Colin@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Colin",
                    LastName = "Colins",
                    Gender = "Male",
                    Address = "Birmingham",
                    DateOfBirth = new DateTime(1980, 10, 10)
                };

                manager.Create(u1, "Colin1!");

                manager.AddToRole(u1.Id, "Swimmer");
            }

            if (!context.Users.Any(u => u.UserName == "David@gmail.com"))
            {
                var u2 = new ApplicationUser
                {
                    UserName = "David@gmail.com",
                    Email = "David@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "David",
                    LastName = "Colins",
                    Gender = "Male",
                    Address = "Birmingham",
                    DateOfBirth = new DateTime(1980, 10, 10)
                };

                manager.Create(u2, "David1!");

                manager.AddToRole(u2.Id, "Swimmer");
            }

            if (!context.Users.Any(u => u.UserName == "Alex@gmail.com"))
            {
                var u3 = new ApplicationUser
                {
                    UserName = "Alex@gmail.com",
                    Email = "Alex@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "Alex",
                    LastName = "Colins",
                    Gender = "Male",
                    Address = "Birmingham",
                    DateOfBirth = new DateTime(1980, 10, 10)
                };

                manager.Create(u3, "Alex1!");

                manager.AddToRole(u3.Id, "Swimmer");
            }

            //Create Family Group
            if (!context.FamilyGroups.Any())
            {
                var p = manager.FindByEmail("Shiela@gmail.com");
                var c1 = manager.FindByEmail("Colin@gmail.com");
                var c2 = manager.FindByEmail("David@gmail.com");
                var c3 = manager.FindByEmail("Alex@gmail.com");

                List<ApplicationUser> children = new List<ApplicationUser>();
                children.Add(c1);
                children.Add(c2);
                children.Add(c3);

                var group = new FamilyGroup
                {
                    GroupName = "Colins",
                    Parent = p,
                    Swimmers = children,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber
                };

                context.FamilyGroups.AddOrUpdate(group);
            }

            var eve = new Event
            {
                AgeRange = "Junior",
                Distance = "100m",
                Gender = "Male",
                
            };
        }
    }
}
