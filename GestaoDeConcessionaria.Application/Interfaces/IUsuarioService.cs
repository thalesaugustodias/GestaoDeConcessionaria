using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarAsync(Usuario usuario, string senha);
        Task<string> AutenticarAsync(string nomeUsuario, string senha);
    }
}
