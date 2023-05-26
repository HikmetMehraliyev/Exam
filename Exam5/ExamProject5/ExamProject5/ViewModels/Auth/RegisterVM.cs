using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject5.ViewModels.Auth
{
    public class RegisterVM
    {
        [Required,MaxLength(60)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required"), MaxLength(60),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required"), MaxLength(60), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(60), DataType(DataType.Password),Compare(nameof(Password),ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
