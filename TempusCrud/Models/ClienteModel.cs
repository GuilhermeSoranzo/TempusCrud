using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TempusCrud.Models
{
    public class ClienteModel
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataCadastro { get; set; }

        public decimal RendaFamiliar { get; set; }

        //[NotMapped]
        //public decimal RendaMedia { get; set; }
    }
}
