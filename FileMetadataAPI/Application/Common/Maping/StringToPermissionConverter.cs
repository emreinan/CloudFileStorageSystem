using AutoMapper;
using FileMetadataAPI.Domain.Enums;

namespace FileMetadataAPI.Application.Common.Maping;

public class StringToPermissionConverter : ITypeConverter<string, Permission>
{
    public Permission Convert(string source, Permission destination, ResolutionContext context)
    {
        if (Enum.TryParse<Permission>(source, true, out var permission))
        {
            return permission;
        }
        throw new ArgumentException($"Geçersiz izin türü: {source}");
    }
}
