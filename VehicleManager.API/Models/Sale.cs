using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManager.API.Models
{
	public class Sale
	{
		// Scalar Properties

		public int SaleId { get; set; }

		public int VehicleId { get; set; }

		public int CustomerId { get; set; }

		public decimal SalePrice { get; set; }

		public DateTime? InvoiceDate { get; set; }

		public DateTime? PaymentReceivedDate { get; set; }
			
		// Navigation Properties

		public virtual Vehicle Vehicle { get; set; }

		public virtual Customer Customer { get; set; }
	}
}