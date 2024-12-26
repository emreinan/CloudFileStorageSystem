using FluentValidation;

namespace FileMetadataAPI.Application.Features.Share.Commands.Create
{
    public class CreateFileShareCommandValidator : AbstractValidator<CreateFileShareDto>
    {
        public CreateFileShareCommandValidator()
        {
            RuleFor(x => x.FileId)
                .NotEmpty().WithMessage("FileId is required.");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Permission)
                .NotEmpty().WithMessage("Permission is required.");
        }
    }
}
