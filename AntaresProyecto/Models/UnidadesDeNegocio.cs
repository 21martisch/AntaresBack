using System;
using System.Collections.Generic;

namespace AntaresProyecto.Models;

public partial class UnidadesDeNegocio
{
    public int UnidadId { get; set; }

    public string NombreUnidad { get; set; } = null!;

    public virtual ICollection<Area> Areas { get;  } = new List<Area>();
}
