using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DWA_Assignment1.Models
{
    public class EventRepository : IRepository<Event>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Event entity)
        {
            context.Events.Include(l => l.Lanes);
            context.Events.Add(entity);
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

        public Event Find(int? Id)
        {
            return context.Events.Find(Id);
        }

        public IEnumerable<Event> Search(SearchViewModel Search)
        {
            var model =
                from m in context.Events
                orderby m.EventId descending
                where (Search.SwimmerId == null || m.Lanes.Any(l => l.Swimmer.Id == Search.SwimmerId))
                where (Search.EventAgeRange == null || m.AgeRange == Search.EventAgeRange)
                where (Search.EventGender == null || m.Gender == Search.EventGender)
                where (Search.EventDistance == null || m.Distance.StartsWith(Search.EventDistance))
                where (Search.EventSwimStroke == null || m.Stroke.StartsWith(Search.EventSwimStroke))
                select m;

            return model;
        }

        public List<Event> ToList()
        {
            return context.Events.ToList();
        }

        public void Update(Event entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}