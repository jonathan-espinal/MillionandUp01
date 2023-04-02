using MillionandUpBackend01.Dtos.Product;
using MillionandUpBackend01.Dtos.ShoppingCart;
using MillionandUpBackend01.Exceptions;
using MillionandUpBackend01.Lib.Validation;
using MillionandUpBackend01.Services;

namespace MillionandUpBackend01.Validators {
    public class ShopingCartValidator {

        private readonly ValidatorService<AddProductDto> _vCartItem;
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShopingCartValidator(ValidatorService<AddProductDto> vCartItem, IProductService productService, IShoppingCartService shoppingCartService) {
            _vCartItem = vCartItem;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<ValidatorService<AddProductDto>> ValidateAddToCartAsync(Guid userId, AddProductDto instance) {

            ShoppingCartProductDto? shoppingCartProduct = null;
            try {
                shoppingCartProduct = await _shoppingCartService.RetrieveProductAsync(userId, instance.Source, instance.Id);
            } catch (RecordNotFoundException) { }

            _vCartItem.AddObject(instance);
            ProductDto? product = null;
            _vCartItem.Add(obj => obj.Quantity)
            .CustomAsync(async obj => {
                try {
                    product = await _productService.RetrieveAsyn(instance.Source, instance.Id);
                    if (product == null) return false;
                } catch (Exception) {
                    if (obj.Quantity <= 0) {
                        return true;
                    }
                    return false;
                }
                return true;
            }, "The product does not exist.")
            .If(obj => product != null)
            .Custom(obj => product!.QuantityAvailable >= obj.Quantity, "The selected quantity is higher than the inventory quantity.")
            .If(obj => shoppingCartProduct == null)
            .Greater(0, "You must select a quantity greater than 0.");
            return _vCartItem;
        }

        public async Task<ErrorResponse> ValidateCheckoutAsync(Guid userId) {

            ShoppingCartDto shoppingCart = await _shoppingCartService.RetrieveAsync(userId);

            ErrorResponse errorResponse = new ErrorResponse();

            if (shoppingCart.Products == null || shoppingCart.Products?.Count == 0) {
                errorResponse.Add("_global", "The shopping cart is empty.");
                return errorResponse;
            }

            foreach (ShoppingCartProductDto product in shoppingCart.Products!) {
                try {
                    ProductDto productDto = await _productService.RetrieveAsyn(product.ProductSource, product.ProductId);
                    if (productDto.QuantityAvailable < product.Quantity) {
                        errorResponse.Add(product.Id.ToString(), "Product inventory is lower than selected.");
                    }
                    if (productDto.Price != product.Price) {
                        errorResponse.Add(product.Id.ToString(), "Product price has been changed.");
                    }
                } catch (Exception) {
                    errorResponse.Add(product.Id.ToString(), "Product is no longer available.");
                }
            }

            return errorResponse;
        }
    }
}
