using System;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdatedBookCommandValidation : AbstractValidator<UpdatedBookCommand>
    {
        public UpdatedBookCommandValidation()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
        }
    }
}