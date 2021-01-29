using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace DWA_Assignment1.Models
{
    public class LaneRepository : IRepository<Lane>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Lane entity)
        {
            context.Lanes.Add(entity);
            context.SaveChanges();
        }

        public UserManager<ApplicationUser> CreateUserStore()
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            return manager;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Lane Find(int? Id)
        {
            return context.Lanes.Find(Id);
        }

        public IEnumerable<Lane> Search(SearchViewModel Search)
        {
            var model =
                from m in context.Lanes
                orderby m.LaneId descending
                where (Search.SwimmerId == null || m.Swimmer.Id == Search.SwimmerId)
                where (Search.SwimmerFirstName == null || m.Swimmer.FirstName.StartsWith(Search.SwimmerFirstName))
                where (Search.SwimmerLastName == null || m.Swimmer.LastName.StartsWith(Search.SwimmerLastName))
                where (Search.SwimmerDOBStartDateDT == null && Search.SwimmerDOBEndDateDT == null || m.Swimmer.DateOfBirth > Search.SwimmerDOBStartDateDT && m.Swimmer.DateOfBirth < Search.SwimmerDOBEndDateDT)
                select m;

            return model;
        }

        public List<Lane> ToList()
        {
            return context.Lanes.ToList();
        }

        public void Update(Lane entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}