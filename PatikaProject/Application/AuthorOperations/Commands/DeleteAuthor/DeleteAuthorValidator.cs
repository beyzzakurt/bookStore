using FluentValidation;

namespace PatikaProject.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorValidator : AbstractValidator<DeleteAuthor>
    {
        public DeleteAuthorValidator() 
        {
            RuleFor(command => command.AuthorId).NotEmpty().GreaterThan(0);
        }
    }
}
