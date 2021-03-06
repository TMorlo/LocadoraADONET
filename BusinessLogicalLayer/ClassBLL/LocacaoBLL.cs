﻿using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogicalLayer
{
    public class LocacaoBLL : IEntityCRUD<Locacao>, ILocacaoService
    {
        public DataResponse<Locacao> Delete(int id)
        {
            DataResponse<Locacao> response = Validates.ValidateLocacao.ValidateIdLocacao(id);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Locacao LocacaoSerExcluido = response.Data[0];
                    db.Entry<Locacao>(LocacaoSerExcluido).State = System.Data.Entity.EntityState.Deleted;
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

        public Response EfetuarLocacao(Locacao locacao)
        {

            Response response = new Response();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    locacao.Cliente = db.Clientes.FirstOrDefault(x => x.ID == locacao.ClienteID);

                    //Response original, precisa validar

                    response = Validates.ValidateLocacao.ValidateLocacaoObj(locacao);

                    //Response teste, nao precisa validar
                    //Response response = new Response();

                    if (response.HasErrors())
                    {
                        return response;
                    }

                    List<Filme> FilmesTrack = new List<Filme>();

                    foreach (Filme f in locacao.Filmes)
                    {
                        FilmesTrack.Add(db.Filmes.FirstOrDefault(x => x.ID == f.ID));
                    }

                    locacao.Filmes = FilmesTrack;
                    db.Locacoes.Add(locacao);
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

        public DataResponse<Locacao> GetByID(int id)
        {
            return Validates.ValidateLocacao.ValidateIdLocacao(id);
        }

        public DataResponse<Locacao> GetData()
        {
            DataResponse<Locacao> response = new DataResponse<Locacao>();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data = db.Locacoes.ToList();
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

        public Response Insert(Locacao item)
        {
            Response response = Validates.ValidateLocacao.ValidateLocacaoObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Locacoes.Add(item);
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

        public Response Update(Locacao item)
        {
            Response response = Validates.ValidateLocacao.ValidateLocacaoObj(item);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Locacao LocacaoUpdate = item;
                    db.Entry<Locacao>(LocacaoUpdate).State = System.Data.Entity.EntityState.Modified;
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
