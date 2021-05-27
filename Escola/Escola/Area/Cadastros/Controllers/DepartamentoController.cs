using Escola.Data;
using Escola.Data.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Areas.Cadastros.Controllers
{
    [Area(nameof(Cadastros))]
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;
        private readonly DepartamentoDAO _departamentoDAO;
        private readonly InstituicaoDAO _instituicaoDAO;

        #region Construtor
        public DepartamentoController(IESContext context)
        {
            _context = context;
            _departamentoDAO = new DepartamentoDAO(context);
            _instituicaoDAO = new InstituicaoDAO(context);
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _departamentoDAO.ObterDepartamentos());
        }
        #endregion

        #region Create - GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var instituicoes = await _instituicaoDAO.ObterInstituicoesClassificadasPorNome();
            ViewBag.Instituicoes = instituicoes;
            return View();
        }
        #endregion

        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, instituicaoID")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departamentoDAO.GravarDepartamento(departamento);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(ex.Message, "Falha ao inserir");
            }
            return View(departamento);
        }
        #endregion

        #region Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {           
            ViewResult visaoDepartamento = (ViewResult)await ObterVisaoDepartamentoPorId(id);
            Departamento departamento = (Departamento)visaoDepartamento.Model;
            
            ViewBag.Instituicoes = new SelectList(await _instituicaoDAO.ObterInstituicoesClassificadasPorNome(),
                "InstituicaoID", "Nome", departamento.instituicaoID);

            return visaoDepartamento;
        }
        #endregion

        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long?id, [Bind("DepartamentoID, Nome, instituicaoID")] 
        Departamento departamento)
        {
            if (id != departamento.DepartamentoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _departamentoDAO.GravarDepartamento(departamento);
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException ex)
                {
                    ModelState.AddModelError(ex.Message, "Falha ao atualizar");
                }
            }

            ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(n => n.Nome),
                "InstituicaoID", "Nome", departamento.instituicaoID);

            return View(departamento);
        }
        #endregion

        #region Details - GET
        public async Task<IActionResult> Details(long? id)
        {
            //if (id== null)
            //{
            //    return NotFound();
            //}
            //var departamento = await _context.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);
            //if (departamento == null)
            //{
            //    return NotFound();
            //}

            //_context.Instituicoes.Where(i => departamento.instituicaoID == i.InstituicaoID).Load();

            return await ObterVisaoDepartamentoPorId(id);
        }
        #endregion

        #region Delete - GET
        public async Task<IActionResult> Delete (long? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            //var departamento = await _context.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);
            //if (departamento == null)
            //{
            //    return NotFound();
            //}

            //_context.Instituicoes.Where(i => departamento.instituicaoID == i.InstituicaoID).Load();

            return await ObterVisaoDepartamentoPorId(id);
        }
        #endregion

        #region Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            //var departamento = await _context.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);
            //if (departamento == null)
            //{
            //    return NotFound();
            //}
            var departamento = await _departamentoDAO.EliminarDepartamentoPorId((long)id);

            TempData["Message"] = "Departamento " + departamento.Nome.ToUpper() + " foi removido!";

            //_context.Departamentos.Remove(departamento);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ObterVisaoDepartamentoPorId
        private async Task<IActionResult> ObterVisaoDepartamentoPorId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _departamentoDAO.ObterDepartamentoPorID((long)id);

            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }
        #endregion
    }
}
