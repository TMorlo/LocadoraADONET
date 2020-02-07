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
    public static class ValidateCliente
    {
        public static Response ValidateClienteObj(Cliente item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do cliente deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Trim();
                item.Nome = Regex.Replace(item.Nome, @"\s+", " ");
                if (item.Nome.Length < 2 || item.Nome.Length > 50)
                {
                    response.Erros.Add("O nome do cliente deve conter entre 2 e 50 caracteres");
                }
            }
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                response.Erros.Add("O email do cliente deve ser informado.");
            }
            else
            {
                item.Email = item.Email.Trim();
                item.Email = Regex.Replace(item.Email, @"\s+", " ");
                if (item.Email.Length < 5 || item.Email.Length > 50)
                {
                    response.Erros.Add("O email do cliente deve conter entre 2 e 50 caracteres");
                }
            }
            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                response.Erros.Add("O cpf deve ser informado !!");
            }
            else
            {
                item.CPF = item.CPF.Trim();
            }

            if (!item.CPF.IsCpf())
            {
                response.Erros.Add("O cpf informado é invalido.");
            }

            response.Sucesso = !(response.HasErrors());

            return response;
        }

        public static Response ValidateIdCliente(int id)
        {
            Response response = new Response();
            response.Sucesso = false;

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Cliente cliente = db.Clientes.FirstOrDefault(x => x.ID == id);
                if(cliente != null)
                {
                    response.Sucesso = true;
                }
            }

            return response;
        }
    }
}
