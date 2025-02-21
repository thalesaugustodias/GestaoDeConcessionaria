using GestaoDeConcessionaria.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        public NivelAcesso NivelAcesso { get; set; }
    }
}
