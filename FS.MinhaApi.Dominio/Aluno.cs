using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.MinhaApi.Dominio
{
    public class Aluno
    {
        public int Id{ get; set; }
        public int Nome { get; set; }
        public int Endereco { get; set; }
        public decimal Mensalidade{ get; set; }
    }
}
