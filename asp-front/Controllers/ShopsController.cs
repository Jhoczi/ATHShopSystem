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
        var x = _shopService.GetAllRecords().ToList();
        //var x = (await _shopService.GetAll()).ToList();
        var y = _mapper.Map<List<ShopViewModel>>(x);
        _shopItemsViewModel.Shops = y;
        return View(_shopItemsViewModel);
    }

    // GET: ShopListController/Details/5
    public async Task<IActionResult> Details(int id)
    {
        // _shopItemsViewModel.Shops = _mapper.Map<List<ShopViewModel>>(await _shopService.GetAll());
        // var selectedShop = _shopItemsViewModel.Shops
        //     .SingleOrDefault(x => x.Id == id);
        // return View(selectedShop);
        return View();
    }

    // GET: ShopListController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: ShopListController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
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

    // GET: ShopListController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: ShopListController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
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