using Microsoft.EntityFrameworkCore;
using Modelo.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Data.DAL
{
    public class DepartamentoDAO
    {
        private IESContext _context;

        #region Construtor
        public DepartamentoDAO(IESContext context)
        {
            _context = context;
        }
        #endregion

        #region ObterDepartamentos
        public async Task<IList<Departamento>> ObterDepartamentos()
        {
            //return await _context.Departamentos.OrderBy(d => d.Nome).ToListAsync();
            return await _context.Departamentos.Include(i=>i.Instituicao).OrderBy(d => d.Nome).ToListAsync();
        }
        #endregion

        #region ObterDepartamentosPorId
        public async Task<Departamento> ObterDepartamentoPorID(long? id)
        {
            var departamento = await _context.Departamentos.Include(i=>i.Instituicao).
                SingleOrDefaultAsync(d => d.DepartamentoID == id);

            _context.Instituicoes.Where(i => departamento.instituicaoID == i.InstituicaoID);

            return departamento;
        }
        #endregion

        #region GravarDepartamento
        public async Task<Departamento> GravarDepartamento(Departamento departamento)
        {
            if (departamento.DepartamentoID == null)
            {
                _context.Departamentos.Add(departamento);
            }
            else
            {
                _context.Update(departamento);
            }
            await _context.SaveChangesAsync();
            
            return departamento;
        }
        #endregion

        #region EliminarDepartamentoPorId
        public async Task<Departamento> EliminarDepartamentoPorId(long id)
        {
            Departamento departamento = await ObterDepartamentoPorID(id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            return departamento;
        }
        #endregion

    }
}
