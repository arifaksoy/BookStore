using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidation : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidation()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
        
    }
}