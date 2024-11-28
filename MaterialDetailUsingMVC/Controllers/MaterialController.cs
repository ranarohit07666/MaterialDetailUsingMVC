using MaterialDetailUsingMVC.BLL.Service;
using MaterialDetailUsingMVC.Models.HelperModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaterialDetailUsingMVC.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private readonly IService _service;
        public MaterialController(IService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var materials = await _service.GetReferenceDetails();
            return View(materials);
        }
        [HttpGet]
        [Route("edit/{id?}")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Types"] = _service.GetTypes().Result.Select(t => new { t.Id, t.Name }).ToList();
            ViewData["Units"] = _service.GetUnits().Result.Select(u => new { u.Id, u.Name }).ToList();
            ViewData["References"] = _service.GetReferenceDetailWithoutInclude().Result.Select(u => new { Id = u.Id, Name = u.ReferenceNumber }).Append(new { Id = 0, Name = "Generate New" }).ToList();

            return View(id > 0 ? await _service.GetMaterialViewById(id) : new MaterialViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id?}")]
        public async Task<IActionResult> Edit(MaterialViewModel material, int id)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateMaterial(material);


                return RedirectToAction(nameof(Index));
            }
            ViewData["Types"] = _service.GetTypes().Result.Select(t => new { t.Id, t.Name }).ToList();
            ViewData["Units"] = _service.GetUnits().Result.Select(u => new { u.Id, u.Name }).ToList();
            ViewData["References"] = _service.GetReferenceDetailWithoutInclude().Result.Select(u => new { Id = u.Id, Name = u.ReferenceNumber }).Append(new { Id = 0, Name = "Generate New" }).ToList();
            return View(material);
        }
        [Authorize(Roles ="Admin")]
        [Route("delete/{id?}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteMaterial(id);
            return RedirectToAction("Index");
        }

    }
}
