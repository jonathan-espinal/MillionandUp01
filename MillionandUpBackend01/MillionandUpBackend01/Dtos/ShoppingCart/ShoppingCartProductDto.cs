

namespace MillionandUpBackend01.Dtos.ShoppingCart {
    public class ShoppingCartProductDto {
        public Guid Id { get; set; }
        public Guid? ShoppingCartId { get; set; } = null;
        public ProductConsts.Sources ProductSource { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get => Price * Quantity; }
    }
}
