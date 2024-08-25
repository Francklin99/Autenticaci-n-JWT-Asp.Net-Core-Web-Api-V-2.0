using Autenticacion.BLL.Servicios.Contrato;
using Autenticacion.DAL.Repositorio.Contrato;
using Autenticacion.DTO;
using Autenticacion.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _repositorio; // <1>
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> repositorio, IMapper mapper)
        {
            _repositorio=repositorio;
            _mapper=mapper;
        }

        public async Task<List<ProductoDTO>> listar()
        {
            try
            {
                var productos=await _repositorio.Consultar();
                return _mapper.Map<List<ProductoDTO>>(productos);
            }
            catch { 
                throw;
            }
            
        }

        public async Task<ProductoDTO> Registrar(ProductoDTO producto)
        {
            try
            {
                var productoModel = _mapper.Map<Producto>(producto);
                await _repositorio.Crear(productoModel);
                return _mapper.Map<ProductoDTO>(productoModel);
            }
            catch
            {
                throw;
            }
        }
    }
}
