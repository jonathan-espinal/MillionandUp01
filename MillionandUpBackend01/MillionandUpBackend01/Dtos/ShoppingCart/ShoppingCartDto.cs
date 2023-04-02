namespace MillionandUpBackend01.Dtos.ShoppingCart {
    public class ShoppingCartDto {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; } = null;
        public List<ShoppingCartProductDto>? Products { get; set; }
        public double TotalPrice { get => (Products != null) ? Products.Sum(x => x.TotalPrice) : 0; }
    }
}
