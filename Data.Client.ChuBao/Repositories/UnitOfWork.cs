using Data.Client.ChuBao.Contexts;
using Data.Client.ChuBao.Entities;
using System;
using System.Threading.Tasks;

namespace Data.Client.ChuBao.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppdbContext _context;
        public IGenericRepository<Linkman> Linkmen { get; }
        public UnitOfWork(AppdbContext context)
        {
            this._context = context;
            Linkmen = new GenericRepository<Linkman>(context);
        }
        public async Task<int> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
