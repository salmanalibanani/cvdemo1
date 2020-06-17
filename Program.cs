using System;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace SalmanCVDemo
{
    class Program
    {
        static string subscriptionKey = Environment.GetEnvironmentVariable("COMPUTER_VISION_SUBSCRIPTION_KEY");
        static string endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT");
        private const string ANALYZE_URL_IMAGE = "https://moderatorsampleimages.blob.core.windows.net/samples/sample16.png";

        static void Main(string[] args)
        {
            var client = CVClient.Authenticate(endpoint, subscriptionKey);
            CVClient.AnalyzeImageUrl(client, ANALYZE_URL_IMAGE).Wait();
        }
    }
}

