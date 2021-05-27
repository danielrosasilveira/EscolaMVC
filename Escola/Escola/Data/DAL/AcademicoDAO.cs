using Microsoft.EntityFrameworkCore;
using Modelo.Discente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Data.DAL
{
    public class AcademicoDAO
    {
        private IESContext _context;

        #region Construtor
        public AcademicoDAO(IESContext context)
        {
            _context = context;
        }
        #endregion

        #region ObterAcademicosClassificadosPorNome
        public async Task<IList<Academico>> ObterAcademicosClassificadosPorNome()
        {
            return await _context.Academicos.OrderBy(a => a.Nome).ToListAsync();
        }
        #endregion

        #region ObterAcademicoPorId
        public async Task<Academico> ObterAcademicoPorId(long id)
        {
            return await _context.Academicos.FindAsync(id);
        }
        #endregion

        #region GravarAcademico
        public async Task<Academico> GravarAcademico (Academico academico)
        {
            try
            {
                if (academico.AcademicoID == null)
                {
                    _context.Academicos.Add(academico);
                }
                else
                {
                    _context.Update(academico);
                }
                await _context.SaveChangesAsync();
                return academico;
            }
            catch(Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }
        #endregion

        #region EliminarAcademicoPorId
        public async Task<Academico> EliminarAcademicoPorId(long id)
        {
            Academico academico = await ObterAcademicoPorId(id);
            _context.Academicos.Remove(academico);
            await _context.SaveChangesAsync();
            return academico;
        }
        #endregion

    }
}
