using System.ComponentModel.DataAnnotations;

namespace GestaoDeConcessionaria.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome de usuário.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Informe a senha.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
