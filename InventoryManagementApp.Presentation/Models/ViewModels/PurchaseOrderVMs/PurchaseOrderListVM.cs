using InventoryManagementApp.Domain.Enums;

namespace InventoryManagementApp.Presentation.Models.ViewModels.PurchaseOrderVMs
{
    public class PurchaseOrderListVM
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
