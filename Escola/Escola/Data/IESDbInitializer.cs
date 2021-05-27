using Modelo.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Departamentos.Any())
            {
                return;
            }

            var instituicoes = new Instituicao[]
            {
                new Instituicao { Nome = "UniRP", Endereco="Ribeirão Preto"},
                new Instituicao { Nome = "Reges RP", Endereco="Jardim Botânico"},
                new Instituicao { Nome = "USP", Endereco="São Paulo"}
            };

            foreach(Instituicao i in instituicoes)
            {
                context.Instituicoes.Add(i);
            }

            context.SaveChanges();

            var departamentos = new Departamento[]
            {
                new Departamento {Nome = "Ciência da Computação", instituicaoID=1},
                new Departamento {Nome = "Engenharia de Alimentos", instituicaoID=2},
                new Departamento {Nome = "Direito", instituicaoID=2}
            };

            foreach(Departamento d in departamentos)
            {
                context.Departamentos.Add(d);
            }            

            context.SaveChanges();
        }
    }
}
