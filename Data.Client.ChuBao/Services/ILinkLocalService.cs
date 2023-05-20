using Data.Client.ChuBao.DTOS;
using Data.Client.ChuBao.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Client.ChuBao.Services
{
    public interface ILinkLocalService
    {
        Task<int> AddAsync(LinkNewDto dto);
        Task<IEnumerable<LinkDto>> GetAllAsync();
        Task<LinkDto> GetAsync();
        Task<int> UpdateAsync(LinkDto dto);
    }
}