using Autenticacion.BLL.Custom;
using Autenticacion.BLL.Servicios.Contrato;
using Autenticacion.DAL.Repositorio.Contrato;
using Autenticacion.DTO;
using Autenticacion.Model;
using AutoMapper;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _repositorio; // <1>
        private readonly IMapper _mapper; // <2>
        private readonly SecurityEncript _securityEncript;

        public UsuarioService(IGenericRepository<Usuario> repositorio, IMapper mapper, SecurityEncript securityEncript) // <3>
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _securityEncript = securityEncript;
        }
        public async Task<Usuario> Login(LoginDTO loginDTO)
        {
            var usuarioEncontrado=await _repositorio.Obtener(u=>u.Correo==loginDTO.Correo && u.Clave==_securityEncript.encriptarSHA256(loginDTO.Clave!));

            if (usuarioEncontrado!=null)
            {
                return usuarioEncontrado;
            }
            else
            {
                return null!;
            }
        }

        public async Task<UsuarioDTO> registrar(UsuarioDTO usuario)
        {
            usuario.Clave = _securityEncript.encriptarSHA256(usuario.Clave!);

            var existeUser= _repositorio.Obtener(u=>u.Correo==usuario.Correo);

            try
            {
                if (existeUser==null)
                {
                    var usuarioModel = _mapper.Map<Usuario>(usuario);
                    await _repositorio.Crear(usuarioModel);
                    return _mapper.Map<UsuarioDTO>(usuarioModel);
                }
                else
                {
                   throw new TaskCanceledException("Este correo ya existe");
                }
                
            }
            catch
            {

                throw;
            }
        }
    }
}
