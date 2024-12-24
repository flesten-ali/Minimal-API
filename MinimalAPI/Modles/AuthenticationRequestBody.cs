using System.ComponentModel.DataAnnotations;
namespace MinimalAPI.Modles;

public class AuthenticationRequestBody
{
    [Required(ErrorMessage = "UserName is required!")]
    public string UserName { get; set; }

    [Required(ErrorMessage ="Password is required!")]
    public string Password { get; set; }
}
