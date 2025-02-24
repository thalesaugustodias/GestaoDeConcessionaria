using GestaoDeConcessionaria.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Gerente")]
    public class RelatoriosController(IVendaService servicoVenda) : ControllerBase
    {
        private readonly IVendaService _servicoVenda = servicoVenda;

        [HttpGet("mensal")]
        public async Task<IActionResult> RelatorioMensal(int mes, int ano)
        {
            if (mes < 1 || mes > 12)
                return BadRequest(new { Message = "Mês inválido." });
            if (ano < 1900 || ano > DateTime.Now.Year)
                return BadRequest(new { Message = "Ano inválido." });

            try
            {
                var vendas = await _servicoVenda.ObterTodosAsync();
                var vendasMensais = vendas.Where(v => v.DataVenda.Month == mes && v.DataVenda.Year == ano).ToList();

                if (!vendasMensais.Any())
                    return NotFound(new { Message = "Nenhuma venda encontrada para o período informado." });

                var totalVendas = vendasMensais.Count;
                var vendasPorTipo = vendasMensais.GroupBy(v => v.Veiculo.Tipo)
                    .Select(g => new { Tipo = g.Key.ToString(), Quantidade = g.Count() });
                var vendasPorFabricante = vendasMensais.GroupBy(v => v.Veiculo.Fabricante.Nome)
                    .Select(g => new { Fabricante = g.Key, Quantidade = g.Count() });
                var desempenhoConcessionarias = vendasMensais.GroupBy(v => v.Concessionaria.Nome)
                    .Select(g => new { Concessionaria = g.Key, Quantidade = g.Count() });

                var relatorio = new
                {
                    TotalVendas = totalVendas,
                    VendasPorTipo = vendasPorTipo,
                    VendasPorFabricante = vendasPorFabricante,
                    DesempenhoConcessionarias = desempenhoConcessionarias
                };

                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao gerar relatório: " + ex.Message });
            }
        }
    }
}