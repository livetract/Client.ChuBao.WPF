using AutoMapper;
using Data.Client.ChuBao.DTOS;
using Data.Client.ChuBao.Entities;

namespace Data.Client.ChuBao.Commons
{
    public class DataProfile : Profile
    {
        public DataProfile() 
        {
            CreateMap<Linkman, LinkNewDto>().ReverseMap();
            CreateMap<Linkman, LinkDto>().ReverseMap();
        }
    }
}
