using FluentValidation;

namespace FileMetadataAPI.Application.Features.Commands.Add;

public class AddFileMetadataCommandValidation : AbstractValidator<AddFileMetadataRequest>
{
    public AddFileMetadataCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.SharingType)
            .NotEmpty().WithMessage("Sharing type is required.");
    }
}
