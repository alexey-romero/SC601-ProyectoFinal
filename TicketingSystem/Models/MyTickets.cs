using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Models;
    public class MyTickets
{
    [Key]
    public int IDRequest { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; }

    [ForeignKey("RequestType")]
    public int ReqTypeId { get; set; }
    public virtual RequestType RequestType { get; set; }  // Relación con la tabla request_types

    [Required]
    public RequestStatus Status { get; set; }  // Enum que definiremos a continuación

    public string Description { get; set; }

    public DateTime? EstimatedDueDate { get; set; }  // Nullable

    public string AdminNotes { get; set; }

    public string ResolutionInfo { get; set; }

    [ForeignKey("User")]
    public int IdUser { get; set; }

    [ForeignKey("Manager")]
    public int IdManager { get; set; }

    [ForeignKey("Admin")]
    public int? IdAdmin { get; set; }  // Nullable, puede ser null hasta que un admin lo tome

    [Required]
     public DateTime CreationDate { get; set; }  // Nueva propiedad para almacenar la fecha de creación

    // Relaciones con otras tablas (Users)
    /* public virtual User User { get; set; }
     public virtual User Manager { get; set; }
     public virtual User Admin { get; set; } */
}

public class RequestType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string TypeName { get; set; }
}

// Enum para el estado del request
public enum RequestStatus
{
    InProgress,
    Approved,
    Rejected,
    Closed
}
