using AutoMapper;
using Core.Client.ChuBao.Dtos;

namespace UI.Client.ChuBao.Commons
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LinkListDto, LinkDto>().ReverseMap();
        }
    }
}
