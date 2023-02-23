using Data.Entidades;
using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios
{
    public class RepositoryProduto : RepositoryGenerics<Produto>, IProduto
    {
        public RepositoryProduto(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
