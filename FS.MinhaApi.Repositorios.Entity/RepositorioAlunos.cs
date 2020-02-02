using FS.Comum.Repositorios.Entity;
using FS.MinhaApi.AcessoDados.Entity.Context;
using FS.MinhaApi.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.MinhaApi.Repositorios.Entity
{
    public class RepositorioAlunos : RepositorioFS<Aluno, int>
    {
        public RepositorioAlunos(MinhaApiDbContext context)
            :base(context)
        {

        }
    }
}
