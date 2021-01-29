using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DWA_Assignment1.Models
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public UserManager<ApplicationUser> CreateUserStore()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Find(int? Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> Search(SearchViewModel Search)
        {
            var model =
                from m in context.Users
                orderby m.Email descending
                where (m.Roles.Any(r => r.RoleId == "Swimmer"))
                select m;

            return model;
        }

        public List<ApplicationUser> ToList()
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}