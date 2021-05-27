using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastro
{
    public class Departamento
    {
        [Key]
        public long? DepartamentoID { get; set; }

        [Required]
        public string Nome { get; set; }

        public long? instituicaoID { get; set; }

        public Instituicao Instituicao { get; set; }

        public virtual IList<Curso> Cursos { get; set; }
    }
}
