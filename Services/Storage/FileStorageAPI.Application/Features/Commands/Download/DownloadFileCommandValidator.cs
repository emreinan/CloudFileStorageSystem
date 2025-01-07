using FluentValidation;

namespace FileStorageAPI.Application.Features.Commands.Download
{
    public class DownloadFileCommandValidator : AbstractValidator<DownloadFileDto>
    {
        public DownloadFileCommandValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("FileName is required")
                .MinimumLength(1).Must(x => x.Contains('.')).WithMessage("FileName must contain '.' ");
        }
    }
    
}
