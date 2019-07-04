using CarsApi.Data;
using CarsApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Controllers
{
	public class CarsController : ApiController
	{
		private readonly ApplicationDbContext context;

		public CarsController(ApplicationDbContext context)
		{
			this.context = context;
		}

		public ActionResult<IEnumerable<Car>> Get()
		{
			var cars = context.Cars.ToList();
			return cars;
		}

		[HttpGet("{id}")]
		public ActionResult<Car> Get(int id)
		{
			var car = context.Cars.FirstOrDefault(c => c.Id == id);
			if (car == null)
			{
				return NotFound($"Car with id {id} was not found.");
			}
			return car;
		}

		[HttpPost]
		public async Task<ActionResult<Car>> Post(Car car)
		{
			await context.Cars.AddAsync(car);
			await context.SaveChangesAsync();
			return CreatedAtAction(nameof(Get), new { id = car.Id }, car);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, Car car)
		{
			var carFromDb = context.Cars.FirstOrDefault(c => c.Id == id);
			if(carFromDb == null)
			{
				return NotFound();
			}

			carFromDb.Model = car.Model;
			carFromDb.Price = car.Price;
			carFromDb.Year = car.Year;
			await context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Car>> Delete(int id)
		{
			var car = context.Cars.FirstOrDefault(c => c.Id == id);
			if(car == null)
			{
				return NotFound();
			}
			context.Cars.Remove(car);
			await context.SaveChangesAsync();

			return car;
		}
	}
}
