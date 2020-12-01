using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBP.WebApp.MVC.Models;
using Refit;

namespace CBP.WebApp.MVC.Services
{
    public interface IPatrimonioService
    {
        Task<IEnumerable<PatrimonioViewModel>> ObterTodos();
        Task<PatrimonioViewModel> ObterPorId(Guid id);
    }

    public interface IPatrimonioServiceRefit
    {
        [Get("/patrimonio/patrimonios/")]
        Task<IEnumerable<PatrimonioViewModel>> ObterTodos();

        [Get("/patrimonio/patrimonios/{id}")]
        Task<PatrimonioViewModel> ObterPorId(Guid id);
    }
}