﻿using System;
using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateRoom
{
    public class UpdateRoomRequestTest
    {
        private readonly Guid roomId;

        public UpdateRoomRequestTest()
        {
            var fixture = new Fixture();
            this.roomId = fixture.Create<Guid>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            UpdateRoomRequestBuilder
                .Build(this.roomId)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/beta/meetings/rooms/{this.roomId}");
    }
}