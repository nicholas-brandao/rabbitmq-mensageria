using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiPublisher.Model.DAO;

namespace WebApiPublisher.Model
{
    [Table("TBL_PAGINA")]
    public class Pagina
    {

        #region contrutores
        public  Pagina()
        {
            PaginaDAO = new PaginaDAO();
            Parametros = new List<Parametro>();
        }
        #endregion

        #region propriedades
        [Key]
        [Column("PAG_ID")]
        public int Id { get; set; }
        

        [Column("PAG_IP")]
        public string Ip { get; set; }

        [Column("PAG_NOME_PAGINA")]
        public string NomePagina { get; set; }

        [Column("PAG_BROWSER")]
        public string Browser { get; set; }

        
        public ICollection<Parametro> Parametros { get; set; }
        

        [NotMapped]
        public PaginaDAO PaginaDAO { get; set; }

        #endregion

        #region metodos

        public void Salvar(Pagina pagina)
        {
            PaginaDAO.Salvar(pagina);
        }

        public void SalvarLista(List<Pagina> paginas)
        {
            PaginaDAO.SalvarLista(paginas);
        }

        #endregion


    }
}
