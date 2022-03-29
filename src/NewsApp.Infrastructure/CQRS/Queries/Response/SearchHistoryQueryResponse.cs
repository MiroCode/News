﻿using System;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class SearchHistoryQueryResponse
    {
        public string Id { get; set; }
        public string SearchText { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
