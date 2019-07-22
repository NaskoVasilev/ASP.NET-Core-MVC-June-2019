using Microsoft.ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SofiaPropertiesPricePredictionModelTrainer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!File.Exists(ModelBuilder.ModelFilePath))
            {
                ModelBuilder.CreateModel();
            }

            var testModelData = new List<ModelInput>
                                {
                                    new ModelInput
                                    {
                                        BuildingType = "3-СТАЕН",
                                        District = "град София, Лозенец",
                                        Floor = 6,
                                        TotalFloors = 6,
                                        Size = 100,
                                        Year = 2017,
                                        Type = "Тухла",
                                    },
                                    new ModelInput
                                    {
                                        BuildingType = "3-СТАЕН",
                                        District = "град София, Лозенец",
                                        Floor = 6,
                                        TotalFloors = 6,
                                        Size = 100,
                                        Year = 1980,
                                        Type = "Тухла",
                                    },
                                    new ModelInput
                                    {
                                        BuildingType = "3-СТАЕН",
                                        District = "град София, Овча купел",
                                        Floor = 6,
                                        TotalFloors = 6,
                                        Size = 100,
                                        Year = 2017,
                                        Type = "Тухла",
                                    },
                                    new ModelInput
                                    {
                                        BuildingType = "3-СТАЕН",
                                        District = "град София, Лозенец",
                                        Floor = 1,
                                        TotalFloors = 6,
                                        Size = 100,
                                        Year = 2017,
                                        Type = "Тухла",
                                    },
                                    new ModelInput
                                    {
                                        BuildingType = "3-СТАЕН",
                                        District = "град София, Лозенец",
                                        Floor = 6,
                                        TotalFloors = 6,
                                        Size = 60,
                                        Year = 2017,
                                        Type = "Тухла",
                                    },
                                };

            TestModel(ModelBuilder.ModelFilePath, testModelData);
        }

        private static void TestModel(string modelFile, IEnumerable<ModelInput> testModelData)
        {
            var context = new MLContext();
            var model = context.Model.Load(modelFile, out _);
            var predictionEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            foreach (var testData in testModelData)
            {
                var prediction = predictionEngine.Predict(testData);
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Input: {JsonConvert.SerializeObject(testData)}");
                Console.WriteLine($"Prediction: {prediction.Score}");
            }
        }
    }
}
