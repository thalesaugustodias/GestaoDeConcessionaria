using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Application.DTOs
{
    public class RegistrarUsuarioDTO
    {
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public NivelAcesso NivelAcesso { get; set; }
    }
}
