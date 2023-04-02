using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MillionandUpBackend01.Dtos.Product;
using MillionandUpBackend01.Dtos.ShoppingCart;
using MillionandUpBackend01.Entities;
using MillionandUpBackend01.Exceptions;
using static MillionandUpBackend01.Consts.ProductConsts;


namespace MillionandUpBackend01.Services {
    public class ShoppingCartService : IShoppingCartService {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ShoppingCartService(DataContext context, IMapper mapper, IProductService productService) {
            _context = context;
            _mapper = mapper;
            _productService = productService;
        }

        private async Task<ShoppingCartProduct> GetProduct(Guid userId, ProductConsts.Sources source, int id) {
            return await _context.ShoppingCartProduct.Where(obj => obj.ProductSource == source && obj.ProductId == id && obj.ShoppingCart.UserId == userId && obj.ShoppingCart.IsComplete == false).FirstAsync();
        }

        public async Task<ShoppingCartDto> RetrieveAsync(Guid userId) {
            try {
                ShoppingCart shoppingCart = await _context.ShoppingCart.Where(obj => obj.UserId == userId && obj.IsComplete == false).FirstAsync();
                ShoppingCartDto shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);

                List<ShoppingCartProduct> shoppingCartPorducts = await _context.ShoppingCartProduct.Where(obj => obj.ShoppingCartId == shoppingCart.Id).ToListAsync();
                shoppingCartDto.Products = _mapper.Map<List<ShoppingCartProductDto>>(shoppingCartPorducts);

                return shoppingCartDto;
            } catch (Exception) {
                throw new RecordNotFoundException("RecordNotFound");
            }
        }

        public async Task<ShoppingCartProductDto> RetrieveProductAsync(Guid userId, ProductConsts.Sources source, int id) {
            try {
                ShoppingCartProduct shoppingCartProduct = await GetProduct(userId, source, id);
                ShoppingCartProductDto currentShoppingCartProduct = _mapper.Map<ShoppingCartProductDto>(shoppingCartProduct);
                return currentShoppingCartProduct;
            } catch (Exception) {
                throw new RecordNotFoundException("RecordNotFound");
            }
        }

        public async Task<ShoppingCartDto> AddToCartAsync(Guid userId, AddProductDto addProduct) {
            ShoppingCart shoppingCart = null!;

            try {
                shoppingCart = await _context.ShoppingCart.Where(obj => obj.UserId == userId && obj.IsComplete == false).FirstAsync();
            } catch (Exception) {
                shoppingCart = new ShoppingCart() {
                    Id = new Guid(),
                    UserId = userId
                };
                _context.Add(shoppingCart);
            }

            try {
                if(addProduct.Quantity <= 0) {
                    ShoppingCartProduct shoppingCartProduct = await GetProduct(userId, addProduct.Source, addProduct.Id);
                    _context.Remove(shoppingCartProduct);
                } else {
                    ProductDto? product = await _productService.RetrieveAsyn(addProduct.Source, addProduct.Id);
                    try {
                        ShoppingCartProduct shoppingCartProduct = await GetProduct(userId, addProduct.Source, addProduct.Id);
                        shoppingCartProduct.Quantity = addProduct.Quantity;
                    } catch (Exception) {
                        ShoppingCartProduct shoppingCartProducts = new ShoppingCartProduct() {
                            Id = new Guid(),
                            ShoppingCartId = shoppingCart.Id,
                            ProductSource = addProduct.Source,
                            ProductId = addProduct.Id,
                            Price = product.Price,
                            Quantity = addProduct.Quantity
                        };
                        _context.Add(shoppingCartProducts);
                    }
                }
                await _context.SaveChangesAsync();
            } catch (Exception) {
                // @TODO
            }

            return await RetrieveAsync(userId);
        }

        public async Task<bool> CheckoutAsync(Guid userId) {
            try {
                ShoppingCart shoppingCart = await _context.ShoppingCart.Where(obj => obj.UserId == userId && obj.IsComplete == false).FirstAsync();
                shoppingCart.IsComplete = true;
                await _context.SaveChangesAsync();
                return true;
            } catch { return false; }
        }
    }
}
