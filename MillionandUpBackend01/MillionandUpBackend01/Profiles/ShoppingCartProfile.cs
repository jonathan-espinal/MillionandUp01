using AutoMapper;
using MillionandUpBackend01.Dtos.ShoppingCart;
using MillionandUpBackend01.Dtos.User;
using MillionandUpBackend01.Entities;

namespace MillionandUpBackend01.Profiles {
    public class ShoppingCartProfile : Profile {

        public ShoppingCartProfile() {
            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<ShoppingCartDto, ShoppingCart>();
        }
    }
}
