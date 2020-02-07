using BusinessLogicalLayer.Security;
using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class FuncionarioBLL : IEntityCRUD<Funcionario>, IFuncionarioService
    {
        public DataResponse<Funcionario> Autenticar(string email, string senha)
        {

            DataResponse<Funcionario> FuncionarioLogar = new DataResponse<Funcionario>();
            FuncionarioLogar.Sucesso = false;

            Response response = Validates.ValidateFuncionario.ValidateLoginFuncionario(email, senha);

            if (response.HasErrors())
            {
                FuncionarioLogar.Erros.Add(response.GetErrorMessage());
                return FuncionarioLogar;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                FuncionarioLogar.Data.Add(db.Funcionarios.FirstOrDefault(x => (x.Email == email && x.Senha == senha)));

                if(FuncionarioLogar.Data.Count == 0)
                {
                    User.FuncionarioLogado = FuncionarioLogar.Data[0];
                    FuncionarioLogar.Sucesso = true;
                }
                FuncionarioLogar.Sucesso = false;
            }

            return FuncionarioLogar;
        }

        public Response Delete(int id)
        {
            //Validacoes 

            Response response = new Response();

            response = Validates.ValidateFuncionario.ValidateIdFuncionario(id);

            if (response.HasErrors())
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Funcionario funcionarioSerExcluido = new Funcionario();
                funcionarioSerExcluido.ID = id;
                db.Entry<Funcionario>(funcionarioSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                response.Sucesso = true;
            }
            return response;

        }

        public DataResponse<Funcionario> GetByID(int id)
        {
            //Validacoes 

            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data.Add(db.Funcionarios.Where(x => x.ID == id).FirstOrDefault());
            }
            return response;
        }

        public DataResponse<Funcionario> GetData()
        {
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data = db.Funcionarios.ToList();
            }

            return response;
        }

        public Response Insert(Funcionario item)
        {
            Response response = Validates.ValidateFuncionario.ValidateFuncionarioObj(item);

            if (!response.Sucesso)
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                db.Funcionarios.Add(item);
                db.SaveChanges();
                response.Sucesso = true;
            }
            return response;
        }

        public Response Update(Funcionario item)
        {
            Response response = Validates.ValidateFuncionario.ValidateFuncionarioObj(item);

            if (!response.Sucesso)
            {
                return response;
            }
               

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Funcionario funcionario = db.Funcionarios.Where(x => x.ID == item.ID).FirstOrDefault();
                funcionario = item;
                db.SaveChanges();
                response.Sucesso = true;
            }
            return response;
        }
    }
}
