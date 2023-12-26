using System;
using System.Collections.Generic;

namespace AntaresProyecto.Models;

public partial class Area
{
    public int AreaId { get; set; }

    public string NombreArea { get; set; } = null!;

    public int? UnidadId { get; set; }

    public virtual UnidadesDeNegocio? Unidad { get; set; }
}
