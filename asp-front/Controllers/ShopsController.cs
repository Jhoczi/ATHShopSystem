using asp_front.Models;
using ath_server.Interfaces;
using ath_server.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace asp_front.Controllers;

public class ShopsController : Controller
{
    private readonly IMapper _mapper;
    //private IShopService _shopService;
    private IRepositoryService<Shop> _shopService;

    private ShopItemsViewModel _shopItemsViewModel;

    public ShopsController(IMapper mapper, IRepositoryService<Shop> shopService)
    {
        _mapper = mapper;
        _shopService = shopService;
        _shopItemsViewModel = new ShopItemsViewModel();
    }

    // GET: ShopListController
    public async Task<IActionResult> Index()
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
        var selectedShop = _shopItemsViewModel.Shops
            .SingleOrDefault(x => x.Id == id);
        return View();
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