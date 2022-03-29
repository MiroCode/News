﻿using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetUserInterestQueryRequest : IRequest<UserInterestQueryResponse>
    {
        public string Id { get; set; }
    }
}
