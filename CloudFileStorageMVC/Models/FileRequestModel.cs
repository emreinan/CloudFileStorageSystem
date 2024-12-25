using System.ComponentModel.DataAnnotations;

namespace CloudFileStorageMVC.Models;

public class FileRequestModel
{
    [Required, MaxLength(500)]
    public string Description { get; set; }
    [Required]
    public string Permission { get; set; }
}
