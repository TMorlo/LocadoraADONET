using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccessLayer;
using Entities.ResultSets;
using Entities.Enums;
using Entities.Entities;

namespace BusinessLogicalLayer
{
    public class FilmeBLL : IEntityCRUD<Filme>, IFilmeService
    {
        public Response Delete(int id)
        {
            //Validacoes 

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Filme filmeSerExcluido = new Filme();
                filmeSerExcluido.ID = id;
                db.Entry<Filme>(filmeSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return new Response();

        }

        public DataResponse<Filme> GetByID(int id)
        {
            //Validacoes 

            DataResponse<Filme> response = new DataResponse<Filme>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data.Add(db.Filmes.Where(x => x.ID == id).FirstOrDefault());
            }
            return response;
        }

        public DataResponse<Filme> GetData()
        {
            DataResponse<Filme> response = new DataResponse<Filme>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data = db.Filmes.ToList();
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmes()
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                List <Filme> filmes = db.Filmes.ToList();

                foreach(Filme filme in filmes)
                {
                    FilmeResultSet filmeResultSet = new FilmeResultSet()
                    {
                        Nome = filme.Nome,
                        Classificacao = filme.Classificacao,
                        Genero = db.Generos.FirstOrDefault(x => x.ID == filme.GeneroID).Nome,
                        ID = filme.ID
                    };

                    response.Data.Add(filmeResultSet);
                }
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                List<Filme> filmes = db.Filmes.Where(x => x.Classificacao.Equals(classificacao)).ToList();

                foreach (Filme filme in filmes)
                {
                    FilmeResultSet filmeResultSet = new FilmeResultSet()
                    {
                        Nome = filme.Nome,
                        Classificacao = filme.Classificacao,
                        Genero = db.Generos.FirstOrDefault(x => x.ID == filme.GeneroID).Nome,
                        ID = filme.ID
                    };

                    response.Data.Add(filmeResultSet);
                }
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByGenero(int genero)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                List <Filme> filmes = db.Filmes.Where(x => x.GeneroID == genero).ToList();

                foreach(Filme filme in filmes)
                {
                    FilmeResultSet filmeResultSet = new FilmeResultSet()
                    {
                        Nome = filme.Nome,
                        Classificacao = filme.Classificacao,
                        Genero = db.Generos.FirstOrDefault(x => x.ID == filme.GeneroID).Nome,
                        ID = filme.ID
                    };

                    response.Data.Add(filmeResultSet);
                }
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByName(string nome)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                List<Filme> filmes = db.Filmes.Where(x => x.Nome.Contains(nome)).ToList();

                foreach (Filme filme in filmes)
                {
                    FilmeResultSet filmeResultSet = new FilmeResultSet()
                    {
                        Nome = filme.Nome,
                        Classificacao = filme.Classificacao,
                        Genero = db.Generos.FirstOrDefault(x => x.ID == filme.GeneroID).Nome,
                        ID = filme.ID
                    };

                    response.Data.Add(filmeResultSet);
                }
            }

            return response;
        }

        public Response Insert(Filme item)
        {
            Response response = new Response();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                db.Filmes.Add(item);
                db.SaveChanges();
            }
            return response;
        }

        public Response Update(Filme item)
        {
            Response response = new Response();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Filme filme = db.Filmes.Where(x => x.ID == item.ID).FirstOrDefault();
                filme = item;       
                db.SaveChanges();
            }
            return response;
        }
    }
}
