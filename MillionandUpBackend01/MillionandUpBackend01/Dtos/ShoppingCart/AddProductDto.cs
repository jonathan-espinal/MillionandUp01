namespace MillionandUpBackend01.Dtos.ShoppingCart {
    public class AddProductDto {
        public virtual ProductConsts.Sources Source { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
