using FluentValidation;

namespace PatikaProject.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthor>
    {
        public CreateAuthorValidator() 
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Birthday).NotEmpty().LessThan(DateTime.UtcNow);
        }
    }
}
