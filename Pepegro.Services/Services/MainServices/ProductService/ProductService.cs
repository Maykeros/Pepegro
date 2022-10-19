namespace Pepegro.Bll.Services.MainServices;

using AutoMapper;
using Domain.DTO_s.MainEntities;
using Domain.Entities.MainEntities;
using Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<GetProductDto>> GetPageOfProducts(int numberOfPage, int pageCount)
    {
        _logger.LogInformation($"Try to get page of products, number: {numberOfPage}");

        var products = await _unitOfWork.Products.GetAll();

        products = products.Skip((numberOfPage - 1) * pageCount).Take(pageCount).ToList();

        return _mapper.Map<List<GetProductDto>>(products);
    }

    public async Task<GetProductDto> GetProductById(int id)
    {
        _logger.LogInformation($"Try to get product");

        var products = await _unitOfWork.Products.Get(p => p.Id == id);

        return _mapper.Map<GetProductDto>(products);
    }

    public async Task<int> CreateProduct(CreateProductDTO createProductDto)
    {
        _logger.LogInformation("Try to create product");

        var product = _mapper.Map<Product>(createProductDto);

        await _unitOfWork.Products.Create(product);

        return product.Id;
    }

    public async Task<int> UpdateProduct(int id, UpdateProductDTO updateProductDto)
    {
        _logger.LogInformation("Try to update hotel");

        var product = await _unitOfWork.Products.Get(p => p.Id == id);

        _mapper.Map(updateProductDto, product);

        await _unitOfWork.Products.Update(product);

        return id;
    }

    public async Task DeleteProduct(int id)
    {
        _logger.LogInformation("Try to delete hotel");

        var product = _unitOfWork.Products.Get(p => p.Id == id);
        if (product == null)
        {
            _logger.LogError("Product is no found, Invalid delete operation");
        }
        
        await _unitOfWork.Products.Delete(id);
    }
}