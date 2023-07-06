﻿using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Request;
using Vonage.Test.Unit.TestHelpers;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification
{
    [Trait("Category", "E2E")]
    public class E2ETest
    {
        private readonly E2EHelper helper;
        private readonly SerializationTestHelper serialization;

        public E2ETest()
        {
            this.helper = new E2EHelper(
                "Vonage.Url.Api",
                Credentials.FromAppIdAndPrivateKey(Guid.NewGuid().ToString(),
                    Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey")));
            this.serialization = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());
        }

        [Fact]
        public async Task StartEmailVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeEmailWorkflow));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(EmailWorkflow.Parse("alice@company.com"))
                .Create());
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartSilentAuthVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeSilentAuthWorkflow));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SilentAuthWorkflow.Parse("447700900000"))
                .Create());
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartSmsVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeSmsWorkflow));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SmsWorkflow.Parse("447700900000"))
                .Create());
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartVerificationWithFallbackWorkflows()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeFallbackWorkflows));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .WithFallbackWorkflow(WhatsAppWorkflow.Parse("447700900000"))
                .WithFallbackWorkflow(VoiceWorkflow.Parse("447700900000"))
                .Create());
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartVoiceVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeVoiceWorkflow));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(VoiceWorkflow.Parse("447700900000"))
                .Create());
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartWhatsAppInteractiveVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeWhatsAppInteractiveWorkflow));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .Create());
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartWhatsAppVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeWhatsAppWorkflow));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppWorkflow.Parse("447700900000"))
                .Create());
            VerifyResponseBody(result);
        }

        private void InitializeWireMock(string bodyScenario) =>
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(bodyScenario))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));

        private static void VerifyResponseBody(Result<StartVerificationResponse> response) =>
            response.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
    }
}