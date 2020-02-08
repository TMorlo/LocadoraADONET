using BusinessLogicalLayer.Validates;
using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.IO;
using System.Linq;

namespace BusinessLogicalLayer
{
    /// <summary>
    /// Classe responsável pelas regras de negócio 
    /// da entidade Gênero.
    /// </summary>
    public class ClienteBLL : IEntityCRUD<Cliente>
    {
        public DataResponse<Cliente> Delete(int id)
        {

            DataResponse<Cliente> response = new DataResponse<Cliente>();
            response = ValidateCliente.ValidateIdCliente(id);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Clientes.Remove(db.Clientes.Find(response.Data[0]));
                    response.Sucesso = true;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;

        }

        public DataResponse<Cliente> GetByID(int id)
        {
            return ValidateCliente.ValidateIdCliente(id); ;
        }

        public DataResponse<Cliente> GetData()
        {
            DataResponse<Cliente> response = new DataResponse<Cliente>();

            try{
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data = db.Clientes.ToList();
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

        public Response Insert(Cliente item)
        {

            Response response = ValidateCliente.ValidateClienteObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Clientes.Add(item);
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

        public Response Update(Cliente item)
        {

            Response response = ValidateCliente.ValidateClienteObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Cliente cliente = db.Clientes.Where(x => x.ID == item.ID).FirstOrDefault();
                    cliente = item;
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
