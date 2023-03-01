﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Server.Video.Archives;
using Vonage.Server.Video.Archives.DeleteArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.DeleteArchive
{
    public class DeleteArchiveTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new ArchiveClient(configuration).DeleteArchiveAsync(this.request);

        private readonly Result<DeleteArchiveRequest> request;

        public DeleteArchiveTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<DeleteArchiveRequest, Unit>(
                (configuration, failureRequest) =>
                    new ArchiveClient(configuration).DeleteArchiveAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
            };

        private static Result<DeleteArchiveRequest> BuildRequest(ISpecimenBuilder fixture) =>
            DeleteArchiveRequest.Parse(fixture.Create<Guid>(), fixture.Create<Guid>());
    }
}