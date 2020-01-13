using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoedasCrypt.Models
{
    public class Moedas
    {
        public int MoedasID { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }

        [NotMapped]
        public bool CheckBoxMarcado { get; set; }
    }
}
