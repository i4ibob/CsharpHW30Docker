using CsharpHW30Docker.Models;
using Microsoft.AspNetCore.Mvc;

namespace CsharpHW30Docker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private static List<Car> cars = new List<Car>
        {
            new Car { Id = 1, Name = "Nissan Silvia s15", Description = "1999", Price = 20 },
            new Car { Id = 2, Name = "BMW e36", Description = "2004", Price = 25 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCars()
        {
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetCar(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        [HttpPost]
        public ActionResult<Car> CreateCar([FromBody] Car car)
        {
            if (car == null) return BadRequest();

            car.Id = cars.Max(b => b.Id) + 1;
            cars.Add(car);
            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCar(int id, [FromBody] Car car)
        {
            if (car == null || id != car.Id) return BadRequest();

            var existingCar = cars.FirstOrDefault(b => b.Id == id);
            if (existingCar == null) return NotFound();

            existingCar.Name = car.Name;
            existingCar.Description = car.Description;
            existingCar.Price = car.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var car = cars.FirstOrDefault(b => b.Id == id);
            if (car == null) return NotFound();

            cars.Remove(car);
            return NoContent();
        }
    }
}
