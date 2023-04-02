namespace MillionandUpBackend01.Dtos.Product {

    public class ProductFakestoreapiDto : IProductDto {

        public int Id { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public RatingFakestoreDto Rating { get; set; } = new RatingFakestoreDto();
        public string? Image { get; set; }

        public ProductDto ToProductDto() {
            ProductDto product = new ProductDto {
                Source = ProductConsts.Sources.Fakestoreapi,
                Id = Id,
                Title = Title,
                Price = Price,
                Category = Category,
                Description = Description,
                Rating = Rating.Rate,
                QuantityAvailable = 0,
                Images = new List<string>() { Image }
            };

            return product;
        }
    }
}
