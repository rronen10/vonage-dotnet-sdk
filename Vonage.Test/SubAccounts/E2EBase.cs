﻿using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;

namespace Vonage.Test.SubAccounts
{
    public abstract class E2EBase
    {
        protected E2EBase(string serializationNamespace)
        {
            this.Helper = TestingContext.WithBasicCredentials("Vonage.Url.Api");
            this.Serialization =
                new SerializationTestHelper(serializationNamespace, JsonSerializerBuilder.BuildWithSnakeCase());
        }

        internal readonly TestingContext Helper;
        internal readonly SerializationTestHelper Serialization;
    }
}