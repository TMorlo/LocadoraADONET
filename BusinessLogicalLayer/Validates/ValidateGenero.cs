using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static DataResponse<Genero> ValidateIdGenero(int id)
        {
            DataResponse<Genero> response = new DataResponse<Genero>();
            response.Sucesso = false;

            if(id <= 0)
            {
                response.Erros.Add("Informe um ID valido para funcionarios");
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Genero genero = db.Generos.FirstOrDefault(x => x.ID == id);
                    if (genero != null)
                    {
                        response.Data.Add(genero);
                        response.Sucesso = true;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }
            response.Erros.Add("nenhum funcionario foi encontrado com esse id");
            return response;
        }
    }
}
