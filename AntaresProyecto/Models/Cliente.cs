using System;
using System.Collections.Generic;

namespace AntaresProyecto.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string? Nombre { get; set; }

    public bool? Estado { get; set; }
}
