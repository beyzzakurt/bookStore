using FluentValidation;

namespace PatikaProject.BookOperations.GetBookById
{
    public class GetByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetByIdQueryValidator() 
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
