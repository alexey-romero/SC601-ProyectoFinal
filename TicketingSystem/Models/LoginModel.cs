using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    //[BindNever]
    //[ValidationsFilter]
    public string Password { get; set; }
}
