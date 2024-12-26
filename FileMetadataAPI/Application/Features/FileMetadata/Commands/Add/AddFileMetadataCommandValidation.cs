using FluentValidation;

namespace FileMetadataAPI.Application.Features.FileMetadata.Commands.Add;

public class AddFileMetadataCommandValidation : AbstractValidator<AddFileMetadataCommand>
{
    public AddFileMetadataCommandValidation()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("OwnerId is required.")
            .GreaterThan(0).WithMessage("OwnerId must be greater than 0.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.UploadDate)
            .NotEmpty().WithMessage("UploadDate is required.");
    }
}
