﻿using CloudFileStorageMVC.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace CloudFileStorageMVC.Models;

public class EditViewModel
{
    [Required, MaxLength(500)]
    public string Description { get; set; }
    [Required]
    public string SharingType { get; set; }
    public string? PermissionLevel { get; set; } = string.Empty;
    public List<int>? SharedWithUserIds { get; set; } = new();
    public List<UserDto>? SharedWithUsers { get; set; } = new();
}
