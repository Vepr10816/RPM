using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RPM_3_Course.Models
{
    public class User
    {
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        public string Middle_Name { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        public string Date_Of_Birth { get; set; }


        [Required(ErrorMessage = "Не указана почта!")]
        [EmailAddress(ErrorMessage = "Некорректный адрес!")]
        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        //[Remote(action: "CheckEmail", controller: "Home", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан логин!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 10 символов!")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не введён пароль!")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public int RolesId { get; set; }      // внешний ключ
        public Roles Roles { get; set; }    // навигационное свойство
        public int PictureeId { get; set; }      // внешний ключ
        public Picturee Picturee { get; set; }    // навигационное свойство
    }
    public enum SortState
    {
        IdAsc,
        IdDesc,
        EmailAsc,
        EmailDesc
    }
}
