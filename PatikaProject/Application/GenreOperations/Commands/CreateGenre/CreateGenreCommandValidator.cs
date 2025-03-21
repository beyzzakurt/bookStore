﻿using FluentValidation;

namespace PatikaProject.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidator :AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator() 
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}
