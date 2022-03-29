﻿using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class ListChannelQueryRequest : IRequest<IEnumerable<ListChannelQueryResponse>>
    {
        public string Name { get; set; }
    }
}