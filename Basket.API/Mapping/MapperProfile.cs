namespace Basket.API.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Data.Entities.Basket, BasketDto>();
            CreateMap<Data.Entities.Basket, BasketDto>();

            CreateMap<BasketCreateRequest, Data.Entities.Basket>();
            CreateMap<Data.Entities.Basket, BasketCreateRequest>();
        }
    }
}