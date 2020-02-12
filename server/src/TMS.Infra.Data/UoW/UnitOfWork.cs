using TMS.Domain.Interfaces;
using TMS.Infra.Data.Context;

namespace TMS.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskContext _context;

        public UnitOfWork(TaskContext context)
        {
            _context = context;
        }
        
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
