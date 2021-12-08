using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPM_3_Course.Models
{
    public class PostProfileView:DbContext
    {
        public IEnumerable<Post> posts { get; set; }
        public IEnumerable<PostPicture> pictureeposts { get; set; }
        public IEnumerable<User> users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostPicture> Pictureeposts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
