using AutoMapper;
using FileMetadataAPI.Domain.Enums;

namespace FileMetadataAPI.Application.Common.Maping;

public class StringToPermissionConverter : ITypeConverter<string, PermissionLevel>
{
    public PermissionLevel Convert(string source, PermissionLevel destination, ResolutionContext context)
    {
        if (Enum.TryParse<PermissionLevel>(source, true, out var permission))
        {
            return permission;
        }
        throw new ArgumentException($"Geçersiz izin türü: {source}");
    }
}

