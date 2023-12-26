using System;
using System.Collections.Generic;

namespace AntaresProyecto.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string? Codigo { get; set; }

    public string? NombreProyecto { get; set; }

    public string? Cliente { get; set; }

    public string? Prioridad { get; set; }

    public string? Estado { get; set; }

    public DateTime? DiaInicio { get; set; }

    public DateTime? DiaFin { get; set; }

    public string? Etapa { get; set; }

    public string? LiderProyecto { get; set; }

    public string? Categorias { get; set; }

    public string? Comentarios { get; set; }
}
