using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static DataResponse<Filme> ValidateIdFilme(int id)
        {
            DataResponse<Filme> response = new DataResponse<Filme>();
            response.Sucesso = false;

            if(id <= 0)
            {
                response.Erros.Add("ID do cliente esta invalido");
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Filme filme = db.Filmes.FirstOrDefault(x => x.ID == id);
                    if (filme != null)
                    {
                        response.Data.Add(filme);
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

            response.Erros.Add("nenhum cliente foi encontrado com esse id");
            return response;
        }
    }
}
