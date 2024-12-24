using FluentValidation;

namespace FileMetadataAPI.Application.Features.Commands.Delete
{
    public class DeleteFileMetadataCommandValidator : AbstractValidator<DeleteFileMetadataCommand>
    {
        public DeleteFileMetadataCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");
        }
    }
}
