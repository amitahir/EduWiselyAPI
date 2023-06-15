using EduWisely.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWisely.Services.Services
{
    public abstract class BaseService : IDisposable
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing) _unitOfWork.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseService()
        {
            Dispose(false);
        }
    }
}
