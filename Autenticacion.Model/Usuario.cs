using System;
using System.Collections.Generic;

namespace Autenticacion.Model;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }
}
