﻿using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.RemoveStreamFromBroadcast;
using Xunit;

namespace Vonage.Test.Video.Broadcast.RemoveStreamFromBroadcast
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly Guid streamId;
        private readonly Guid broadcastId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<Guid>();
            this.streamId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            RemoveStreamFromBroadcastRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenBroadcastIdIsEmpty() =>
            RemoveStreamFromBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(Guid.Empty)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeParsingFailure("BroadcastId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenStreamIdIsEmpty() =>
            RemoveStreamFromBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(Guid.Empty)
                .Create()
                .Should()
                .BeParsingFailure("StreamId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnSuccess_WithMandatoryValues() =>
            RemoveStreamFromBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ApplicationId.Should().Be(this.applicationId);
                    success.BroadcastId.Should().Be(this.broadcastId);
                    success.StreamId.Should().Be(this.streamId);
                });
    }
}