using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWisely.UoW
{
    public partial class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork()
        {

        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
