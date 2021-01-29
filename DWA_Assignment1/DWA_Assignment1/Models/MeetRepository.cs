using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DWA_Assignment1.Models
{
    public class MeetRepository : IRepository<Meet>
    {
        private ApplicationDbContext context = new ApplicationDbContext();


        public void Add(Meet entity)
        {
            context.Meets.Add(entity);
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

        public Meet Find(int? Id)
        {
            return context.Meets.Find(Id);
        }

        public List<Meet> ToList()
        {
            return context.Meets.ToList();
        }

        public void Update(Meet entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Meet> Search(SearchViewModel Search)
        {
            var model =
                from m in context.Meets
                orderby m.MeetName descending
                where (Search.MeetName == null || m.MeetName.StartsWith(Search.MeetName))
                where (Search.MeetVenue == null || m.Venue.StartsWith(Search.MeetVenue))
                where (Search.MeetStartDateDT == null && Search.MeetEndDateDT == null || m.Date > Search.MeetStartDateDT && m.Date < Search.MeetEndDateDT)
                where (Search.SwimmerId == null || m.Events.Any(e => e.Lanes.Any(l => l.Swimmer.Id == Search.SwimmerId)))
                select m;

            return model;
        }
    }
}