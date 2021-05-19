﻿//using System.Threading.Tasks;
//using Grpc.Core;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using CBP.Transferencia.API.Data;
//using CBP.Transferencia.API.Model;
//using CBP.WebAPI.Core.Usuario;

//namespace CBP.Transferencia.API.Services.gRPC
//{
//  [Authorize]
//  public class guiaGrpcService : CarrinhoCompras.CarrinhoComprasBase
//  {
//    private readonly ILogger<guiaGrpcService> _logger;

//    private readonly IAspNetUser _user;
//    private readonly CarrinhoContext _context;

//    public guiaGrpcService(
//        ILogger<guiaGrpcService> logger,
//        IAspNetUser user,
//        CarrinhoContext context)
//    {
//      _logger = logger;
//      _user = user;
//      _context = context;
//    }

//    public override async Task<CarrinhoClienteResponse> ObterCarrinho(ObterCarrinhoRequest request, ServerCallContext context)
//    {
//      _logger.LogInformation("Chamando ObterCarrinho");

//      var carrinho = await ObterCarrinhoCliente() ?? new CarrinhoCliente();

//      return MapCarrinhoClienteToProtoResponse(carrinho);
//    }

//    private async Task<CarrinhoCliente> ObterCarrinhoCliente()
//    {
//      return await _context.CarrinhoCliente
//          .Include(c => c.Itens)
//          .FirstOrDefaultAsync(c => c.ClienteId == _user.ObterUserId());
//    }

//    private static CarrinhoClienteResponse MapCarrinhoClienteToProtoResponse(CarrinhoCliente carrinho)
//    {
//      var carrinhoProto = new CarrinhoClienteResponse
//      {
//        Id = carrinho.Id.ToString(),
//        Clienteid = carrinho.ClienteId.ToString(),
//        Valortotal = (double)carrinho.ValorTotal,
//        Desconto = (double)carrinho.Desconto,
//        Voucherutilizado = carrinho.VoucherUtilizado,
//      };

//      if (carrinho.Voucher != null)
//      {
//        carrinhoProto.Voucher = new VoucherResponse
//        {
//          Codigo = carrinho.Voucher.Codigo,
//          Percentual = (double?)carrinho.Voucher.Percentual ?? 0,
//          Valordesconto = (double?)carrinho.Voucher.ValorDesconto ?? 0,
//          Tipodesconto = (int)carrinho.Voucher.TipoDesconto
//        };
//      }

//      foreach (var item in carrinho.Itens)
//      {
//        carrinhoProto.Itens.Add(new CarrinhoItemResponse
//        {
//          Id = item.Id.ToString(),
//          Nome = item.Nome,
//          Imagem = item.Imagem,
//          Produtoid = item.ProdutoId.ToString(),
//          Quantidade = item.Quantidade,
//          Valor = (double)item.Valor
//        });
//      }

//      return carrinhoProto;
//    }
//  }
//}