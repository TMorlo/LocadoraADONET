using BusinessLogicalLayer.Security;
using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class FuncionarioBLL : IEntityCRUD<Funcionario>, IFuncionarioService
    {
        public DataResponse<Funcionario> Autenticar(string email, string senha)
        {

            DataResponse<Funcionario> response = new DataResponse<Funcionario>();
            response.Sucesso = false;

            Response responseValidateLogin = Validates.ValidateFuncionario.ValidateLoginFuncionario(email, senha);

            if (responseValidateLogin.HasErrors())
            {
                response.Erros.Add(responseValidateLogin.GetErrorMessage());
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Funcionario funcionarioLogar = db.Funcionarios.FirstOrDefault(x => (x.Email == email) && (x.Senha == senha));
                    if (funcionarioLogar != null)
                    {
                        response.Data.Add(funcionarioLogar);
                        response.Sucesso = true;
                        User.FuncionarioLogado = funcionarioLogar;
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

            response.Erros.Add("Erro na senha ou no Email");
            return response;

        }

        public DataResponse<Funcionario> Delete(int id)
        {

            DataResponse<Funcionario> response = Validates.ValidateFuncionario.ValidateIdFuncionario(id);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Funcionario FuncionarioSerExcluido = response.Data[0];
                    db.Entry<Funcionario>(FuncionarioSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    response.Sucesso = true;
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;

        }

        public DataResponse<Funcionario> GetByID(int id)
        {
            return Validates.ValidateFuncionario.ValidateIdFuncionario(id);
        }

        public DataResponse<Funcionario> GetData()
        {
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data = db.Funcionarios.ToList();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;
        }

        public Response Insert(Funcionario item)
        {
            Response response = Validates.ValidateFuncionario.ValidateFuncionarioObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Funcionarios.Add(item);
                    db.SaveChanges();
                    response.Sucesso = true;
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;
        }

        public Response Update(Funcionario item)
        {
            Response response = Validates.ValidateFuncionario.ValidateFuncionarioObj(item);

            if (response.HasErrors())
            {
                return response;
            }
            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Funcionario FuncionarioUpdate = item;
                    db.Entry<Funcionario>(FuncionarioUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    response.Sucesso = true;
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;
        }
    }
}
