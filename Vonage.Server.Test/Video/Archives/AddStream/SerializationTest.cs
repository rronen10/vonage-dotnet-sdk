﻿using System;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.AddStream;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.AddStream
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithCamelCase());

        [Fact]
        public void ShouldSerialize() =>
            AddStreamRequest
                .Build()
                .WithApplicationId(Guid.NewGuid())
                .WithArchiveId(Guid.NewGuid())
                .WithStreamId(Guid.Parse("12312312-3811-4726-b508-e41a0f96c68f"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithoutAudio() =>
            AddStreamRequest
                .Build()
                .WithApplicationId(Guid.NewGuid())
                .WithArchiveId(Guid.NewGuid())
                .WithStreamId(Guid.Parse("12312312-3811-4726-b508-e41a0f96c68f"))
                .DisableAudio()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithoutVideo() =>
            AddStreamRequest
                .Build()
                .WithApplicationId(Guid.NewGuid())
                .WithArchiveId(Guid.NewGuid())
                .WithStreamId(Guid.Parse("12312312-3811-4726-b508-e41a0f96c68f"))
                .DisableVideo()
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}