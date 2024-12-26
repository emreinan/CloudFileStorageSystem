using FluentValidation;

namespace FileMetadataAPI.Application.Features.FileMetadata.Commands.Update
{
    public class UpdateFileMetadataCommandValidator : AbstractValidator<UpdateFileMetadataRequest>
    {
        public UpdateFileMetadataCommandValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }
    }
}
