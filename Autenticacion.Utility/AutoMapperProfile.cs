using Autenticacion.DTO;
using Autenticacion.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Utility
{
    public class AutoMapperProfile:Profile
    {

     public AutoMapperProfile()
     {
            CreateMap<Usuario,UsuarioDTO>().ReverseMap();

            CreateMap<Producto,ProductoDTO>().ReverseMap();


     }
    }
}
