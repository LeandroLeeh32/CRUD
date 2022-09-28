using System;
using System.Collections.Generic;

namespace CRUD.Infrastructure
{
    public partial class Jogador
    {
        public string Cpf { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string NomeMae { get; set; } = null!;
    }
}
