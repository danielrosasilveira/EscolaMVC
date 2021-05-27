using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Cadastro
{
    public class Disciplina
    {
        public long? DisciplinaID { get; set; }
        public string Nome { get; set; }
        public virtual IList<CursoDisciplina> CursosDisciplinas { get; set; }
    }
}
