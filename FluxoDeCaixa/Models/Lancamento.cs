using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Lancamento
    {
        public int Id { get; set; }
        public string Tipo { get; set; } // "Debito" ou "Credito"
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }
}
