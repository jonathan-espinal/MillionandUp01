using MillionandUpBackend01.Dtos.Product;

namespace MillionandUpBackend01.Services
{

    public interface IProductService {

        public Task<List<ProductDto>> ListAsync();
        public Task<ProductDto> RetrieveAsyn(ProductConsts.Sources source, int id);
        public Task<List<ProductDto>> OrderAndFilterAsync(ProductConsts.Order order = ProductConsts.Order.Title, string? filterTitle = null, double filterPriceMin = 0, double filterPriceMax = 0);
    }
}
