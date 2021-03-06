﻿using MobileApiGateway.Services.ObjectCommentServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApiGateway.Services.ObjectServices
{
    public class UpstreamObjectDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CountOfViews { get; set; }

        public int CountOfImpressions { get; set; }

        public float? Rating { get; set; }

        public string OwnerId { get; set; }

        public List<string> Photos { get; set; }

        public List<string> Tags { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionType Type { get; set; }

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public bool IsLikedByMe { get; set; }

        public List<UpstreamCommentDto> Comments { get; set; }

    }
}
