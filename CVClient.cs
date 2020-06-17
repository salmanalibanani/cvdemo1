using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

public class CVClient {
    public static ComputerVisionClient Authenticate(string endpoint, string key)
    {
        ComputerVisionClient client =
        new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
        { Endpoint = endpoint };
        return client;
    }

    public static async Task AnalyzeImageUrl(ComputerVisionClient client, string imageUrl)
    {
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("ANALYZE IMAGE - URL");
        Console.WriteLine();

        // Creating a list that defines the features to be extracted from the image. 
        List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
        {
        VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
        VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
        VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
        VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
        VisualFeatureTypes.Objects
        };

        Console.WriteLine($"Analyzing the image {Path.GetFileName(imageUrl)}...");
        Console.WriteLine();
        ImageAnalysis results = await client.AnalyzeImageAsync(imageUrl, features);

        Console.WriteLine("Summary:");
        foreach (var caption in results.Description.Captions)
        {
            Console.WriteLine($"{caption.Text} with confidence {caption.Confidence}");
        }

        Console.WriteLine("Categories:");
        foreach (var category in results.Categories)
        {
            Console.WriteLine($"{category.Name} with confidence {category.Score}");
        }

        Console.WriteLine("Tags:");
        foreach (var tag in results.Tags)
        {
            Console.WriteLine($"{tag.Name} {tag.Confidence}");
        }

        Console.WriteLine("Objects:");
        foreach (var obj in results.Objects)
        {
            Console.WriteLine($"{obj.ObjectProperty} with confidence {obj.Confidence} at location {obj.Rectangle.X}, " +
            $"{obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y}, {obj.Rectangle.Y + obj.Rectangle.H}");
        }

        Console.WriteLine();
    }
}
