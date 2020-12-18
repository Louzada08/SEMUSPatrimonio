using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CBP.Transferencia.API.Data;
using CBP.Transferencia.API.Model;
using CBP.WebAPI.Core.Controllers;
using CBP.WebAPI.Core.Usuario;

namespace CBP.Transferencia.API.Controllers
{
    [Authorize]
    public class TermoTransferenciaController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly TermoTransferenciaContext _context;

        public TermoTransferenciaController(IAspNetUser user, TermoTransferenciaContext context)
        {
            _user = user;
            _context = context;
        }
        
        [HttpGet("termotransferencia")]
        public async Task<TermoTransferencia> ObterTermoTransferencia()
        {
            return await ObterTermoTransferenciaCliente() ?? new TermoTransferencia();
        }

        [HttpPost("termotransferencia")]
        public async Task<IActionResult> AdicionarItemTermoTransferencia(TermoTransferenciaItem item)
        {
            var TermoTransferencia = await ObterTermoTransferenciaCliente();

            if (TermoTransferencia == null)
                ManipularNovoTermoTransferencia(item);
            else
                ManipularTermoTransferenciaExistente(TermoTransferencia, item);

            if (!OperacaoValida()) return CustomResponse();

            await PersistirDados();
            return CustomResponse();
        }

        [HttpPut("TermoTransferencia/{patrimonioId}")]
        public async Task<IActionResult> AtualizarItemTermoTransferencia(Guid patrimonioId, TermoTransferenciaItem item)
        {
            var TermoTransferencia = await ObterTermoTransferenciaCliente();
            var itemTermoTransferencia = await ObterItemTermoTransferenciaValidado(patrimonioId, TermoTransferencia, item);
            if (itemTermoTransferencia == null) return CustomResponse();

            TermoTransferencia.AtualizarUnidades(itemTermoTransferencia, item.Quantidade);

            ValidarTermoTransferencia(TermoTransferencia);
            if (!OperacaoValida()) return CustomResponse();

            _context.TermoTransferenciaItens.Update(itemTermoTransferencia);
            _context.TermoTransferencia.Update(TermoTransferencia);

            await PersistirDados();
            return CustomResponse();
        }

        [HttpDelete("TermoTransferencia/{patrimonioId}")]
        public async Task<IActionResult> RemoverItemTermoTransferencia(Guid patrimonioId)
        {
            var TermoTransferencia = await ObterTermoTransferenciaCliente();

            var itemTermoTransferencia = await ObterItemTermoTransferenciaValidado(patrimonioId, TermoTransferencia);
            if (itemTermoTransferencia == null) return CustomResponse();

            ValidarTermoTransferencia(TermoTransferencia);
            if (!OperacaoValida()) return CustomResponse();

            TermoTransferencia.RemoverItem(itemTermoTransferencia);

            _context.TermoTransferenciaItens.Remove(itemTermoTransferencia);
            _context.TermoTransferencia.Update(TermoTransferencia);

            await PersistirDados();
            return CustomResponse();
        }

        private async Task<TermoTransferencia> ObterTermoTransferenciaCliente()
        {
            return await _context.TermoTransferencia
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.ResponsavelCedenteId == _user.ObterUserId());
        }
        private void ManipularNovoTermoTransferencia(TermoTransferenciaItem item)
        {
            var TermoTransferencia = new TermoTransferencia(_user.ObterUserId());
            TermoTransferencia.AdicionarItem(item);

            ValidarTermoTransferencia(TermoTransferencia);
            _context.TermoTransferencia.Add(TermoTransferencia);
        }
        private void ManipularTermoTransferenciaExistente(TermoTransferencia TermoTransferencia, TermoTransferenciaItem item)
        {
            var patrimonioItemExistente = TermoTransferencia.TermoTransferenciaItemExistente(item);

            TermoTransferencia.AdicionarItem(item);
            ValidarTermoTransferencia(TermoTransferencia);

            if (patrimonioItemExistente)
            {
                _context.TermoTransferenciaItens.Update(TermoTransferencia.ObterPorPatrimonioId(item.PatrimonioId));
            }
            else
            {
                _context.TermoTransferenciaItens.Add(item);
            }

            _context.TermoTransferencia.Update(TermoTransferencia);
        }
        private async Task<TermoTransferenciaItem> ObterItemTermoTransferenciaValidado(Guid patrimonioId, TermoTransferencia TermoTransferencia, TermoTransferenciaItem item = null)
        {
            if (item != null && patrimonioId != item.PatrimonioId)
            {
                AdicionarErroProcessamento("O item não corresponde ao informado");
                return null;
            }

            if (TermoTransferencia == null)
            {
                AdicionarErroProcessamento("TermoTransferencia não encontrado");
                return null;
            }

            var itemTermoTransferencia = await _context.TermoTransferenciaItens
                .FirstOrDefaultAsync(i => i.TermoTransferenciaId == TermoTransferencia.Id && i.PatrimonioId == patrimonioId);

            if (itemTermoTransferencia == null || !TermoTransferencia.TermoTransferenciaItemExistente(itemTermoTransferencia))
            {
                AdicionarErroProcessamento("O item não está no TermoTransferencia");
                return null;
            }

            return itemTermoTransferencia;
        }
        private async Task PersistirDados()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }
        private bool ValidarTermoTransferencia(TermoTransferencia TermoTransferencia)
        {
            if (TermoTransferencia.EhValido()) return true;

            TermoTransferencia.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }
    }
}
