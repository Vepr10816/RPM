using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RPM_3_Course.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }      // внешний ключ
        public User User { get; set; }    // навигационное свойство
        public int PostPictureId { get; set; }      // внешний ключ
        public PostPicture PostPicture { get; set; }    // навигационное свойство
    }
}
