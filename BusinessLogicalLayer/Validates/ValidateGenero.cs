using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validates
{
    public static class ValidateGenero
    {
        public static Response ValidateGeneroObj(Genero item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do gênero deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Trim();
                item.Nome = Regex.Replace(item.Nome, @"\s+", " ");
                if (item.Nome.Length < 2 || item.Nome.Length > 50)
                {
                    response.Erros.Add("O nome do gênero deve conter entre 2 e 50 caracteres");
                }
            }

            response.Sucesso = !(response.HasErrors());

            return response;
        }

        public static Response ValidateIdGenero(int id)
        {
            Response response = new Response();
            response.Sucesso = false;

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Genero genero = db.Generos.FirstOrDefault(x => x.ID == id);
                if (genero != null)
                {
                    response.Sucesso = true;
                }
            }

            return response;
        }
    }
}
