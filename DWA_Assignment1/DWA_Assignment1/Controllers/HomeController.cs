using DWA_Assignment1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace DWA_Assignment1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private ApplicationDbContext context = new ApplicationDbContext();
        private IRepository<Event> eventRP;/* = new EventRepository();*/
        private IRepository<FamilyGroup> famGroupRP;/* = new FamilyGroupRepository();*/
        private IRepository<Lane> laneRP;/* = new LaneRepository();*/
        private IRepository<Meet> meetRP;/* = new MeetRepository();*/
        //private IRepository<ApplicationUser> userRP = new UserRepository();

        public HomeController()
        {
            eventRP = new EventRepository();
            famGroupRP = new FamilyGroupRepository();
            laneRP = new LaneRepository();
            meetRP = new MeetRepository();
        }

        public HomeController(IRepository<Event> repository)
        {
            eventRP = repository;
        }

        public HomeController(IRepository<FamilyGroup> repository)
        {
            famGroupRP = repository;
        }

        public HomeController(IRepository<Lane> repository)
        {
            laneRP = repository;
        }

        public HomeController(IRepository<Meet> repository)
        {
            meetRP = repository;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = meetRP.ToList();

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Club Official")]
        public ViewResult ClubOfficialOptions()
        {
            return View();
        }

        //
        //GET: Find Family Group
        [Authorize(Roles = "Club Official,Parent")]
        public ViewResult FindFamilyGroups()
        {
            return View();
        }

        //
        //POST: Find Family Group
        [Authorize(Roles = "Club Official,Parent")]
        [HttpPost]
        public ActionResult FindFamilyGroups(int? groupId)
        {
            if (groupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //FamilyGroup group = context.FamilyGroups.Find(groupId);
            FamilyGroup group = famGroupRP.Find(groupId);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View("FamilyGroupDisplay", group);
        }

        //
        //GET: Create Family Group
        [Authorize(Roles = "Club Official")]
        public ViewResult CreateFamilyGroup()
        {
            return View();
        }

        //
        //POST: Create Family Group
        [Authorize(Roles = "Club Official")]
        [HttpPost]
        public ActionResult CreateFamilyGroup(FamilyGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var store = new UserStore<ApplicationUser>(context);
                //var manager = new UserManager<ApplicationUser>(store);

                var manager = famGroupRP.CreateUserStore();

                ApplicationUser child2 = null;
                ApplicationUser child3 = null;
                ApplicationUser child4 = null;
                ApplicationUser child5 = null;

                List<ApplicationUser> swimmerList = new List<ApplicationUser>();
                if (model.ParentEmail == null || model.Child1Email == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var parent = manager.FindByEmail(model.ParentEmail);

                if (parent == null)
                {
                    return HttpNotFound();
                }

                var child1 = manager.FindByEmail(model.Child1Email);

                if (child1 == null)
                {
                    return HttpNotFound();
                }

                FamilyGroup group = new FamilyGroup
                {
                    GroupName = model.GroupName,
                    Email = model.ParentEmail,
                    PhoneNumber = model.PhoneNumber,
                    Parent = parent,
                    Swimmers = swimmerList
                };

                group.Swimmers.Add(child1);

                if (model.Child2Email != null)
                {
                    child2 = manager.FindByEmail(model.Child2Email);

                    if (child2 != null)
                    {
                        group.Swimmers.Add(child2);
                    }
                }

                if (model.Child3Email != null)
                {
                    child3 = manager.FindByEmail(model.Child3Email);

                    if (child3 != null)
                    {
                        group.Swimmers.Add(child3);
                    }
                }

                if (model.Child4Email != null)
                {
                    child4 = manager.FindByEmail(model.Child4Email);

                    if (child4 != null)
                    {
                        group.Swimmers.Add(child4);
                    }
                }

                if (model.Child5Email != null)
                {
                    child5 = manager.FindByEmail(model.Child5Email);

                    if (child5 != null)
                    {
                        group.Swimmers.Add(child5);
                    }
                }

                //context.FamilyGroups.Add(group);
                famGroupRP.Add(group);

                parent.FamilyGroupId = group.GroupId;
                manager.Update(parent);

                child1.FamilyGroupId = group.GroupId;
                manager.Update(child1);

                if (child2 != null)
                {
                    child2.FamilyGroupId = group.GroupId;
                    manager.Update(child2);
                }

                if (child3 != null)
                {
                    child3.FamilyGroupId = group.GroupId;
                    manager.Update(child3);
                }

                if (child4 != null)
                {
                    child2.FamilyGroupId = group.GroupId;
                    manager.Update(child4);
                }

                if (child5 != null)
                {
                    child2.FamilyGroupId = group.GroupId;
                    manager.Update(child5);
                }

                return View("FamilyGroupDisplay", group);
            }
            return View("CreateFamilyGroup");
        }

        [Authorize(Roles = "Club Official,Parent,Swimmer")]
        public ActionResult FindCurrentUserFamilyGroup()
        {
            //var store = new UserStore<ApplicationUser>(context);
            //var manager = new UserManager<ApplicationUser>(store);

            var manager = famGroupRP.CreateUserStore();

            string userId = User.Identity.GetUserId();

            var user = manager.FindById(userId);

            if(user == null)
            {
                return HttpNotFound();
            }

            //var group = context.FamilyGroups.Find(user.FamilyGroupId);
            var group = famGroupRP.Find(user.FamilyGroupId);

            if(group == null)
            {
                return HttpNotFound();
            }

            return View("FamilyGroupDisplay", group);
        }

        [Authorize(Roles = "Club Official,Parent")]
        public ViewResult EditUserFamilyGroup(int? GroupId)
        {
            ViewBag.GroupId = GroupId;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Club Official,Parent")]
        public ActionResult EditUserFamilyGroup(FamilyGroupViewModel model)
        {
            if(model.GroupId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var group = context.FamilyGroups.Find(model.GroupId);
            var group = famGroupRP.Find(model.GroupId);

            if(group == null)
            {
                return HttpNotFound();
            }

            group.GroupName = model.GroupName;
            group.PhoneNumber = model.PhoneNumber;

            //context.SaveChanges();
            famGroupRP.Update(group);

            return View("FamilyGroupDisplay", group);
        }

        //[Authorize(Roles = "Club Official")]
        //public ViewResult FindLane()
        //{
        //    return View();
        //}

        //[HttpPost]
        [AllowAnonymous]
        public ActionResult FindLane(int? LaneId)
        {
            if(LaneId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var lane = context.Lanes.Find(LaneId);
            var lane = laneRP.Find(LaneId);

            if(lane == null)
            {
                return HttpNotFound();
            }

            return View("LaneDisplay", lane);
        }

        [Authorize(Roles = "Club Official")]
        public ViewResult CreateLane(int? EventId)
        {
            LaneViewModel lane = new LaneViewModel
            {
                EventId = EventId
            };

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Club Official")]
        public ActionResult CreateLane(LaneViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var store = new UserStore<ApplicationUser>(context);
                //var manager = new UserManager<ApplicationUser>(store);
                var manager = laneRP.CreateUserStore();

                var swimmer = manager.FindByEmail(model.SwimmerEmail);

                TimeSpan t = new TimeSpan(23, 59, 59);

                Lane lane = new Lane
                {
                    EventId = model.EventId,
                    LaneNumber = model.LaneNumber,
                    Heat = model.Heat,
                    Swimmer = swimmer,
                    SwimmerTime = t
                };

                //context.Lanes.Add(lane);
                //context.SaveChanges();
                laneRP.Add(lane);

                return View("LaneDisplay", lane);
            }

            return View(model);
        }

        [Authorize(Roles = "Club Official")]
        public ViewResult CreateEvent(int? MeetId)
        {
            EventViewModel eve = new EventViewModel
            {
                MeetId = MeetId
            };

            return View(eve);
        }

        [HttpPost]
        [Authorize(Roles = "Club Official")]
        public ActionResult CreateEvent(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Lane> laneList = new List<Lane>();
                
                var eve = new Event
                {
                    MeetId = model.MeetId,
                    AgeRange = model.AgeRange,
                    Distance = model.Distance,
                    Stroke = model.Stroke,
                    Gender = model.Gender,
                    Lanes = laneList
                };

                eventRP.Add(eve);

                //context.Events.Add(eve);
                //context.SaveChanges();

                return View("EventDisplay", eve);
            }

            return View(model);
        }

        //[Authorize(Roles = "Club Official")]
        //public ViewResult FindEvent()
        //{
        //    return View();
        //}

        //[HttpPost]
        [AllowAnonymous]
        public ActionResult FindEvent(int? EventId)
        {
            if (EventId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var eve = context.Lanes.Find(EventId);
            var eve = eventRP.Find(EventId);

            if (eve == null)
            {
                return HttpNotFound();
            }

            return View("EventDisplay", eve);
        }

        [Authorize(Roles = "Club Official")]
        public ViewResult CreateMeet()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Club Official")]
        public ActionResult CreateMeet(MeetViewModel model)
        {
            if(ModelState.IsValid)
            {
                List<Event> eventList = new List<Event>();

                var meet = new Meet
                {
                    MeetName = model.Name,
                    Venue = model.Venue,
                    Date = model.Date,
                    PoolLength = model.PoolLength,
                    Events = eventList
                };

                meetRP.Add(meet);

                return View("MeetDisplay", meet);
            }

            return View(model);
        }

        //public ViewResult FindMeet()
        //{
        //    return View();
        //}

        //[HttpPost]
        [AllowAnonymous]
        public ActionResult FindMeet(int? MeetId)
        {
            if (MeetId == null)
            {
                //try
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //    throw new HttpException(400, "No Meet Id was found");
                //}

                //catch(HttpException e)
                //{
                //    return View("ErrorPage", e);
                //}
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meet = meetRP.Find(MeetId);

            if (meet == null)
            {
                return HttpNotFound();
            }

            return View("MeetDisplay", meet);
        }

        [Authorize(Roles = "Club Official")]
        public ViewResult UpdateLaneTime(int? LaneId)
        {
            ViewBag.UpdateLaneTimeLaneId = LaneId;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Club Official")]
        public ActionResult UpdateLaneTime(int? LaneId, string Time)
        {
            if(LaneId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lane = laneRP.Find(LaneId);

            if(lane == null)
            {
                return HttpNotFound();
            }

            TimeSpan updatedTime;
            if (!TimeSpan.TryParseExact(Time, @"mm\:ss\.ff", null, out updatedTime))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            lane.SwimmerTime = updatedTime;

            laneRP.Update(lane);
            
            return View("LaneDisplay", lane);
        }

        [AllowAnonymous]
        public ViewResult SearchPage()
        {
            return View();
        }

        [Authorize(Roles = "Swimmer")]
        public ActionResult CurrentSwimmerMeets()
        {
            string userId = User.Identity.GetUserId();
            SearchViewModel model = new SearchViewModel
            {
                SwimmerId = userId
            };

            var meets = meetRP.Search(model);

            return View("SearchMeetResults", meets);
        }

        [Authorize(Roles = "Swimmer")]
        public ActionResult CurrentSwimmerEvents()
        {
            string userId = User.Identity.GetUserId();
            SearchViewModel model = new SearchViewModel
            {
                SwimmerId = userId
            };

            var events = eventRP.Search(model);

            return View("SearchEventResults", events);
        }

        [Authorize(Roles = "Swimmer")]
        public ActionResult CurrentSwimmerLanes()
        {
            string userId = User.Identity.GetUserId();
            SearchViewModel model = new SearchViewModel
            {
                SwimmerId = userId
            };

            var lanes = laneRP.Search(model);

            return View("SearchLaneResults", lanes);
        }

        [Authorize(Roles = "Parent")]
        public ActionResult SearchChildMeets(string ChildId)
        {
            SearchViewModel model = new SearchViewModel
            {
                SwimmerId = ChildId
            };

            var meets = meetRP.Search(model);

            return View("SearchMeetResults", meets);
        }

        [AllowAnonymous]
        public ViewResult SearchMeets()
        {
            return View();
        }      

        [AllowAnonymous]
        public ActionResult SearchMeetResults(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var meets = meetRP.Search(model);

                return View(meets);
            }

            return View("SearchMeets", model);
        }


        public ActionResult SearchChildEvents(string ChildId)
        {
            SearchViewModel model = new SearchViewModel
            {
                SwimmerId = ChildId
            };

            var events = eventRP.Search(model);

            return View("SearchEventResults", events);
        }

        [AllowAnonymous]
        public ViewResult SearchEvents()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SearchEventResults(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var events = eventRP.Search(model);

                return View(events);
            }

            return View("SearchEvents", model);
        }

        [AllowAnonymous]
        public ViewResult SearchLanes()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SearchLaneResults(SearchViewModel model)
        {
            if(ModelState.IsValid)
            {
                var lanes = laneRP.Search(model);

                return View(lanes);
            }

            return View("SearchLanes", model);
        }
        //ToDo: Figure out what to do with the swimmer filter.
    }
}