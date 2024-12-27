using AutoMapper;
using FileMetadataAPI.Domain.Enums;

namespace FileMetadataAPI.Application.Common.Maping;

public class PermissionToStringConverter : ITypeConverter<Permission, string>
{
    public string Convert(Permission source, string destination, ResolutionContext context)
    {
        return source.ToString();
    }
}

