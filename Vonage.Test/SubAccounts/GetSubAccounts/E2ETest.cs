﻿using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.SubAccounts.GetSubAccounts
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetSubAccount()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/accounts/790fc5e5/subaccounts")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.SubAccountsClient.GetSubAccountsAsync()
                .Should()
                .BeSuccessAsync(success =>
                {
                    success.PrimaryAccount.Should().Be(SerializationTest.GetExpectedPrimaryAccount());
                    success.SubAccounts.Should().BeEquivalentTo(SerializationTest.GetExpectedSubAccounts());
                });
        }
    }
}