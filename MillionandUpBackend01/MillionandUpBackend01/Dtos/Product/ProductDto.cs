namespace MillionandUpBackend01.Dtos.Product {

    public class ProductDto {
        public virtual ProductConsts.Sources Source { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public virtual float Rating { get; set; }
        public int QuantityAvailable { get; set; }
        public virtual List<string>? Images { get; set; }
    }
}
