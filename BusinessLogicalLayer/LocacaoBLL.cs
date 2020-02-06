using DataAccessLayer;
using Entities;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogicalLayer
{
    public class LocacaoBLL : IEntityCRUD<Locacao>, ILocacaoService
    {
        public Response Delete(int id)
        {
            //Validacoes 

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Locacao LocacaoSerExcluido = new Locacao();
                LocacaoSerExcluido.ID = id;
                db.Entry<Locacao>(LocacaoSerExcluido).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return new Response();

        }

        public Response EfetuarLocacao(Locacao locacao)
        {
            Response response = new Response();

            using(LocadoraDbContext db = new LocadoraDbContext())
            {
                db.Locacoes.Add(locacao);
                db.SaveChanges();
            }
            return response;
        }

        public DataResponse<Locacao> GetByID(int id)
        {
            //Validacoes 

            DataResponse<Locacao> response = new DataResponse<Locacao>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data.Add(db.Locacoes.Where(x => x.ID == id).FirstOrDefault());
            }
            return response;
        }

        public DataResponse<Locacao> GetData()
        {
            Entities.Entities.DataResponse<Locacao> response = new DataResponse<Locacao>();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                response.Data = db.Locacoes.ToList();
            }

            return response;
        }

        public Response Insert(Locacao item)
        {
            Response response = new Response();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                db.Locacoes.Add(item);
                db.SaveChanges();
            }
            return response;
        }

        public Response Update(Locacao item)
        {
            Response response = new Response();

            using (LocadoraDbContext db = new LocadoraDbContext())
            {
                Locacao locacao = db.Locacoes.Where(x => x.ID == item.ID).FirstOrDefault();
                locacao = item;
                db.SaveChanges();
            }
            return response;
        }
    }
}
