using System.ComponentModel.DataAnnotations;

namespace dotnetBlazorFullStuck.Web.Components.Models
{
    public class RegisterFormModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
