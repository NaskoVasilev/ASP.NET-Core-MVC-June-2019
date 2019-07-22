using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using SofiaPropertiesPricePrediction.Models;

namespace SofiaPropertiesPricePrediction.Controllers
{
    public class HomeController : Controller
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> predictionEngine;

        public HomeController(PredictionEnginePool<ModelInput, ModelOutput> predictionEngine)
        {
            this.predictionEngine = predictionEngine;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Predict(PropertyInputModel model)
        {
            var modelInput = new ModelInput
            {
                BuildingType = model.BuildingType,
                District = model.District,
                Floor = model.Floor,
                Size = model.Size,
                TotalFloors = model.TotalFloors,
                Type = model.Type,
                Year = model.Year
            };

            var modelOutput = predictionEngine.Predict(modelInput);

            var viewModel = new PropertyViewModel
            {
                BuildingType = model.BuildingType,
                District = model.District,
                Floor = model.Floor,
                Price = modelOutput.Score,
                Size = model.Size,
                TotalFloors = model.TotalFloors,
                Type = model.Type,
                Year = model.Year
            };

            return RedirectToAction(nameof(Result), viewModel);
        }

        public IActionResult Result(PropertyViewModel model)
        {
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
