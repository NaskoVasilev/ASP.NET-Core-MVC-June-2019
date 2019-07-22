using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Trainers.FastTree;
using CarPricePrediction.DataModels;

namespace MachineLearningML.ConsoleApp
{
    public static class ModelBuilder
    {
        public const string TrainDataFilePatj = @"Data\carsbg.csv";
        public const string ModelFilePath = @"../../../MLModels/MLModel.zip";

        public static void CreateModel()
        {
            MLContext mlContext = new MLContext(seed: 1);

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: TrainDataFilePatj,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            var dataProcessPipeline = mlContext.Transforms.Categorical.OneHotEncoding(
                new[] 
                {
                    new InputOutputColumnPair("Make", "Make"),
                    new InputOutputColumnPair("FuelType", "FuelType"),
                    new InputOutputColumnPair("Year", "Year"),
                    new InputOutputColumnPair("Gear", "Gear")
                })
                .Append(mlContext.Transforms.Categorical.OneHotHashEncoding(
                    new[] 
                    {
                        new InputOutputColumnPair("Model", "Model")
                    }))
                .Append(mlContext.Transforms.Concatenate("Features", new[] { "Make", "FuelType", "Year", "Gear", "Model", "HorsePower", "Range", "CubicCapacity" }));

            var trainer = mlContext.Regression.Trainers.FastTree(
                new FastTreeRegressionTrainer.Options()
                {
                    NumberOfLeaves = 57,
                    MinimumExampleCountPerLeaf = 1,
                    NumberOfTrees = 500,
                    LearningRate = 0.03438529f,
                    Shrinkage = 3.101394f,
                    LabelColumnName = "Price",
                    FeatureColumnName = "Features"
                });
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            mlContext.Model.Save(model, trainingDataView.Schema, ModelFilePath);
        }
    }
}
