using FluentValidation;

namespace PatikaProject.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator() 
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}
