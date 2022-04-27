using asp_front.Models;
using ath_server.Interfaces;
using ath_server.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_front.Areas.UserShop.Controllers;

[Authorize(Roles = "User")]
[Area("UserShop")]
public class UserShopController : Controller
{
    
    private readonly IMapper _mapper;
    private IRepositoryService<Shop> _shopService;
    private ShopViewModel _shopViewModel;

    public UserShopController(IMapper mapper, IRepositoryService<Shop> shopService)
    {
        _mapper = mapper;
        _shopService = shopService;
        _shopViewModel = _mapper.Map<ShopViewModel>(_shopService.GetAllRecords().FirstOrDefault());
    }
    
    public IActionResult Index()
    {
        return View(_shopViewModel);
    }
}