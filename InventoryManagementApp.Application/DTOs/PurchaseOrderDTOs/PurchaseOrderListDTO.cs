using InventoryManagementApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementApp.Application.DTOs.PurchaseOrderDTOs
{
    public class PurchaseOrderListDTO
    {
        public int ID { get; set; }
        public Status Status { get; set; }
        public decimal PurchaseOrderTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public CurrencyCodes Currency { get; set; }
        public int SupplierID { get; set; }
        public string? SupplierName { get; set; }
    }
}
