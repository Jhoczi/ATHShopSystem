namespace asp_front.Models.Product;

public class CreateNewProductVm : IProductViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

