﻿using FluentValidation;

namespace AuthenticationAPI.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<UserLoginDto>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
