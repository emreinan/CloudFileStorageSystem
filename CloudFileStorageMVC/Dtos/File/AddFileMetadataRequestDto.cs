namespace CloudFileStorageMVC.Dtos.File;

public class AddFileMetadataRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SharingType { get; set; }
    public string PermissionLevel { get; set; }
    public List<int>? SharedWithUserIds { get; set; }

    public AddFileMetadataRequestDto(string name, string description, string sharingType, string permissionLevel)
    {
        Name = name;
        Description = description;
        SharingType = sharingType;
        PermissionLevel = permissionLevel;
    }
}
