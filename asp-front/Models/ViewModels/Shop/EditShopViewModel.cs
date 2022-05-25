using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_front.Models.ViewModels.Shop;

public class EditShopViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public string OwnerCredentials { get; set; }
    public string Address { get; set; }
    public List<global::Product> SelectedProducts { get; set; }
    public List<SelectListItem> SelectListProductItem { get; set; }
}