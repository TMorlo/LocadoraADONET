using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    /// <summary>
    /// Classe responsável pelas regras de negócio 
    /// da entidade Gênero.
    /// </summary>
    public class ClienteBLL : IEntityCRUD<Cliente>
    {
        public Response Delete(int id)
        {
            //Validacoes 

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Cliente ClienteSerExcluido = new Cliente();
                ClienteSerExcluido.ID = id;
                db.Entry<Cliente>(ClienteSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return new Response();

        }

        public DataResponse<Cliente> GetByID(int id)
        {
            //Validacoes 

            DataResponse<Cliente> response = new DataResponse<Cliente>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data.Add(db.Clientes.Where(x => x.ID == id).FirstOrDefault());
            }
            return response;
        }

        public DataResponse<Cliente> GetData()
        {
            DataResponse<Cliente> response = new DataResponse<Cliente>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data = db.Clientes.ToList();
            }

            return response;
        }

        public Response Insert(Cliente item)
        {
            Response response = new Response();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                db.Clientes.Add(item);
                db.SaveChanges();
            }
            return response;
        }

        public Response Update(Cliente item)
        {
            Response response = new Response();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Cliente cliente = db.Clientes.Where(x => x.ID == item.ID).FirstOrDefault();
                cliente = item;
                db.SaveChanges();
            }
            return response;
        }
    }
}
