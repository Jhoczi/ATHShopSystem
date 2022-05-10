using asp_front.Models.Product;
using AutoMapper;

namespace asp_front.MapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductVm>().ReverseMap();
    }
}