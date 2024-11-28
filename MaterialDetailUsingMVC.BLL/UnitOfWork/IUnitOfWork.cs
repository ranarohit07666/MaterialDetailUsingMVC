using MaterialDetailUsingMVC.BLL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDetailUsingMVC.BLL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IMaterialRepository MaterialRepository { get; }
        IReferenceDetailRepository ReferenceDetailRepository { get; }
        IUserRepository UserRepository { get; }
        IUnitRepository UnitRepository { get; }
        ITypesRepository TypeRepository { get; }
        int Save();
        Task<int> SaveChangesAsync();
    }
}
