namespace MillionandUpBackend01.Dtos.Product {

    public class ProductDummyjsonDto : IProductDto {
        public int Id { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public float Rating { get; set; }
        public int Stock { get; set; }
        public List<string>? Images { get; set; }

        public ProductDto ToProductDto() {
            ProductDto product = new ProductDto {
                Source = ProductConsts.Sources.Dummyjson,
                Id = Id,
                Title = Title,
                Price = Price,
                Category = Category,
                Description = Description,
                Rating = Rating,
                QuantityAvailable = Stock,
                Images = Images
            };

            return product;
        }
    }
}
