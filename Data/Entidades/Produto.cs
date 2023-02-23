using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entidades
{
    [Table("Produto")]
    public class Produto
    {
        [Column("Id")]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
    }
}
