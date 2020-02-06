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
    public class FuncionarioBLL : IEntityCRUD<Funcionario>
    {
        public Response Delete(int id)
        {
            //Validacoes 

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Funcionario FuncionarioSerExcluido = new Funcionario();
                FuncionarioSerExcluido.ID = id;
                db.Entry<Funcionario>(FuncionarioSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return new Response();

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
            Response response = new Response();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                db.Funcionarios.Add(item);
                db.SaveChanges();
            }
            return response;
        }

        public Response Update(Funcionario item)
        {
            Response response = new Response();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Funcionario funcionario = db.Funcionarios.Where(x => x.ID == item.ID).FirstOrDefault();
                funcionario = item;
                db.SaveChanges();
            }
            return response;
        }
    }
}
