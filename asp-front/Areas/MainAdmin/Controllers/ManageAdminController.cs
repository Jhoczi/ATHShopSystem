using asp_front.Models;
using ath_server.Interfaces;
using ath_server.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace asp_front.Areas.MainAdmin.Controllers;

[Authorize(Roles = "Admin")]
[Area("MainAdmin")]
public class ManageAdminController : Controller
{
    private readonly IMapper _mapper;
    private IRepositoryService<Shop> _shopService;
    private ShopItemsViewModel _shopItemsViewModel;
    
    public ManageAdminController(IMapper mapper, IRepositoryService<Shop> shopService)
    {
        _mapper = mapper;
        _shopService = shopService;
        _shopItemsViewModel = new ShopItemsViewModel();
    }
    
    public IActionResult Index()
    {
        IEnumerable<Shop> dbShops = _shopService.GetAllRecords();
        _shopItemsViewModel.Shops = _mapper.Map<List<ShopViewModel>>(dbShops);
        return View(_shopItemsViewModel);
    }
    
    // GET: ShopListController/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var dbShop = _shopService.GetSingle(id);
        var shopViewModel = _mapper.Map<ShopViewModel>(dbShop);
        return View(shopViewModel);
    }

    // GET: ShopListController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: ShopListController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(ShopViewModel shopViewModel)
    {
        try
        {
            _shopService.Add(_mapper.Map<Shop>(shopViewModel));
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: ShopListController/Edit/5
    public ActionResult Edit(int id)
    {
        var dbShop = _shopService.GetSingle(id);
        var shopViewModel = _mapper.Map<ShopViewModel>(dbShop);
        return View(shopViewModel);
    }

    // POST: ShopListController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, ShopViewModel shopViewModel)
    {
        try
        {
            _shopService.Edit(_mapper.Map<Shop>(shopViewModel));
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: ShopListController/Delete/5
    public ActionResult Delete(int id)
    {
        var dbShop = _shopService.GetSingle(id);
        _shopService.Delete(dbShop);
        return RedirectToAction("Index");
    }

    // POST: ShopListController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}