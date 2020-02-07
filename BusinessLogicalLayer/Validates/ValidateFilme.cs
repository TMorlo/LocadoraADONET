using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validates
{
    public static class ValidateFilme
    {
        public static Response ValidateFilmeObj(Filme item)
        {
            Response response = new Response();

            if (item.Duracao <= 10)
            {
                response.Erros.Add("Duração não pode ser menor que 10 minutos.");
            }

            if (item.DataLancamento == DateTime.MinValue
                                    ||
                item.DataLancamento > DateTime.Now)
            {
                response.Erros.Add("Data inválida.");
            }

            response.Sucesso = !(response.HasErrors());

            return response;
        }

        public static Response ValidateIdFilme(int id)
        {
            Response response = new Response();
            response.Sucesso = false;

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Filme filme = db.Filmes.FirstOrDefault(x => x.ID == id);
                if (filme != null)
                {
                    response.Sucesso = true;
                }
            }

            return response;
        }
    }
}
