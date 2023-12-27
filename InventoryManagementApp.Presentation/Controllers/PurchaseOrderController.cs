using AutoMapper;
using InventoryManagementApp.Application.DTOs.PurchaseOrderDTOs;
using InventoryManagementApp.Application.Services.PurchaseOrderService;
using InventoryManagementApp.Application.Services.SupplierService;
using InventoryManagementApp.Presentation.Models.ViewModels.InventoryVMs;
using InventoryManagementApp.Presentation.Models.ViewModels.PurchaseOrderVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementApp.Presentation.Controllers
{
    //[Authorize(Roles = "Admin", "Manager", "Employee")]

    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        IWebHostEnvironment _webHostEnvironment;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IMapper mapper, IWebHostEnvironment webHostEnvironment, ISupplierService supplierService)
        {
            _purchaseOrderService = purchaseOrderService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _supplierService = supplierService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Listing only active purchaseOrders
        [Route("[controller]/ActiveList")]
        public async Task<IActionResult> GetAllActivePurchaseOrders()
        {
            var purchaseOrderListDTO = await _purchaseOrderService.GetDefaults(x => x.Status == Domain.Enums.Status.Active);
            var purchaseOrderListVM = _mapper.Map<List<PurchaseOrderListVM>>(purchaseOrderListDTO);

            return View(purchaseOrderListVM);
        }


        //Listing all purchaseOrders
        [Route("[controller]/List")]
        public async Task<IActionResult> GetAllPurchaseOrders()
        {
            List<PurchaseOrderListVM> purchaseOrdersList = _mapper.Map<List<PurchaseOrderListVM>>(await _purchaseOrderService.All());
            foreach (var po in purchaseOrdersList)
            {
                po.SupplierName = await _supplierService.GetNameById(po.SupplierID);
            }
            return View(purchaseOrdersList);
        }

        //Adding purchaseOrder
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PurchaseOrderCreateVM purchaseOrderCreateVm = new PurchaseOrderCreateVM();
            ViewBag.Suppliers = await GetSuppliers();
            return View(purchaseOrderCreateVm);

        }


        [HttpPost]
        public async Task<IActionResult> Create(PurchaseOrderCreateVM purchaseOrderCreateVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var purchaseOrderCreateDto = _mapper.Map<PurchaseOrderCreateDTO>(purchaseOrderCreateVm);
                    await _purchaseOrderService.Create(purchaseOrderCreateDto);
                    return RedirectToAction("GetAllPurchaseOrders");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            return View(purchaseOrderCreateVm);
        }



        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _purchaseOrderService.Delete(id);
                return RedirectToAction("GetAllPurchaseOrders");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GetAllPurchaseOrders");
            }
        }



        [HttpGet]
        public async Task<IActionResult> UpdateDetails(int id)
        {
            if (await _purchaseOrderService.GetById(id) == null)
            {
                return NotFound();
            }
            else
            {
                PurchaseOrderUpdateVM purchaseOrderUpdateVm = _mapper.Map<PurchaseOrderUpdateVM>(await _purchaseOrderService.GetById(id));
                return View(purchaseOrderUpdateVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetails(PurchaseOrderUpdateVM vm)
        {

            var purchaseOrdersUpdateDto = _mapper.Map<PurchaseOrderUpdateDTO>(vm);
            await _purchaseOrderService.Update(purchaseOrdersUpdateDto);
            return RedirectToAction("GetAllPurchaseOrders");
        }

        private async Task<SelectList> GetSuppliers()
        {
            var suppliersList = await _supplierService.All();
            var supplierItems = suppliersList.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.Name
            }).ToList();

            return new SelectList(supplierItems, "Value", "Text");
        }

    }
}

