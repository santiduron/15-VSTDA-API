using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManager.API.Models
{
	public class Vehicle
	{
		// Scalar Properties
		public int VehicleId { get; set; }


		public string Make { get; set; }

		public string Model { get; set; }

		public int Year { get; set; }

		public string Color { get; set; }

		public string VehicleType { get; set; }

		public decimal RetailPrice { get; set; }

		// Navigation Properties

		public virtual ICollection<Sale> Sales { get; set; }
	}
}