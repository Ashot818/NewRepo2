using System.ComponentModel.DataAnnotations;

namespace dotnetBlazorFullStuck.Web.Components.Models
{
    public class LoginFormModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
