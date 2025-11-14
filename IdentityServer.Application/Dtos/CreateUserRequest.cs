using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Application.Dtos;

public class CreateUserRequest
{
    [Required(ErrorMessage = "This field must not be empty!")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email is not in a valid format.")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "This field must not be empty!")]
    public required string UserName { get; set; }
    [Required(ErrorMessage = "This field must not be empty!")]
    public required string Password { get; set; }
    [Required(ErrorMessage = "This field must not be empty!")]
    public required string FirstName { get; set; }
    [Required(ErrorMessage = "This field must not be empty!")]
    public required string LastName { get; set; }
    [RegularExpression(@"^(?:0|84)(?:2(?:0[3-9]|1[0-9]|2[0-9]|3[2-9]|4[0-9]|5[0-9]|6[0-9]|7[0-9]|8[0-9]|9[0-9])|3[2-9]|5[25689]|7[06-9]|8[0-9]|9[012346789])[0-9]{7}$", ErrorMessage = "Phone number is not in a valid format.")]
    public required string PhoneNumber { get; set; }
    public required string Role { get; set; } = "User";
}