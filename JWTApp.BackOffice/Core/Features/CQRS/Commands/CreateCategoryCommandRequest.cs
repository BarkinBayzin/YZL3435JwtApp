﻿using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Commands
{
    public class CreateCategoryCommandRequest : IRequest
    {
        public string Definition { get; set; }
    }
}
