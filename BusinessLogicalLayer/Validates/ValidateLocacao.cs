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
    public static class ValidateLocacao
    {
        public static Response ValidateLocacaoObj(Locacao locacao)
        {
            Response response = new Response();

            if (locacao.Filmes.Count == 0)
            {
                response.Erros.Add("Não é possível realizar a locação sem filmes.");
                response.Sucesso = false;
                return response;
            }

            TimeSpan ts = DateTime.Now.Subtract(locacao.Cliente.DataNascimento);
            int idade = (int)(ts.TotalDays / 365);

            foreach (Filme filme in locacao.Filmes)
            {
                if ((int)filme.Classificacao > idade)
                {
                    response.Erros.Add("A idade do cliente não corresponde com a classificação indicativa do filme " + filme.Nome);
                }
            }

            response.Sucesso = !(response.HasErrors());

            return response;
        }

        public static DataResponse<Locacao> ValidateIdLocacao(int id)
        {
            DataResponse<Locacao> response = new DataResponse<Locacao>();
            response.Sucesso = false;

            if (id <= 0)
            {
                response.Erros.Add("Informe um ID valido para Locacao");
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Locacao locacao = db.Locacoes.FirstOrDefault(x => x.ID == id);
                    if (locacao != null)
                    {
                        response.Data.Add(locacao);
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

            response.Erros.Add("nenhuma locacao foi encontrado com esse id");
            return response;
        }
    }
}
