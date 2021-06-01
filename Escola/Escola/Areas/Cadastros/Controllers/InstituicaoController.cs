using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Data;
using Modelo.Cadastro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escola.Data.DAL;

namespace Escola.Areas.Cadastros.Controllers
{
    [Area(nameof(Cadastros))]
    public class InstituicaoController : Controller
    {
        private readonly IESContext _context;
        private readonly InstituicaoDAO _instituicaoDAO;

        #region Construtor
        public InstituicaoController(IESContext context)
        {
            _context = context;
            _instituicaoDAO = new InstituicaoDAO(context);
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instituicoes.OrderBy(c => c.Nome).ToListAsync()); ;
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
        public async Task<IActionResult> Create([Bind("Nome, Endereco")] Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _instituicaoDAO.GravarInstituicao(instituicao);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(ex.Message, "Falha ao inserir");
            }
            return View(instituicao);
        }
        #endregion

        #region Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await _instituicaoDAO.ObterInstituicaoPorId((long)id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }
        #endregion

        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID, Nome, Endereco")]
        Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {                    
                    await _instituicaoDAO.GravarInstituicao(instituicao);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(ex.Message, "Falha ao atualizar");
                }
            }
            return View(instituicao);
        }
        #endregion

        #region Details - GET
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await _instituicaoDAO.ObterInstituicaoPorId((long)id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }
        #endregion

        #region Delete - GET
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await _instituicaoDAO.ObterInstituicaoPorId((long)id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }
        #endregion

        #region Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            
            var instituicao = await _instituicaoDAO.EliminarInstituicaoPorId((long)id);            
            TempData["Message"] = $"Intituição {instituicao.Nome.ToUpper()} removida!";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
