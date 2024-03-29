﻿using Core.Client.ChuBao.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Client.ChuBao.Services
{
    public interface ILinkService
    {
        Task<List<LinkListDto>> LoadLinkListAsync();
        Task<LinkDto> LoadLinkAsync();

        Task<bool> AddLinkAsync(LinkCreateDto model);
        Task<bool> ModifyLinkAsync(LinkDto model);

        Task<IEnumerable<RecordDto>> GetRecordListAsync(Guid contactId);
        Task<bool> AddLinkRecordAsync(RecordCreateDto model);
        public Task<MarkDto> GetMarkAsync(Guid contactId);
        public Task<bool> UpdateLinkMarkAsync(MarkDto model);
    }
}
