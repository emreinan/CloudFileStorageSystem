using FluentValidation;

namespace FileMetadataAPI.Application.Features.Share.Commands.Update
{
    public class UpdateFileShareCommandValidator : AbstractValidator<UpdateFileShareDto>
    {
        public UpdateFileShareCommandValidator()
        {
            RuleFor(x => x.Permission)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }

    }
}
