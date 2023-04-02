using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MillionandUpBackend01.Dtos.ShoppingCart;
using MillionandUpBackend01.Exceptions;
using MillionandUpBackend01.Lib.Session;
using MillionandUpBackend01.Lib.Validation;
using MillionandUpBackend01.Services;
using MillionandUpBackend01.Validators;

namespace MillionandUpBackend01.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase {

        private readonly ShopingCartValidator _shopingCartValidator;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly SessionService _sessionService;

        public ShoppingCartController(ShopingCartValidator shopingCartValidator, IShoppingCartService shoppingCartService, SessionService sessionService) {
            _shopingCartValidator = shopingCartValidator;
            _shoppingCartService = shoppingCartService;
            _sessionService = sessionService;
        }

        [HttpGet("Retrieve")]
        [Authorize]
        public async Task<ActionResult<ShoppingCartDto>> Retrieve() {
            try {
                return await _shoppingCartService.RetrieveAsync(_sessionService.User.Id);
            } catch (RecordNotFoundException) {
                return BadRequest(new {
                    response = "There is no shopping cart available for this user."
                });
            }
        }

        [HttpPost("AddToCart")]
        [Authorize]
        public async Task<ActionResult<ShoppingCartDto>> AddToCart(AddProductDto product) {
            ValidatorService<AddProductDto> validator = await _shopingCartValidator.ValidateAddToCartAsync(_sessionService.User.Id, product);
            if (!await validator.ValidateAsync()) return BadRequest(validator.Errors);
            return await _shoppingCartService.AddToCartAsync(_sessionService.User.Id, product);
        }

        [HttpPost("Checkout")]
        [Authorize]
        public async Task<ActionResult<Dictionary<string, bool>>> Checkout() {
            ErrorResponse errorResponse = await _shopingCartValidator.ValidateCheckoutAsync(_sessionService.User.Id);
            if(errorResponse.Count > 0) {
                return BadRequest(errorResponse.Data);
            }
            Dictionary<string, bool> returnData = new Dictionary<string, bool>();
            bool response = await _shoppingCartService.CheckoutAsync(_sessionService.User.Id);
            returnData.Add("response", response);
            return returnData;
        }
    }
}
