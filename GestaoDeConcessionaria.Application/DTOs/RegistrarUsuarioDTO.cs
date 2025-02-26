using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Application.DTOs
{
    public record RegistrarUsuarioDto(string NomeUsuario, string Email, string Senha, NivelAcesso NivelAcesso);
}
