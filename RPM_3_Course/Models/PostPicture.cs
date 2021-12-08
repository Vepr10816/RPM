using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPM_3_Course.Models
{
    public class PostPicture
    {
        [Key]
        public int Id { get; set; }
        public string Name_Picture { get; set; }
        public string Path { get; set; }

    }
}
