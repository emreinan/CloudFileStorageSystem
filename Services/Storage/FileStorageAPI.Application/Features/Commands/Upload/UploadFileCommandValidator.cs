using FluentValidation;

namespace FileStorageAPI.Application.Features.Commands.Upload;

public class UploadFileCommandValidator : AbstractValidator<UploadFileDto>
{
    public UploadFileCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.")
            .Must(x => x.Length > 0).WithMessage("File is empty.");

        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("Description must not exceed 100 characters.");

    }
}
