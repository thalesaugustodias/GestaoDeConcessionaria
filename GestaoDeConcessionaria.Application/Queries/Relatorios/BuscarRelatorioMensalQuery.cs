using GestaoDeConcessionaria.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeConcessionaria.Application.Queries.Relatorios
{
    public record BuscarRelatorioMensalQuery (int mes, int ano) : IRequest<DashboardDto>;
}
