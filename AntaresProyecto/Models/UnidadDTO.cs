namespace AntaresProyecto.Models
{
    public class UnidadDTO
    {
        public int UnidadId { get; set; }
        public string NombreUnidad { get; set; }
        public ICollection<AreaDTO> Areas { get; set; }
    }
}
