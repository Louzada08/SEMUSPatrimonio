//using CBP.Core.Communication;
using CBP.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace CBP.WebApp.MVC.Services
{
    public interface ITermoTransferenciaService
    {
        Task<TermoTransferenciaViewModel> ObterTermoTransferencia();
        Task<ResponseResult> AdicionarItemTermoTransferencia(ItemPatrimonioViewModel patrimonio);
        Task<ResponseResult> AtualizarItemTermoTransferencia(Guid patrimonioId, ItemPatrimonioViewModel patrimonio);
        Task<ResponseResult> RemoverItemTermoTransferencia(Guid patrimonioId);
    }
}