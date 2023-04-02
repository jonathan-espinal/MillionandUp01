using MillionandUpBackend01.Dtos.ShoppingCart;

namespace MillionandUpBackend01.Services {
    public interface IShoppingCartService {

        public Task<ShoppingCartDto> RetrieveAsync(Guid userId);
        public Task<ShoppingCartProductDto> RetrieveProductAsync(Guid userId, ProductConsts.Sources source, int id);
        public Task<ShoppingCartDto> AddToCartAsync(Guid userId, AddProductDto addProduct);
        public Task<bool> CheckoutAsync(Guid userId);
    }
}
