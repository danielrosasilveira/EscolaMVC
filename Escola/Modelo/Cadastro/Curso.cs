using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Cadastro
{
    public class Curso
    {
        public long? CursoID { get; set; }
        public string Nome { get; set; }

        public long? DepartamentoID { get; set; }
        public Departamento Departamento { get; set; }


        public virtual IList<CursoDisciplina> CursosDisciplinas { get; set; }
    }
}
