using Core.Client.ChuBao.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Access.Client.ChuBao
{
    public class LinkService : ILinkService
    {
        private readonly HttpClient _client;

        public LinkService(HttpClient client)
        {
            this._client = client;
        }

        

        public async Task<bool> AddLinkAsync(LinkCreateDto model)
        {
            var url = _client.BaseAddress + "createcontact";
            var reponse = await _client.PostAsJsonAsync(url,model,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (!reponse.IsSuccessStatusCode )
            {
                return false;
            }
            return true;
        }

        public Task<LinkDto> LoadLinkAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LinkDto>> LoadLinkListAsync()
        {
            //throw new NotImplementedException();
            var url = _client.BaseAddress + "getcontacts";
            var reponse = await _client.GetAsync(url);
            if (!reponse.IsSuccessStatusCode)
            {
                // 请求失败怎么办？
                return Enumerable.Empty<LinkDto>();
            }
            var links = await _client.GetFromJsonAsync<IEnumerable<LinkDto>>(url, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return links;
        }

        public async Task<bool> ModifyLinkAsync(LinkDto model)
        {
            var url = _client.BaseAddress + "updatecontact";
            var reponse = await _client.PutAsJsonAsync(url, model,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)); 
            return reponse.IsSuccessStatusCode ? true : false;
        }

        // Contact Record

        public async Task<bool> AddLinkRecordAsync(RecordCreateDto model)
        {
            var url = _client.BaseAddress + "addcontactrecord";
            var reponse = await _client.PostAsJsonAsync(url, model,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (!reponse.IsSuccessStatusCode) { return false; }
            return true;
        }

        public async Task<IEnumerable<RecordDto>> GetRecordListAsync(Guid contactId)
        {
            var url = _client.BaseAddress + "getcontactrecords";
            var reponse = await _client.PostAsJsonAsync(url, contactId,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (!reponse.IsSuccessStatusCode)
            {
                return Enumerable.Empty<RecordDto>();
            }
            var records = await reponse.Content.ReadFromJsonAsync<List<RecordDto>>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return records;
        }

        // Contact Mark
        public async Task<MarkDto> GetLinkMarkAsync(Guid contactId)
        {
            var url = _client.BaseAddress + "getcontactmark";
            var reponse = await _client.PostAsJsonAsync(url, contactId,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (!reponse.IsSuccessStatusCode)
            {
                // ...
            }
            var mark = await reponse.Content.ReadFromJsonAsync<MarkDto>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return mark;
        }

        public async Task<bool> UpdateLinkMarkAsync(MarkDto model)
        {
            var url = _client.BaseAddress + "updatecontactmark";
            var reponse = await _client.PutAsJsonAsync(url, model,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (!reponse.IsSuccessStatusCode) {  return false; }
            return true;
        }
    }
}
