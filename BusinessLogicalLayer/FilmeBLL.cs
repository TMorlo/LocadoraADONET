using DataAccessLayer;
using Entities;
using Entities.Entities;
using Entities.Enums;
using Entities.ResultSets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessLogicalLayer
{
    public class FilmeBLL : IEntityCRUD<Filme>, IFilmeService
    {
        public DataResponse<Filme> Delete(int id)
        {
            //Validacoes 


            DataResponse<Filme> response = Validates.ValidateFilme.ValidateIdFilme(id);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Filmes.Remove(db.Filmes.Find(response.Data[0]));
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

        public DataResponse<Filme> GetByID(int id)
        {

            return Validates.ValidateFilme.ValidateIdFilme(id);
        }

        public DataResponse<Filme> GetData()
        {
            DataResponse<Filme> response = new DataResponse<Filme>();
            response.Sucesso = false;

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data = db.Filmes.ToList();
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

        public DataResponse<FilmeResultSet> GetFilmes()
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            response.Sucesso = false;

            try
            {
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
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            response.Sucesso = false;

            try
            {
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
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByGenero(int genero)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            response.Sucesso = false;

            Response responseIdGenero = Validates.ValidateGenero.ValidateIdGenero(genero);

            if (responseIdGenero.HasErrors())
            {
                response.Erros.Add(responseIdGenero.GetErrorMessage());
                return response;
            }

            try
            {
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
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByName(string nome)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            response.Sucesso = false;

            if (string.IsNullOrWhiteSpace(nome))
            {
                response.Erros.Add("Infome o nome de algum filme para pesquisar");
                return response;
            }

            try
            {
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
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
                response.Sucesso = false;
                response.Erros.Add("Erro no meu programinha");
            }

            return response;
        }

        public Response Insert(Filme item)
        {
            Response response = Validates.ValidateFilme.ValidateFilmeObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Filmes.Add(item);
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

        public Response Update(Filme item)
        {
            Response response = Validates.ValidateFilme.ValidateFilmeObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Filme filme = db.Filmes.Where(x => x.ID == item.ID).FirstOrDefault();
                    filme = item;
                    db.SaveChanges();
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
