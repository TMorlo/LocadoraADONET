using BusinessLogicalLayer.Validates;
using DataAccessLayer;
using Entities;
using Entities.Entities;
using System.Linq;
using System.Text.RegularExpressions;

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

            Response response = new Response();

            response = ValidateCliente.ValidateIdCliente(id);

            if (response.HasErrors())
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Cliente ClienteSerExcluido = new Cliente();
                ClienteSerExcluido.ID = id;
                db.Entry<Cliente>(ClienteSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                response.Sucesso = true;
            }

            return response;

        }

        public DataResponse<Cliente> GetByID(int id)
        {
            //Validacoes 

            DataResponse<Cliente> response = new DataResponse<Cliente>();
            response.Sucesso = false;

            Response response1 = ValidateCliente.ValidateIdCliente(id);

            if (response.Sucesso)
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data.Add(db.Clientes.Where(x => x.ID == id).FirstOrDefault());
                    response.Sucesso = true;
                }

                response.Erros.Add("Erro no meu programinha");
                response.Sucesso = false;

                return response;
            }

            return response;
        }

        public DataResponse<Cliente> GetData()
        {
            DataResponse<Cliente> response = new DataResponse<Cliente>();


            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data = db.Clientes.ToList();
                response.Sucesso = true;
            }

            return response;
        }

        public Response Insert(Cliente item)
        {
            Response response = new Response();

            response = ValidateCliente.ValidateClienteObj(item);

            if (response.HasErrors())
            {
                return response;
            }

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


            response = ValidateCliente.ValidateClienteObj(item);

            if (response.HasErrors())
            {
                return response;
            }
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
