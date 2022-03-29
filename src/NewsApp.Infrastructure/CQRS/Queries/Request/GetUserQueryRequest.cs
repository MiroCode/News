﻿using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetUserQueryRequest : IRequest<UserQueryResponse>
    {
        public string Id { get; set; }
    }
}
