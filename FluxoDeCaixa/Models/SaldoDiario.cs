﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SaldoDiario
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Saldo { get; set; }
    }
}
