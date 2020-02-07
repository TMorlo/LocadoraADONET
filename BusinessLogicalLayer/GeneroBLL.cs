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
    public class GeneroBLL : IEntityCRUD<Genero>
    {
        public Response Delete(int id)
        {
            //Validacoes 

            Response response = Validates.ValidateGenero.ValidateIdGenero(id);

            if (response.HasErrors())
            {
                return response;
            }


            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Genero GeneroSerExcluido = new Genero();
                GeneroSerExcluido.ID = id;
                db.Entry<Genero>(GeneroSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                response.Sucesso = true;
            }

            return response;

        }

        public DataResponse<Genero> GetByID(int id)
        {
            //Validacoes 

            DataResponse<Genero> response = new DataResponse<Genero>();
            response.Sucesso = false;

            Response response1 = Validates.ValidateGenero.ValidateIdGenero(id);

            if (response1.HasErrors())
            {
                return response;
            }
            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data.Add(db.Generos.Where(x => x.ID == id).FirstOrDefault());
                response.Sucesso = true;
            }
            return response;
        }

        public DataResponse<Genero> GetData()
        {
            DataResponse<Genero> response = new DataResponse<Genero>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data = db.Generos.ToList();
            }

            return response;
        }

        public Response Insert(Genero item)
        {
            Response response = Validates.ValidateGenero.ValidateGeneroObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                db.Generos.Add(item);
                db.SaveChanges();
                response.Sucesso = true;
            }
            return response;
        }

        public Response Update(Genero item)
        {
            Response response = Validates.ValidateGenero.ValidateGeneroObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Genero genero = db.Generos.Where(x => x.ID == item.ID).FirstOrDefault();
                genero = item;
                db.SaveChanges();
                response.Sucesso = true;
            }
            return response;
        }
    }
}
