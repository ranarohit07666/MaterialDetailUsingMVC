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
    public class TypesRepository:GenericRepository<Types>,ITypesRepository
    {
        public TypesRepository(MaterialDataContext context):base(context)
        {
            
        }
    }
}
