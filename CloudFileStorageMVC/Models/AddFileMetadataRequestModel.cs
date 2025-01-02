namespace CloudFileStorageMVC.Models;

public class AddFileMetadataRequestModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SharingType { get; set; }
    public string PermissionLevel { get; set; }


    public AddFileMetadataRequestModel(string name, string description, string sharingType, string permissionLevel)
    {
        Name = name;
        Description = description;
        SharingType = sharingType;
        PermissionLevel = permissionLevel;
    }
}
