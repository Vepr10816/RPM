using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPM_3_Course.Models
{
    public class ProfileView
    {
        public IEnumerable<User> User { get; set; }
        public IEnumerable<Post> Post { get; set; }
    }
}
