using Core.Client.ChuBao.Dtos;
using Core.Client.ChuBao.Models;
using System.Threading.Tasks;

namespace Access.Client.ChuBao.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginDto model);
    }
}
