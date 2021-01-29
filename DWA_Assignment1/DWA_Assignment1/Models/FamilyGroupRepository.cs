using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DWA_Assignment1.Models
{
    public class FamilyGroupRepository : IRepository<FamilyGroup>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(FamilyGroup entity)
        {
            context.FamilyGroups.Add(entity);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public FamilyGroup Find(int? Id)
        {
            return context.FamilyGroups.Find(Id);
        }

        public List<FamilyGroup> ToList()
        {
            return context.FamilyGroups.ToList();
        }

        public void Update(FamilyGroup entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public UserManager<ApplicationUser> CreateUserStore()
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            return manager;
        }

        public IEnumerable<FamilyGroup> Search(SearchViewModel Search)
        {
            throw new NotImplementedException();
        }
    }
}