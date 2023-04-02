using MillionandUpBackend01.Dtos.Product;
using MillionandUpBackend01.Exceptions;


namespace MillionandUpBackend01.Services
{

    public class ProductService : IProductService {

        private readonly HttpClient _clientFakestoreapi;
        private readonly HttpClient _clientDummyjson;

        public ProductService(IHttpClientFactory httpClientFactory) {
            _clientFakestoreapi = httpClientFactory.CreateClient(ProductConsts.Sources.Fakestoreapi.ToString());
            _clientDummyjson = httpClientFactory.CreateClient(ProductConsts.Sources.Dummyjson.ToString());
        }

        private async Task<List<ProductDto>> ListFromFakestoreapiAsync() {
            List<ProductDto> products = new List<ProductDto>();
            List<ProductFakestoreapiDto>? fakestoreapiProducts = await _clientFakestoreapi.GetFromJsonAsync<List<ProductFakestoreapiDto>>("products");
            if (fakestoreapiProducts != null) {
                foreach (ProductFakestoreapiDto fakestoreapiProduct in fakestoreapiProducts) {
                    products.Add(fakestoreapiProduct.ToProductDto());
                }
            }
            return products;
        }

        private async Task<List<ProductDto>> ListFromDummyjsonAsync() {
            List<ProductDto> products = new List<ProductDto>();
            ResponseProductDummyjsonDto? response = await _clientDummyjson.GetFromJsonAsync<ResponseProductDummyjsonDto>("products");
            if (response?.Products != null) {
                foreach (ProductDummyjsonDto dummyjsonProduct in response.Products) {
                    products.Add(dummyjsonProduct.ToProductDto());
                }
            }
            return products;
        }

        public async Task<List<ProductDto>> ListAsync() {
            List<ProductDto> fakestoreapiProducts = await ListFromFakestoreapiAsync();
            List<ProductDto> dummyjsonProducts = await ListFromDummyjsonAsync();

            List<ProductDto> products = new List<ProductDto>();
            products.AddRange(fakestoreapiProducts);
            products.AddRange(dummyjsonProducts);

            return products;
        }

        public async Task<ProductDto> RetrieveAsyn(ProductConsts.Sources source, int id) {
            if (source == ProductConsts.Sources.Fakestoreapi) {
                ProductFakestoreapiDto? productFakestoreapi = await _clientFakestoreapi.GetFromJsonAsync<ProductFakestoreapiDto>($"products/{id}");
                if(productFakestoreapi != null) {
                    return productFakestoreapi.ToProductDto();
                }
            }
            ProductDummyjsonDto? productDummyjson = await _clientDummyjson.GetFromJsonAsync<ProductDummyjsonDto>($"products/{id}");
            if (productDummyjson != null) {
                return productDummyjson.ToProductDto();
            }
            
            throw new RecordExistException("RecordExist");
        }

        public async Task<List<ProductDto>> OrderAndFilterAsync(ProductConsts.Order order = ProductConsts.Order.Title, string? filterTitle = null, double filterPriceMin = 0, double filterPriceMax = 0) {
            List<ProductDto> products = await ListAsync();

            if (filterTitle != null) {
                products = products.Where(product => {
                    if (product.Title != null) {
                        return product.Title.ToLower().Contains(filterTitle.ToLower());
                    }
                    return false;
                }).ToList();
            }
            if(filterPriceMax > 0) {
                products = products.Where(product => product.Price >= filterPriceMin && product.Price <= filterPriceMax).ToList();
            }

            if (order == ProductConsts.Order.Title) {
                products = products.OrderBy(product => product.Title).ToList();
            } else {
                products = products.OrderByDescending(product => product.Price).ToList();
            }

            return products;
        }
    }
}
