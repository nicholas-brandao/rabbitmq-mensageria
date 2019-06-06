using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiPublisher.Model
{

    [Table("TBL_PARAMETRO")]
    public class Parametro
    {

        [Key]
        [Column("PAR_ID")]
        public int Id { get; set; }

        [Column("PAG_ID")]
        public int PaginaId { get; set; }
        //[ForeignKey("FK_TBL_PAGINA_TBL_PARAMETROS")]
        public Pagina Pagina { get; set; }

        [Column("PAR_NOME")]
        public string Nome { get; set; }

        [Column("PAR_VALOR")]
        public string Valor { get; set; }

    }
}