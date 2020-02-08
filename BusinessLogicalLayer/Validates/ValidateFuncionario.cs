using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogicalLayer.Validates
{
    public static class ValidateFuncionario
    {
        public static Response ValidateFuncionarioObj(Funcionario item)
        {
            Response response = ValidateLoginFuncionario(item.Email, item.Senha);

            if (response.HasErrors())
            {
                return response;
            }

            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                response.Erros.Add("O CPF deve ser informado");
            }
            else
            {
                item.CPF = item.CPF.Trim();
                if (!item.CPF.IsCpf())
                {
                    response.Erros.Add("O CPF informado é inválido.");

                    response.Sucesso = false;
                }
            }

            TimeSpan ts = DateTime.Now.Subtract(item.DataNascimento);
            int idade = (int)(ts.TotalDays / 365);

            if (idade < 14)
            {
                response.Erros.Add("O funcionario deve conter pelo menos 14 anos para começar a trabalhar");
            }

            response.Sucesso = !(response.HasErrors());

            return response;

        }

        public static Response ValidateLoginFuncionario(string Email, string Senha)
        {
            Response response = Security.SenhaValidator.ValidateSenha(Senha);

            if (response.HasErrors())
            {
                return response;
            }

            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (!rg.IsMatch(Email))
            {
                response.Erros.Add("Email informado está invalido");
            }

            response.Sucesso = !(response.HasErrors());

            return response;
        }

        public static DataResponse<Funcionario> ValidateIdFuncionario(int id)
        {
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();
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
                    Funcionario funcionario = db.Funcionarios.FirstOrDefault(x => x.ID == id);
                    if (funcionario != null)
                    {
                        response.Data.Add(funcionario);
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
