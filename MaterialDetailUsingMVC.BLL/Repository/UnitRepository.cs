using MaterialDetailUsingMVC.BLL.IRepository;
using MaterialDetailUsingMVC.DLL;
using MaterialDetailUsingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDetailUsingMVC.BLL.Repository
{
    public class UnitRepository:GenericRepository<Unit>,IUnitRepository
    {
        public UnitRepository(MaterialDataContext context):base(context)
        {
            
        }
    }
}
