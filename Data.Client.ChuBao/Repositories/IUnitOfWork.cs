using Data.Client.ChuBao.Entities;
using System.Threading.Tasks;

namespace Data.Client.ChuBao.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Linkman> Linkmen { get; }
        Task<int> CommitAsync();
        void Dispose();
    }
}