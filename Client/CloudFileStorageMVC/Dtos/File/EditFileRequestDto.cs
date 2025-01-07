namespace CloudFileStorageMVC.Dtos.File;

public class EditFileRequestDto
{
    public string Description { get; set; }
    public string SharingType { get; set; }
    public string PermissionLevel { get; set; }
    public List<int>? SharedWithUserIds { get; set; }

    public EditFileRequestDto(string description, string sharingType, string permissionLevel)
    {
        Description = description;
        SharingType = sharingType;
        PermissionLevel = permissionLevel;
    }

}
