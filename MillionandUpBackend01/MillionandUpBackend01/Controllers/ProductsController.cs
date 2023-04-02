using Microsoft.AspNetCore.Mvc;
using MillionandUpBackend01.Dtos.Product;
using MillionandUpBackend01.Services;


namespace MillionandUpBackend01.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService) {
            _productService = productService;
        }

        [HttpGet(Name = "List")]
        public async Task<ActionResult<List<ProductDto>>> List(
            [FromQuery] ProductConsts.Order order = ProductConsts.Order.Title,
            [FromQuery] string? filterTitle = null,
            [FromQuery] double filterPriceMin = 0,
            [FromQuery] double filterPriceMax = 0
        ) {

            return await _productService.OrderAndFilterAsync(order, filterTitle, filterPriceMin, filterPriceMax);
        }

        [HttpGet("{source}/{id}", Name = "Retrieve")]
        public async Task<ActionResult<ProductDto?>> Retrieve(ProductConsts.Sources source, int id) {

            try {
                ProductDto? product = await _productService.RetrieveAsyn(source, id);
                return product;
            } catch (Exception) {
                return NotFound();
            }
        }
    }
}
