using asp_front.Models;
using asp_front.Models.ViewModels.Shop;
using asp_front.Models.ViewModels.User;
using ath_server.Interfaces;
using ath_server.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace asp_front.Areas.MainAdmin.Controllers;

[Authorize(Roles = "Admin")]
[Area("MainAdmin")]
public class ShopsController : Controller
{
    private readonly IMapper _mapper;
    private IRepositoryService<Shop> _shopService;
    private UserManager<IdentityUser> _usersManager;
    private ShopItemsViewModel _shopItemsViewModel;

    public ShopsController(IMapper mapper, IRepositoryService<Shop> shopService, IRepositoryService<Product> userService, UserManager<IdentityUser> userManager)
    {
        _mapper = mapper;
        _shopService = shopService;
        _usersManager = userManager;
        var x = _usersManager.Users.ToList();
        _shopItemsViewModel = new ShopItemsViewModel();
    }

    // GET: ShopListController
    public async Task<IActionResult> Index()
    {
        IEnumerable<Shop> dbShops = _shopService.GetAllRecords().Where(shop => shop.ArchivedAt == null);
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
        var shopViewModel = _mapper.Map<EditShopViewModel>(dbShop);
        shopViewModel.UserList = new List<SelectListItem>();
        shopViewModel.UserList.Add(new ()
        {
            Text = "Select User",
            Value = ""
        });
        var users = _mapper.Map<List<UserViewModel>>(_usersManager.Users.ToList());
        foreach (var user in users)
        {
            shopViewModel.UserList.Add(new ()
            {
                Text = user.Username,
                Value = user.Username
            });
        }
        return View(shopViewModel);
    }

    // POST: ShopListController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, EditShopViewModel shopViewModel)
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

    public ActionResult Archive(int id)
    {
        var dbShop = _shopService.GetSingle(id);
        dbShop.ArchivedAt = DateTime.Now;
        _shopService.Edit(dbShop);
        return RedirectToAction("Index");
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