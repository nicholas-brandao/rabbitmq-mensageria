using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPublisher.Connection;

namespace WebApiPublisher.Model.DAO
{
    public class PaginaDAO : IDAO<Pagina>
    {

        private ConnContext conn = new ConnContext();

        public void Salvar(Pagina pagina)
        {
            conn.Pagina.Add(pagina);
            conn.SaveChanges();
        }

        public void SalvarLista(List<Pagina> paginas)
        {

            foreach (var pagina in paginas)
            {
                conn.Pagina.Add(pagina);

                //if (pagina.Parametros != null)
                //{



                //    foreach (var parametro in pagina.Parametros)
                //    {
                //        parametro.Pagina = pagina;
                        
                //        conn.Parametro.Add(parametro);
                //    }
                //}
            }

            conn.SaveChanges();
        }
    }
}
