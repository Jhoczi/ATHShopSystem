using asp_front.Models.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_front.Models.ViewModels.Shop;

public class EditShopViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public UserViewModel ShopOwner { get; set; }
    public List<SelectListItem> UserList { get; set; }
    
}