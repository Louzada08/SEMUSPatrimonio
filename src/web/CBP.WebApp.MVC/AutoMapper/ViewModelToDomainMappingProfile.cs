//using AutoMapper;
//using CBP.WebAPI.Core.Usuario;
//using CBP.WebApp.MVC.DTO;
//using CBP.WebApp.MVC.Models;

//namespace CBP.WebApp.MVC.AutoMapper
//{
//  public class ViewModelToDomainMappingProfile : Profile
//  {
//    public ViewModelToDomainMappingProfile()
//    {
//      CreateMap<ResponsavelDTO, UsuarioViewModel>()
//        .ForMember(d => d.Funcao, o => o.MapFrom(s => Funcao.RetornaEnumPeloNome(s.Funcao)));
//    }

//    //public ViewModel2ToDomainMappingProfile()
//    //{
//    //  CreateMap<ProdutoViewModel, Produto>()
//    //      .ConstructUsing(p =>
//    //          new Produto(p.Nome, p.Descricao, p.Ativo,
//    //              p.Valor, p.CategoriaId, p.DataCadastro,
//    //              p.Imagem, new Dimensoes(p.Altura, p.Largura, p.Profundidade)));

//    //  CreateMap<CategoriaViewModel, Categoria>()
//    //      .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
//    //}

//  }
//}
