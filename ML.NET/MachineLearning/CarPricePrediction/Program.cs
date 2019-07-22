using System;
using System.IO;
using Microsoft.ML;
using MachineLearningML.Model.DataModels;
using CarPricePrediction.DataModels;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MachineLearningML.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(ModelBuilder.ModelFilePath))
            {
                ModelBuilder.CreateModel();
            }

            var testData = new List<ModelInput>()
            {
                new ModelInput
                {
                    CubicCapacity = 2000,
                    FuelType = "Дизел",
                    Gear = "Ръчни",
                    HorsePower = 140,
                    Make = "Audi",
                    Model = "A4",
                    Range = 181000,
                    Year = "12/02/2007"
                },
                new ModelInput
                {
                    CubicCapacity = 2200,
                    FuelType = "Дизел",
                    Gear = "Ръчни",
                    HorsePower = 150,
                    Make = "Mercedes-Benz",
                    Model = "C 220",
                    Range = 190000,
                    Year = "12/02/2005"
                },
                new ModelInput
                {
                    CubicCapacity = 1900,
                    FuelType = "Дизел",
                    Gear = "Ръчни",
                    HorsePower = 150,
                    Make = "Аlfa Romeo",
                    Model = "159",
                    Range = 150000,
                    Year = "12/02/2007"
                }
            };

            MLContext mlContext = new MLContext();

            ITransformer mlModel = mlContext.Model.Load(ModelBuilder.ModelFilePath, out DataViewSchema inputSchema);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            foreach (var data in testData)
            {
                ModelOutput predictionResult = predictionEngine.Predict(data);
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(JsonConvert.SerializeObject(data));
                Console.WriteLine($"Price: {predictionResult.Score}");
            }
        }
    }
}
