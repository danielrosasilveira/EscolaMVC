using Microsoft.EntityFrameworkCore;
using Modelo.Cadastro;
using Modelo.Discente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Data
{
    public class IESContext : DbContext
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {

        }        

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<CursoDisciplina>CursosDisciplinas { get; set; }

        public DbSet<Academico> Academicos { get; set; }
    }
}
