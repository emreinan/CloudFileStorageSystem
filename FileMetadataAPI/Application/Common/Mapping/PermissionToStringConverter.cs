using AutoMapper;
using FileMetadataAPI.Domain.Enums;
using System.Security;

namespace FileMetadataAPI.Application.Common.Mapping;

public class PermissionToStringConverter : ITypeConverter<SharingType, string>
{
    public string Convert(SharingType source, string destination, ResolutionContext context)
    {
        return source.ToString();
    }
}