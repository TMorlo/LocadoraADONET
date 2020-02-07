using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogicalLayer.Validates
{
    public static class ValidateFuncionario
    {
        public static Response ValidateFuncionarioObj(Funcionario item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                response.Erros.Add("O cpf deve ser informado");
            }
            else
            {
                item.CPF = item.CPF.Trim();
                if (!item.CPF.IsCpf())
                {
                    response.Erros.Add("O cpf informado é inválido.");

                    response.Sucesso = false;
                }
            }

            TimeSpan ts = DateTime.Now.Subtract(item.DataNascimento);
            int idade = (int)(ts.TotalDays / 365);

            if (idade < 14)
            {
                response.Erros.Add("Idade Insuficiente para trabalhar vai pra casa jogar CS o crianção");
            }

            if (response.Erros.Count == 0)
            {
                response.Sucesso = true;
            }

            return response;

        }

        public static Response ValidateLoginFuncionario(string Email, string Senha)
        {
            Response response = new Response();


            //Regex sen = new Regex("@(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{4,8})$");
            //if (!sen.IsMatch(Senha))
            //{
            //    response.Erros.Add("SENHA INVÁLIDA");
            //}

            //Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");


            //if (!rg.IsMatch(Email))
            //{
            //    response.Erros.Add("Email informado está invalido !!");
            //}

            response.Sucesso = !(response.HasErrors());

            return response;
        }

        public static Response ValidateIdFuncionario(int id)
        {
            Response response = new Response();
            response.Sucesso = false;

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Funcionario funcionario = db.Funcionarios.FirstOrDefault(x => x.ID == id);
                if (funcionario != null)
                {
                    response.Sucesso = true;
                }
            }

            return response;
        }
    }
}
