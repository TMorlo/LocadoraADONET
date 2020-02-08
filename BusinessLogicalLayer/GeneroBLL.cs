using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
        public DataResponse<Genero> Delete(int id)
        {

            DataResponse<Genero> response = Validates.ValidateGenero.ValidateIdGenero(id);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Generos.Remove(db.Generos.Find(response.Data[0]));
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

        public DataResponse<Genero> GetByID(int id)
        {
            return Validates.ValidateGenero.ValidateIdGenero(id);
        }

        public DataResponse<Genero> GetData()
        {
            DataResponse<Genero> response = new DataResponse<Genero>();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data = db.Generos.ToList();
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

        public Response Insert(Genero item)
        {
            Response response = Validates.ValidateGenero.ValidateGeneroObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Generos.Add(item);
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

        public Response Update(Genero item)
        {
            Response response = Validates.ValidateGenero.ValidateGeneroObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Genero genero = db.Generos.Where(x => x.ID == item.ID).FirstOrDefault();
                    genero = item;
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
