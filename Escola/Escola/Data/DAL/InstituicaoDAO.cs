using Microsoft.EntityFrameworkCore;
using Modelo.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Data.DAL
{
    public class InstituicaoDAO
    {
        private readonly IESContext _context;

        #region Construtor
        public InstituicaoDAO(IESContext context)
        {
            _context = context;
        }
        #endregion

        #region ObterInstituicoesClassificadasPorNome
        public async Task<List<Instituicao>> ObterInstituicoesClassificadasPorNome()
        {
            return await _context.Instituicoes.OrderBy(i => i.Nome).ToListAsync();            
        }
        #endregion

        #region ObterInstituicaoClassificadaPorId
        public async Task<Instituicao> ObterInstituicaoPorId(long id)
        {
            return await _context.Instituicoes.FindAsync(id);
        }
        #endregion

        #region GravarInstituicao
        public async Task<Instituicao> GravarInstituicao(Instituicao instituicao)
        {
            if (instituicao.InstituicaoID == null)
            {
                _context.Instituicoes.Add(instituicao);
            }
            else
            {
                _context.Update(instituicao);
            }
            await _context.SaveChangesAsync();
            
            return instituicao;
        }
        #endregion

        #region EliminarInstituicaoPorId
        public async Task<Instituicao> EliminarInstituicaoPorId(long id)
        {
            Instituicao instituicao = await ObterInstituicaoPorId(id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            return instituicao;
        }
        #endregion
    }
}
