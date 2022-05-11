namespace asp_front.Models.Product;

public class FormProductVm : IProductViewModelWithId
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}