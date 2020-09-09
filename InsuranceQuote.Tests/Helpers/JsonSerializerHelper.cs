using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace InsuranceQuote.Tests.Helpers
{
    public static class JsonSerializerHelper
    {
        public static JsonSerializerOptions DefaultDeserialisationOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
