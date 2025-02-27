using FluentValidation;

namespace PatikaProject.Application.BookOperations.Queries.GetBookById
{
    public class GetByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
