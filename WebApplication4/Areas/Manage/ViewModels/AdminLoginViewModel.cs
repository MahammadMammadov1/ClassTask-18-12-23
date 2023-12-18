using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
