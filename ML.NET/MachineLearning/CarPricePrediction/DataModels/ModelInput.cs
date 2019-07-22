using Microsoft.ML.Data;

namespace CarPricePrediction.DataModels
{
    public class ModelInput
    {
        [ColumnName("Make"), LoadColumn(0)]
        public string Make { get; set; }


        [ColumnName("Model"), LoadColumn(1)]
        public string Model { get; set; }


        [ColumnName("FuelType"), LoadColumn(2)]
        public string FuelType { get; set; }


        [ColumnName("Year"), LoadColumn(3)]
        public string Year { get; set; }


        [ColumnName("HorsePower"), LoadColumn(4)]
        public float HorsePower { get; set; }


        [ColumnName("Range"), LoadColumn(5)]
        public float Range { get; set; }


        [ColumnName("Gear"), LoadColumn(6)]
        public string Gear { get; set; }


        [ColumnName("Price"), LoadColumn(7)]
        public float Price { get; set; }


        [ColumnName("CubicCapacity"), LoadColumn(8)]
        public float CubicCapacity { get; set; }


    }
}
