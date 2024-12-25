﻿using FileMetadataAPI.Domain.Enums;

namespace FileMetadataAPI.Domain.Entities;

public class FileShare
{
    public int FileId { get; set; }
    public int UserId { get; set; }
    public Permission Permission { get; set; }

    public File File { get; set; } = default!;
}
