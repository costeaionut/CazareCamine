using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CazareCamine.Data.Entities.DTO
{
    public class CurrentUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsAuthenticated { get; set; }

        public CurrentUser()
        {
            this.Roles = new List<string>();
            this.IsAuthenticated = true;
        }

    }
}
