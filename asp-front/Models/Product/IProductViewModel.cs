namespace asp_front.Models.Product;

public interface IProductViewModel
{  
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
}

public interface IProductViewModelWithId : IProductViewModel
{
    public int Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
}