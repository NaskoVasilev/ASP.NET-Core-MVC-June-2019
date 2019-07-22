using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;

namespace SofiaPropertiesPricePredictionModelTrainer
{
    public static class ModelBuilder
    {
        public const string TrainDataFilePath = "Data/imot.bg-2019-07-06.csv";
        public const string ModelFilePath = "../../../MLModels/MLModel.zip";

        public static void CreateModel()
        {
            MLContext mlContext = new MLContext();

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: TrainDataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            var dataProcessPipeline = mlContext.Transforms.Categorical.OneHotEncoding(
                new[]
                {
                    new InputOutputColumnPair("District", "District"),
                    new InputOutputColumnPair("Type", "Type"),
                    new InputOutputColumnPair("BuildingType", "BuildingType")
                })
                .Append(mlContext.Transforms.Concatenate("Features", new[] { "District", "Type", "BuildingType", "Size", "Floor", "TotalFloors", "Year" }));

            FastTreeTweedieTrainer trainer = mlContext.Regression.Trainers.FastTreeTweedie(new FastTreeTweedieTrainer.Options() { NumberOfLeaves = 20, MinimumExampleCountPerLeaf = 10, NumberOfTrees = 500, LearningRate = 0.07684207f, Shrinkage = 1.057825f, LabelColumnName = "Price", FeatureColumnName = "Features" });

            EstimatorChain<RegressionPredictionTransformer<FastTreeTweedieModelParameters>> trainingPipeline = dataProcessPipeline.Append(trainer);

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            mlContext.Model.Save(model, trainingDataView.Schema, ModelFilePath);
        }
    }
}
