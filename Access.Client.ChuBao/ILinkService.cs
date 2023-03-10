﻿using Core.Client.ChuBao.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Client.ChuBao
{
    public interface ILinkService
    {
        Task<IEnumerable<LinkDto>> LoadLinkListAsync();
        Task<LinkDto> LoadLinkAsync();

        Task<bool> AddLinkAsync(LinkCreateDto model);
        Task<bool> ModifyLinkAsync(LinkDto model);
    }
}