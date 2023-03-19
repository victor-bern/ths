using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ths.Shared.Entities
{
    public class Provider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Razão Social obrigatório")]
        public string CorporateName { get; set; }
        [Required(ErrorMessage = "Campo Telefone obrigatório")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Campo CNPJ obrigatório")]
        [RegularExpression("^[0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2}$", ErrorMessage = "CNPJ inválido")]
        public string Document { get; set; }
        [Required(ErrorMessage = "Campo Endereço obrigatório")]
        public string Address { get; set; }
    }
}
