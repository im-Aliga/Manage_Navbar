using DemoApplication.ViewModels.Admin.Navbar;
using FluentValidation;

namespace DemoApplication.Validators.Admin.Navbar.Add
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(n => n.Title)
                .NotNull()
                .WithMessage("Title can't be empty")
                .NotEmpty()
                .WithMessage("Title can't be empty")
                .MinimumLength(1)
                .WithMessage("Minimum length should be 10")
                .MaximumLength(15)
                .WithMessage("Maximum length should be 45");

            RuleFor(n => n.Order)
                .NotNull()
                .WithMessage("Order can't be empty")
                .NotEmpty()
                .WithMessage("Order can't be empty");

            RuleFor(n => n.Url)
              .NotNull()
                .WithMessage("Url can't be empty")
                .NotEmpty()
                .WithMessage("Url can't be empty")
                .MinimumLength(1)
                .WithMessage("Minimum length should be 10")
                .MaximumLength(200)
                .WithMessage("Maximum length should be 45");


        }
    }
}
