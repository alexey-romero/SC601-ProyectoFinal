using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class UserTicketsController : Controller
    {
        public IActionResult MyRequests(string filter)
        {
            // Simulación de datos (estos datos TIENEN QUE VENIR DE LA BASE DE DATOS)
            var tickets = GetTickets();

            // Convierte el filtro a RequestStatus enum si es válido
            if (Enum.TryParse(filter, out RequestStatus status))
            {
                tickets = tickets.Where(t => t.Status == status).ToList();
            }

            return View(tickets);
        }

        public IActionResult MyApprovals(string filter)
        {
            // Si no se especifica un filtro, redirigir a la vista con el filtro "InProgress"

            if (string.IsNullOrEmpty(filter))
            {
                return RedirectToAction("MyApprovals", new { filter = "InProgress" });
            }

            var tickets = GetTickets();

            // Filtra los tickets por "In Progress" (Need Approval) y "Approved"
            if (filter == "InProgress")
            {
                tickets = tickets.Where(t => t.Status == RequestStatus.InProgress).ToList();
            }
            else if (filter == "Approved")
            {
                tickets = tickets.Where(t => t.Status == RequestStatus.Approved).ToList();
            }

            return View(tickets);
        }

        // Simulación de una lista de tickets
        private List<MyTickets> GetTickets()
        {
            return new List<MyTickets>
            {
                new MyTickets { IDRequest = 1, Title = "Issue with login", CreationDate = DateTime.Now.AddDays(-1), Status = RequestStatus.InProgress },
                new MyTickets { IDRequest = 2, Title = "Cannot access email",CreationDate = DateTime.Now.AddDays(-5), Status = RequestStatus.Closed },
                new MyTickets { IDRequest = 3, Title = "System running slow", CreationDate = DateTime.Now.AddDays(-2), Status = RequestStatus.InProgress },
                new MyTickets { IDRequest = 4, Title = "Need access to new software", CreationDate = DateTime.Now.AddDays(-3), Status = RequestStatus.Approved }
            };
        }
    }
}
