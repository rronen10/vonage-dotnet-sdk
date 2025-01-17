﻿using Vonage.Serialization;
using Vonage.SubAccounts.TransferAmount;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SubAccounts.TransferAmount
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldSerialize() =>
            TransferAmountRequest.Build()
                .WithFrom("7c9738e6")
                .WithTo("ad6dc56f")
                .WithAmount((decimal) 123.45)
                .WithReference("This gets added to the audit log")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}