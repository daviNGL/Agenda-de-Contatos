using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    public class Contato
    {

        public Contato() { }

        public Contato(String name, String tel)
        {
            this.nome = name;
            this.telefone = tel;
        }

        public String nome { get; set; }
        public String telefone { get; set; }
    }
}
