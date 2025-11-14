using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Application.Dtos;

public class UpdateUserRequest
{
    [Required(ErrorMessage = "This field must not be empty!")]
    [Length(7, 30, ErrorMessage = "Min length is 7 and Max is 30 chars")]
    public required string FirstName { get; set; }
    [Required(ErrorMessage = "This field must not be empty!")]
    [Length(7, 50, ErrorMessage = "Min length is 7 and Max is 30 chars")]
    public required string LastName { get; set; }
    [Required(ErrorMessage = "This field must not be empty!")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email is not in a valid format.")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "This field must not be empty!")]
    [RegularExpression(@"^(?:0|84)(?:2(?:0[3-9]|1[0-9]|2[0-9]|3[2-9]|4[0-9]|5[0-9]|6[0-9]|7[0-9]|8[0-9]|9[0-9])|3[2-9]|5[25689]|7[06-9]|8[0-9]|9[012346789])[0-9]{7}$", ErrorMessage = "Phone number is not in a valid format.")]
    public required string PhoneNumber { get; set; }
    [Required(ErrorMessage = "This field must not be empty!")]
    [Length(7, 30, ErrorMessage = "Min length is 7 and Max is 30 chars")]
    public required string UserName { get; set; }

}