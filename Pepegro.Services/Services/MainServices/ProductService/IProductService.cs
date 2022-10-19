namespace Pepegro.Bll.Services.MainServices;

using Domain.DTO_s.MainEntities;

public interface IProductService
{
    public Task<List<GetProductDto>> GetPageOfProducts(int numberOfPage, int pageCount);
    
    public Task<GetProductDto> GetProductById(int id);

    public Task<int> CreateProduct(CreateProductDTO updateProductDto);

    public Task<int> UpdateProduct(int id, UpdateProductDTO updateProductDto);

    public Task DeleteProduct(int id);
    
    
}