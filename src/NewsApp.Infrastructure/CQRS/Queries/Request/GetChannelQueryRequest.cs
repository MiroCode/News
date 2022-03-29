﻿using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetChannelQueryRequest : IRequest<ChannelQueryResponse>
    {
        public string Id { get; set; }
    }
}