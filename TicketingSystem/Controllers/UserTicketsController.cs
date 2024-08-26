using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using RepositoryLayer.Models;
using System.Collections.Generic;

namespace TicketingSystem.Controllers
{
    public class UserTicketsController : Controller
    {
        private readonly IRequestService _requestService;

        public UserTicketsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // Acción para listar todos los tickets del usuario (MyRequests)
        public async Task<IActionResult> MyRequests(string filter = "All")
        {
            var userId = User.FindFirstValue("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var requests = await _requestService.GetRequestsByUserId(int.Parse(userId));

            // Asignar la fecha de creación si no está definida
            foreach (var request in requests)
            {
                if (request.CreationDate == default)
                {
                    request.CreationDate = DateTime.Now;
                }
            }

            // Filtrar según el estado
            if (filter == "InProgress")
            {
                requests = requests.Where(r => r.RequestStatusNavigation.Status == "In Progress").ToList();
            }
            else if (filter == "Closed")
            {
                requests = requests.Where(r => r.RequestStatusNavigation.Status == "Closed").ToList();
            }

            return View(requests);
        }

        // Acción para listar los tickets en MyApprovals (estado visual de aprobación)
        public async Task<IActionResult> MyApprovals(string filter = "InProgress")
        {
            // Obtiene el ID del usuario autenticado desde los claims
            var userId = User.FindFirstValue("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); // Manejar el caso en que no se puede obtener el ID del usuario
            }

            var requests = await _requestService.GetRequestsByUserId(int.Parse(userId));

            // Asignar la fecha de creación si no está definida
            foreach (var request in requests)
            {
                if (request.CreationDate == default)
                {
                    request.CreationDate = DateTime.Now;
                }
            }

            // Filtrado de requests según el estado
            List<Request> filteredRequests = requests;

            if (filter == "InProgress")
            {
                filteredRequests = requests.Where(r => r.RequestStatusNavigation.Status == "In Progress").ToList();
            }
            else if (filter == "Approved")
            {
                filteredRequests = requests.Where(r => r.RequestStatusNavigation.Status == "Approved").ToList();
            }

            return View(filteredRequests);
        }
    }
}
