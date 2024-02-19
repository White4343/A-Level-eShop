using AutoMapper;

namespace Order.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Entities.OrderBasket, Models.Dtos.OrderBasketDto>();
            CreateMap<Models.Dtos.OrderBasketDto, Data.Entities.OrderBasket>();
        }
    }
}
