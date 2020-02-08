using DataAccessLayer;
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
                    db.Locacoes.Remove(db.Locacoes.Find(response.Data[0]));
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
            Response response = Validates.ValidateLocacao.ValidateLocacaoObj(locacao);

            if (response.HasErrors())
            {
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
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
                    Locacao locacao = db.Locacoes.Where(x => x.ID == item.ID).FirstOrDefault();
                    locacao = item;
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
