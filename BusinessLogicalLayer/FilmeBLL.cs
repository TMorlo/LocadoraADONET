using DataAccessLayer;
using Entities;
using Entities.Entities;
using Entities.Enums;
using Entities.ResultSets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicalLayer
{
    public class FilmeBLL : IEntityCRUD<Filme>, IFilmeService
    {
        public Response Delete(int id)
        {
            //Validacoes 

            Response response = new Response();

            response = Validates.ValidateFilme.ValidateIdFilme(id);

            if (response.HasErrors())
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Filme filmeSerExcluido = new Filme();
                filmeSerExcluido.ID = id;
                db.Entry<Filme>(filmeSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                response.Sucesso = true;
            }
            return response;

        }

        public DataResponse<Filme> GetByID(int id)
        {
            //Validacoes 

            DataResponse<Filme> response = new DataResponse<Filme>();
            response.Sucesso = false;


            Response response1 = new Response();

            response1 = Validates.ValidateFilme.ValidateIdFilme(id);

            if (response.HasErrors())
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data.Add(db.Filmes.Where(x => x.ID == id).FirstOrDefault());
                response.Sucesso = true;
            }

            response.Sucesso = false;

            return response;
        }

        public DataResponse<Filme> GetData()
        {
            DataResponse<Filme> response = new DataResponse<Filme>();
            response.Sucesso = false;

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data = db.Filmes.ToList();
                response.Sucesso = true;
            }
            return response;

        }

        public DataResponse<FilmeResultSet> GetFilmes()
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            response.Sucesso = false;

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                List<Filme> filmes = db.Filmes.ToList();

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
                    response.Sucesso = true;
                }
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            response.Sucesso = false;

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

                    response.Sucesso = true;
                }
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByGenero(int genero)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            response.Sucesso = false;

            Response response1 = Validates.ValidateGenero.ValidateIdGenero(genero);

            if (response1.HasErrors())
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                List<Filme> filmes = db.Filmes.Where(x => x.GeneroID == genero).ToList();

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
                    response.Sucesso = true;
                }
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByName(string nome)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            response.Sucesso = false;

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
                    response.Sucesso = true;
                }
            }

            return response;
        }

        public Response Insert(Filme item)
        {
            Response response = new Response();

            response = Validates.ValidateFilme.ValidateFilmeObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                db.Filmes.Add(item);
                db.SaveChanges();
                response.Sucesso = true;
            }
            return response;
        }

        public Response Update(Filme item)
        {
            Response response = new Response();

            response = Validates.ValidateFilme.ValidateFilmeObj(item);

            if (response.HasErrors())
            {
                return response;
            }

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
