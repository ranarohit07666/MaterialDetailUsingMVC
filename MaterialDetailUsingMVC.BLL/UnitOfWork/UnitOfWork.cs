using MaterialDetailUsingMVC.BLL.IRepository;
using MaterialDetailUsingMVC.BLL.Repository;
using MaterialDetailUsingMVC.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDetailUsingMVC.BLL.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly MaterialDataContext _context;
        public UnitOfWork(MaterialDataContext context)
        {
            _context = context;
            MaterialRepository=new MaterialRepository(_context);
            UnitRepository= new UnitRepository(_context);
            TypeRepository=new TypesRepository(_context);
            ReferenceDetailRepository= new ReferenceDetailRepository(_context);
            UserRepository= new UserRepository(_context);
        }

        public IMaterialRepository MaterialRepository { get; private set; }

        public IReferenceDetailRepository ReferenceDetailRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IUnitRepository UnitRepository { get; private set; }

        public ITypesRepository TypeRepository { get; private set; }
        
        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
