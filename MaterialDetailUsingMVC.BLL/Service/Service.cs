using MaterialDetailUsingMVC.BLL.UnitOfWork;
using MaterialDetailUsingMVC.Models;
using MaterialDetailUsingMVC.Models.HelperModels;
using Microsoft.EntityFrameworkCore;

namespace MaterialDetailUsingMVC.BLL.Service
{
    public class Service : IService
    {
        private readonly IUnitOfWork _unitOfWork;
        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Material
        public async Task DeleteMaterial(int id)
        {
            var entity = await _unitOfWork.MaterialRepository.GetByIdAsync(x => x.Id == id).FirstOrDefaultAsync();
            if (entity != null)
            {
                _unitOfWork.MaterialRepository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IList<Material>> GetMaterials()
        {
            return await _unitOfWork.MaterialRepository.GetAll().Include(x => x.Unit).Include(x => x.Types).Include(x => x.ReferenceDetail).ToListAsync();
        }

        public async Task CreateMaterial(MaterialViewModel material)
        {
            var existingMaterial = await _unitOfWork.MaterialRepository
                .GetByIdAsync(x => x.Id == material.Id).FirstOrDefaultAsync();

            ReferenceDetail referenceDetail;
            if (material.ReferenceId > 0)
            {
                referenceDetail = await _unitOfWork.ReferenceDetailRepository
                    .GetByIdAsync(x => x.Id == material.ReferenceId).FirstOrDefaultAsync();
            }
            else
            {
                referenceDetail = new ReferenceDetail { ReferenceDate = DateTime.Now };
                await _unitOfWork.ReferenceDetailRepository.AddAsync(referenceDetail);
                await _unitOfWork.SaveChangesAsync();
            }

            var unitById = material.UnitId > 0
                ? await _unitOfWork.UnitRepository.GetByIdAsync(x => x.Id == material.UnitId).FirstOrDefaultAsync()
                : null;

            var typeById = material.TypeId > 0
                ? await _unitOfWork.TypeRepository.GetByIdAsync(x => x.Id == material.TypeId).FirstOrDefaultAsync()
                : null;

            if (unitById != null && typeById != null)
            {
                if (existingMaterial == null)
                {
                    existingMaterial = new Material
                    {
                        MaterialName = material.MaterialName,
                        Rate = material.Rate,
                        Consumption = material.Consumption,
                        Unit = unitById,
                        Types = typeById,
                        ReferenceDetail = referenceDetail
                    };
                    await _unitOfWork.MaterialRepository.AddAsync(existingMaterial);
                }
                else
                {
                    existingMaterial.MaterialName = material.MaterialName;
                    existingMaterial.Rate = material.Rate;
                    existingMaterial.Consumption = material.Consumption;
                    existingMaterial.Unit = unitById;
                    existingMaterial.Types = typeById;
                    existingMaterial.ReferenceDetail = referenceDetail;
                    _unitOfWork.MaterialRepository.Update(existingMaterial);
                }
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<Material> GetMaterialById(int id)
        {
            return await _unitOfWork.MaterialRepository.GetByIdAsync(x => x.Id == id).Include(x => x.Unit).Include(x => x.Types).Include(x => x.ReferenceDetail).FirstOrDefaultAsync();
        }
        public async Task<MaterialViewModel> GetMaterialViewById(int id)
        {
            var material = await _unitOfWork.MaterialRepository.GetByIdAsync(x => x.Id == id).Include(x => x.Unit).Include(x => x.Types).Include(x => x.ReferenceDetail).FirstOrDefaultAsync();
            var viewModel = new MaterialViewModel();
            if (material != null)
            {
                viewModel.Id = id;
                viewModel.Rate = material.Rate;
                viewModel.Consumption = material.Consumption;
                viewModel.MaterialName = material.MaterialName;
                viewModel.UnitId = material.Unit.Id;
                viewModel.TypeId = material.Types.Id;
                viewModel.ReferenceId = material.ReferenceDetail.Id;

            }
            return viewModel;
        }
        #endregion

        #region ReferenceDetail
        public async Task<IList<ReferenceDetail>> GetReferenceDetailWithoutInclude()
        {
            return await _unitOfWork.ReferenceDetailRepository.GetAll().ToListAsync();
        }
        public async Task DeleteReferenceDetail(int id)
        {
            if (id > 0)
            {
                var referenceDetailById = await _unitOfWork.ReferenceDetailRepository.GetByIdAsync(x => x.Id == id).FirstOrDefaultAsync();
                if (referenceDetailById != null)
                {
                    _unitOfWork.ReferenceDetailRepository.Remove(referenceDetailById);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task<ReferenceDetail> GetReferenceDetailById(int id)
        {
            return await _unitOfWork.ReferenceDetailRepository.GetByIdAsync(x => x.Id == id).Include(x => x.Materials).ThenInclude(m => m.Unit).Include(x => x.Materials).ThenInclude(m => m.Types).FirstOrDefaultAsync();
        }

        public async Task<IList<ReferenceDetail>> GetReferenceDetails()
        {
            return await _unitOfWork.ReferenceDetailRepository.GetAll().Include(x => x.Materials).ThenInclude(m => m.Unit).Include(x => x.Materials).ThenInclude(m => m.Types).ToListAsync();
        }
        #endregion

        #region Types
        public async Task CreateType(Types types)
        {

            if (types == null) throw new Exception("Type not valid");
            if (types.Id > 0)
            {
                await _unitOfWork.TypeRepository.AddAsync(types);
            }
            else
            {
                _unitOfWork.TypeRepository.Update(types);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteType(int id)
        {
            if (id > 0)
            {
                var typeById = await _unitOfWork.TypeRepository.GetByIdAsync(x => x.Id == id).FirstOrDefaultAsync();
                if (typeById != null)
                {
                    _unitOfWork.TypeRepository.Remove(typeById);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task<Types> GetTypeById(int id)
        {
            return await _unitOfWork.TypeRepository.GetByIdAsync(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<Types>> GetTypes()
        {
            return await _unitOfWork.TypeRepository.GetAll().ToListAsync();
        }
        #endregion

        #region Unit
        public async Task CreateUnit(Unit unit)
        {
            if (unit == null) throw new Exception("Unit not valid");
            if (unit.Id > 0)
            {
                await _unitOfWork.UnitRepository.AddAsync(unit);
            }
            else
            {
                _unitOfWork.UnitRepository.Update(unit);
            }
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteUnit(int id)
        {
            if (id > 0)
            {
                var unitById = await _unitOfWork.UnitRepository.GetByIdAsync(x => x.Id == id).FirstOrDefaultAsync();
                if (unitById != null)
                {
                    _unitOfWork.UnitRepository.Remove(unitById);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task<Unit> GetUnitById(int id)
        {
            return await _unitOfWork.UnitRepository.GetByIdAsync(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<Unit>> GetUnits()
        {
            return await _unitOfWork.UnitRepository.GetAll().ToListAsync();
        }
        #endregion
    }
}
