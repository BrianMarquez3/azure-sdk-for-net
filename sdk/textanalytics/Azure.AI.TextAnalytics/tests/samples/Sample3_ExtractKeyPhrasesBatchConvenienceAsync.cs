﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core.TestFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.AI.TextAnalytics.Samples
{
    [LiveOnly]
    public partial class TextAnalyticsSamples
    {
        [Test]
        public async Task ExtractKeyPhrasesBatchConvenienceAsync()
        {
            string endpoint = TestEnvironment.Endpoint;
            string apiKey = TestEnvironment.ApiKey;

            // Instantiate a client that will be used to call the service.
            var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

            var documents = new List<string>
            {
                "Microsoft was founded by Bill Gates and Paul Allen.",
                "Text Analytics is one of the Azure Cognitive Services.",
                "My cat might need to see a veterinarian.",
            };

            ExtractKeyPhrasesResultCollection results = await client.ExtractKeyPhrasesBatchAsync(documents);

            Console.WriteLine($"Extracted key phrases for each document are:");
            int i = 0;
            foreach (ExtractKeyPhrasesResult result in results)
            {
                Console.WriteLine($"For document: \"{documents[i++]}\",");
                Console.WriteLine($"the following {result.KeyPhrases.Count()} key phrases were found: ");

                foreach (string keyPhrase in result.KeyPhrases)
                {
                    Console.WriteLine($"    {keyPhrase}");
                }
            }
        }
    }
}
