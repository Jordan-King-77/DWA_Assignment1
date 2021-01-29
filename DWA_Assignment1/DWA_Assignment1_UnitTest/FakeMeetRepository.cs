using DWA_Assignment1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWA_Assignment1_UnitTest
{
    class FakeMeetRepository : IRepository<Meet>
    {
        public bool found;

        public FakeMeetRepository(bool found = true)
        {
            this.found = found;
        }

        public void Add(Meet entity)
        {
        }

        public UserManager<ApplicationUser> CreateUserStore()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Meet Find(int? Id)
        {
            if (found)
            {
                return new Meet();
            }
            return null;
        }

        public IEnumerable<Meet> Search(SearchViewModel Search)
        {
            if (found)
            {
                return new List<Meet>();
            }
            return null;
        }

        public List<Meet> ToList()
        {
            return new List<Meet>();
        }

        public void Update(Meet entity)
        {
        }
    }
}
