using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VehicleManager.API.Models;

namespace VehicleManager.API.Data
{
	public class VehicleManagerDataContext : DbContext
	{
		//Constructor
		// Tells entity framework that during migrations
		//	our database should be called VehicleManager
		public VehicleManagerDataContext() : base("VehicleManager")
		{

		}


		// DbSets
		public IDbSet<Customer> Customers { get; set; }

		public IDbSet<Sale> Sales { get; set; }

		public IDbSet<Vehicle> Vehicles { get; set; }

		// Model Configuration
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Vehicle has many sales

			modelBuilder.Entity<Vehicle>()
				.HasMany(vehicle => vehicle.Sales)
				.WithRequired(sale => sale.Vehicle)
				.HasForeignKey(sale => sale.VehicleId);


			// Customer has many sales
			modelBuilder.Entity<Customer>()
				.HasMany(customer => customer.Sales)
				.WithRequired(sale => sale.Customer)
				.HasForeignKey(sale => sale.CustomerId);
		}
	}
}
	