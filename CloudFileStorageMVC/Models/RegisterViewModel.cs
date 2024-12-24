using System.ComponentModel.DataAnnotations;

namespace CloudFileStorageMVC.Models;

public class RegisterViewModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required, MinLength(4), DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required, MinLength(1), MaxLength(50)]
    public string Name { get; set; } = null!;
}