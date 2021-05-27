using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Cadastro
{
    public class CursoDisciplina
    {

        public long? CursoDisciplinaID { get; set; }

        public long? CursoID { get; set; }
        public Curso Curso { get; set; }


        public long? DisciplinaID { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
