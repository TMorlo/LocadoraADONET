using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class XxxLocadoraTesteStrategy : DropCreateDatabaseAlways<LocadoraDbContext>
    {
        protected override void Seed(LocadoraDbContext context)
        {

            DateTime nascimeto = new DateTime(2000, 2, 21);

            Genero g = new Genero(1, "comedia");
            Funcionario f = new Funcionario(1, "luiz", "luizfelipekohler03@gmail.com", "10766581993", nascimeto, "33351345", "Abcd123+", true);
            Cliente c = new Cliente(1, "gislaine", "10766581993", "gislaine01@gmail.com", nascimeto, true);
            Filme m = new Filme(1, "eu a patroa e as cria", nascimeto, Entities.Enums.Classificacao.Livre, 120, 1);

            context.Generos.Add(g);
            context.Funcionarios.Add(f);
            context.Clientes.Add(c);
            context.Filmes.Add(m);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
