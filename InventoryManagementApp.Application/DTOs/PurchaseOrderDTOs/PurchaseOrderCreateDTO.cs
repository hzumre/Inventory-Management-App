using InventoryManagementApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementApp.Application.DTOs.PurchaseOrderDTOs
{
    public class PurchaseOrderCreateDTO
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public Status Status { get; set; } = Status.Active;
		public string? CreatedBy { get; set; }
		public decimal PurchaseOrderTotal { get; set; }
		public decimal TaxTotal { get; set; }
		public CurrencyCodes Currency { get; set; }
		public int SupplierID { get; set; }
	}
}
