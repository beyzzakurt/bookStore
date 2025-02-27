using FluentValidation;

namespace PatikaProject.Application.AuthorOperations.Queries.GetAuthorById
{
    public class GetAuthorByIdValidator : AbstractValidator<GetAuthorById>
    {
        public GetAuthorByIdValidator()
        {
            RuleFor(query => query.AuthorId).GreaterThan(0);
        }
    }
}
