using Escola.Data;
using Escola.Data.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Discente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Areas.Discente.Controllers
{
    [Area(nameof(Discente))]
    public class AcademicoController : Controller
    {
        private readonly IESContext _context;
        private readonly AcademicoDAO _academicoDAO;

        #region Construtor
        public AcademicoController(IESContext context)
        {
            _context = context;
            _academicoDAO = new AcademicoDAO(context);
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _academicoDAO.ObterAcademicosClassificadosPorNome());
        }
        #endregion

        #region ObterVisaoAcademicoPorId
        public async Task<IActionResult> ObterVisaoAcademicoPorId(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var academico = await _academicoDAO.ObterAcademicoPorId((long)id);

            if (academico == null)
            {
                return NotFound();
            }
            return View(academico);
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details (long? id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }
        #endregion

        #region Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit (long? id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }
        #endregion

        #region Delete - GET
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }
        #endregion

        #region Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, RegistroAcademico, Nascimento")]Academico academico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _academicoDAO.GravarAcademico(academico);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Erro: {ex.Message}");
            }
            return View(academico);
        }
        #endregion

        #region AcademicoExists
        private async Task<bool> AcademicoExists(long? id)
        {
            return await _academicoDAO.ObterAcademicoPorId((long)id) != null;
        }
        #endregion

        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (long? id, [Bind("AcademicoID, Nome, RegistroAcademico, Nascimento")]
            Academico academico)
        {
            if (id != academico.AcademicoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _academicoDAO.GravarAcademico(academico);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AcademicoExists(academico.AcademicoID))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(academico);
        }
        #endregion

        #region Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var academico = await _academicoDAO.EliminarAcademicoPorId((long)id);
            TempData["Message"] = $"Acadêmico {academico.Nome.ToUpper()} removido";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
