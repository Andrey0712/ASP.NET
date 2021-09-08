using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSite.Areas.Admin.Models
{
    public class ViewUserRolesModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class UserWithRoles
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }

    public class ChangeRoleModel
    {
        public string Role { get; set; }
        public string OldRole { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }

    }
    public class CreateUserModel
    {
        [Display(Name = "Електронна пошта")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        [EmailAddress(ErrorMessage = "Неправильне введення пошти")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Невірний пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        public IFormFile Image { get; set; }
    }

    public class EditUserModel
    {
        [Required(ErrorMessage = "Обов'язкове поле")]
        [EmailAddress(ErrorMessage = "Неправильний формат електронної пошти")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        public string NameUser { get; set; }

        [Required(ErrorMessage = "Обовєязкове полу")]
        public string RoleUser { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Image { get; set; }
    }
}
