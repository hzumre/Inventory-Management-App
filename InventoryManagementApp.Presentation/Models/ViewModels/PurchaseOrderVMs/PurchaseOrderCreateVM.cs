namespace InventoryManagementApp.Presentation.Models.ViewModels.PurchaseOrderVMs
{
    public class PurchaseOrderCreateVM
    {
		public decimal PurchaseOrderTotal { get; set; }
		public decimal TaxTotal { get; set; }
		public CurrencyCodes Currency { get; set; }
		public int SupplierID { get; set; }
	}
}
