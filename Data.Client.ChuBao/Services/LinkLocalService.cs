using AutoMapper;
using Data.Client.ChuBao.DTOS;
using Data.Client.ChuBao.Entities;
using Data.Client.ChuBao.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Client.ChuBao.Services
{
    public class LinkLocalService : ILinkLocalService
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public LinkLocalService(IUnitOfWork work, IMapper mapper)
        {
            this._work = work;
            this._mapper = mapper;
        }


        public async Task<IEnumerable<LinkDto>> GetAllAsync()
        {
            var list = await _work.Linkmen.GetAllAsync();
            var result = _mapper.Map<IEnumerable<LinkDto>>(list);
            return result;
        }

        public async Task<LinkDto> GetAsync()
        {
            var entity = await _work.Linkmen.GetAsync();
            var dto = _mapper.Map<LinkDto>(entity);
            return dto;
        }

        public async Task<int> AddAsync(LinkNewDto dto)
        {
            var entity = _mapper.Map<Linkman>(dto);
            await _work.Linkmen.AddAsync(entity);
            return await _work.CommitAsync();
        }
        public async Task<int> UpdateAsync(LinkDto dto)
        {
            var newEntity = _mapper.Map<Linkman>(dto);
            var exist = await _work.Linkmen.GetAsync(q => q.Id == newEntity.Id);
            // comparate exist data and new data;
            if (exist == null) { return 0; }
            _work.Linkmen.Update(newEntity);
            return await _work.CommitAsync();
        }
    }
}
