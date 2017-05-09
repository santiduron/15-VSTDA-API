namespace VehicleManager.API.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using VehicleManager.API.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<VehicleManager.API.Data.VehicleManagerDataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(VehicleManager.API.Data.VehicleManagerDataContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			string[] colors = new string[] { "Green", "Red", "Yellow", "Hot Pink", "Black", "White" };
			string[] makes = new string[] { "Honda", "Toyota", "Ford", "Accura", "Nissan" };
			string[] models = new string[] { "Standard", "Extra-Luxurious", "Luxurious", "Sport" };
			string[] vehicleTypes = new string[] { "Sedan", "SUV", "Cross-over", "Coupe" };


			if (context.Customers.Count() == 0)
			{
				for (int i = 0; i < 1000; i++)
				{
					context.Customers.Add(new Models.Customer
					{
						EmailAddress = Faker.InternetFaker.Email(),
						DateOfBirth = Faker.DateTimeFaker.BirthDay(),
						FirstName = Faker.NameFaker.FirstName(),
						LastName = Faker.NameFaker.LastName(),
						Telephone = Faker.PhoneFaker.Phone()
					});
				}

				context.SaveChanges();
			}

			if (context.Vehicles.Count() == 0)
			{
				for (int i = 0; i < 100; i++)
				{
					context.Vehicles.Add(new Vehicle
					{
						Model = Faker.ArrayFaker.SelectFrom(models),
						Make = Faker.ArrayFaker.SelectFrom(makes),
						Color = Faker.ArrayFaker.SelectFrom(colors),
						VehicleType = Faker.ArrayFaker.SelectFrom(vehicleTypes),
						RetailPrice = Faker.NumberFaker.Number(15000, 30000),
						Year = Faker.DateTimeFaker.DateTime().Year
					});
				}
				context.SaveChanges();
			}

			if (context.Sales.Count() == 0)
			{
				for (int i = 0; i < 100; i++)
				{
					var vehicle = context.Vehicles.Find(Faker.NumberFaker.Number(1,100));
					var invoiceDate = Faker.DateTimeFaker.DateTime();

					context.Sales.Add(new Sale
					{
						Customer = context.Customers.Find(Faker.NumberFaker.Number(1,100)),
						Vehicle = vehicle,
						InvoiceDate = invoiceDate,
						SalePrice = vehicle.RetailPrice,
						PaymentReceivedDate = invoiceDate.AddDays(Faker.NumberFaker.Number(1, 14))
					});
				}
				context.SaveChanges();


			}
		}

	}
}


