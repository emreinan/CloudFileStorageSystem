﻿using FileMetadataAPI.Domain.Enums;

namespace FileMetadataAPI.Domain.Entities;

public class FileShare
{
    public int Id { get; set; }
    public int FileId { get; set; }
    public int UserId { get; set; }
    public PermissionLevel PermissionLevel { get; set; }

    public File File { get; set; } = default!;
}
