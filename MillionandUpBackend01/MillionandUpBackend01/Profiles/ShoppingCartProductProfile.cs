using AutoMapper;
using MillionandUpBackend01.Dtos.ShoppingCart;
using MillionandUpBackend01.Entities;

namespace MillionandUpBackend01.Profiles {
    public class ShoppingCartProductProfile : Profile {

        public ShoppingCartProductProfile() {
            CreateMap<ShoppingCartProduct, ShoppingCartProductDto>();
            CreateMap<ShoppingCartProductDto, ShoppingCartProduct>();
        }
    }
}
