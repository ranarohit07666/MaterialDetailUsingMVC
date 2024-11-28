using MaterialDetailUsingMVC.Models.HelperModels;
using MaterialDetailUsingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDetailUsingMVC.BLL.Service
{
    public interface IService
    {
        #region ReferenceDetails

        Task<IList<ReferenceDetail>> GetReferenceDetailWithoutInclude();
        Task<IList<ReferenceDetail>> GetReferenceDetails();
        Task<ReferenceDetail> GetReferenceDetailById(int id);
        Task DeleteReferenceDetail(int id);
        #endregion

        #region Material
        Task<IList<Material>> GetMaterials();
        Task<Material> GetMaterialById(int id);
        Task<MaterialViewModel> GetMaterialViewById(int id);
        Task CreateMaterial(MaterialViewModel material);
        Task DeleteMaterial(int id);
        #endregion

        #region Types
        Task<IList<Types>> GetTypes();
        Task<Types> GetTypeById(int id);
        Task DeleteType(int id);
        Task CreateType(Types types);
        #endregion

        #region Unit
        Task<IList<Unit>> GetUnits();
        Task<Unit> GetUnitById(int id);
        Task DeleteUnit(int id);
        Task CreateUnit(Unit unit);
        #endregion

    }
}
