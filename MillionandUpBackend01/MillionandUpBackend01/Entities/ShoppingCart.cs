namespace MillionandUpBackend01.Entities {
    public class ShoppingCart : BaseEntity {
        public Guid? UserId { get; set; } = null;
        public User? User { get; set; } = null;
        public bool IsComplete { get; set; } = false;
    }

    public class ShoppingCartProduct : BaseEntity {
        public Guid? ShoppingCartId { get; set; } = null;
        public ShoppingCart ShoppingCart { get; set; } = null!;
        public ProductConsts.Sources ProductSource { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
