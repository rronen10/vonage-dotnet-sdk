﻿using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.CreateArchive;
using Xunit;

namespace Vonage.Test.Video.Archives.CreateArchive
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive");
    }
}