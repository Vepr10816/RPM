using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RPM_3_Course.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name_Role { get; set; }
    }
}
