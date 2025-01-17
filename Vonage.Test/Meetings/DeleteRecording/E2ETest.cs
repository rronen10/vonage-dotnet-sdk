﻿using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Meetings.DeleteRecording;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Meetings.DeleteRecording
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task DeleteRecording()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/recordings/48a355bf-924d-4d4d-8e98-78575cf212dd")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.MeetingsClient.DeleteRecordingAsync(
                    DeleteRecordingRequest.Parse(new Guid("48a355bf-924d-4d4d-8e98-78575cf212dd")))
                .Should()
                .BeSuccessAsync();
        }
    }
}