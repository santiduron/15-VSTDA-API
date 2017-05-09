using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using VehicleManager.API.Data;
using VehicleManager.API.Models;

namespace VehicleManager.API.Controllers
{
    public class VehiclesController : ApiController
    {
        private VehicleManagerDataContext db = new VehicleManagerDataContext();

		// GET: api/Vehicles
		public IHttpActionResult GetVehicles()
		{
			var resultSet = db.Vehicles.Select(vehicle => new
			{
				vehicle.VehicleId,
				vehicle.Make,
				vehicle.Model,
				vehicle.Year,
				vehicle.Color,
				vehicle.VehicleType,
				vehicle.RetailPrice
			});
			return Ok(resultSet);
        }

        // GET: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult GetVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(new
			{
				vehicle.VehicleId,
				vehicle.Make,
				vehicle.Model,
				vehicle.Year,
				vehicle.Color,
				vehicle.VehicleType,
				vehicle.RetailPrice
			});
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }
			// Grab the customer from the database
			var dbVehicle = db.Vehicles.Find(id);

			// Manually update each property
			dbVehicle.Color = vehicle.Color;
			dbVehicle.Make = vehicle.Make;
			dbVehicle.Model = vehicle.Model;
			dbVehicle.RetailPrice = vehicle.RetailPrice;
			dbVehicle.VehicleType = vehicle.VehicleType;
			dbVehicle.Year = vehicle.Year;

			db.Entry(dbVehicle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vehicles
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vehicle.VehicleId }, new
			{
				vehicle.VehicleId,
				vehicle.Make,
				vehicle.Model,
				vehicle.Year,
				vehicle.Color,
				vehicle.VehicleType,
				vehicle.RetailPrice
			});
        }

        // DELETE: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            return Ok(new
			{
				vehicle.VehicleId,
				vehicle.Make,
				vehicle.Model,
				vehicle.Year,
				vehicle.Color,
				vehicle.VehicleType,
				vehicle.RetailPrice
			});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehicleExists(int id)
        {
            return db.Vehicles.Count(e => e.VehicleId == id) > 0;
        }
    }
}