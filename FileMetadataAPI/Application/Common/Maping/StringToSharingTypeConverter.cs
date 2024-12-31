using AutoMapper;
using FileMetadataAPI.Domain.Enums;

namespace FileMetadataAPI.Application.Common.Maping;

public class StringToSharingTypeConverter : ITypeConverter<string, SharingType>
{
    public SharingType Convert(string source, SharingType destination, ResolutionContext context)
    {
        if (Enum.TryParse<SharingType>(source, true, out var permission))
        {
            return permission;
        }
        throw new ArgumentException($"Geçersiz izin türü: {source}");
    }
}

