using FluentValidation;

namespace FileStorageAPI.Application.Features.Commands.Delete
{
    public class DeleteFileCommandValidator : AbstractValidator<DeleteFileDto>
    {
        public DeleteFileCommandValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("FileName is required")
                .MinimumLength(1).Must(x => x.Contains('.')).WithMessage("FileName must contain '.' ");
        }
    }

}
