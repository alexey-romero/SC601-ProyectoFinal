using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class CreateRequestController : Controller
    {
        private readonly IRequestService _requestService;

        public CreateRequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            return View("/Views/Home/CreateRequest.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestModel model)
        {
            var requestTypes = await _requestService.GetRequestTypes();
            if (requestTypes != null)
            {
                ViewBag.RequestTypes = requestTypes;
            }
            return View(model);
        }

        // TEST THAT THE PROGRAM RECOGNIZES THE REQUEST TYPES
        // https://localhost:7154/api/request-types
        [HttpGet("api/request-types")]
        public async Task<IActionResult> GetRequestTypesApi()
        {
            var requestTypes = await _requestService.GetRequestTypes();
            return Ok(requestTypes);
        }

        // TEST THAT THE PROGRAM RECOGNIZES THE REQUEST STATUSES
        // https://localhost:7154/api/request-statuses
        [HttpGet("api/request-statuses")]
        public async Task<IActionResult> GetRequestStatusesApi()
        {
            var requestTypes = await _requestService.GetRequestStatuses();
            return Ok(requestTypes);
        }

        // TEST THAT THE PROGRAM RECOGNIZES THE UNASSIGNED REQUESTS
        [HttpGet("api/requests")]
        public async Task<IActionResult> GetRequests([FromQuery] string q)
        {
            if (q == "unassigned")
            {
                var unassignedRequests = await _requestService.GetAllUnassignedRequests();
                return Ok(unassignedRequests);
            }
            else if (q == null)
            {
                var requestTypes = await _requestService.GetAllRequests();
                return Ok(requestTypes);

            }

            return BadRequest("Invalid query parameter.");
        }

        [HttpGet("api/requests/{userId}")]
        public async Task<IActionResult> GetRequestsByUserIdApi(int userId)
        {
            try
            {
                var requests = await _requestService.GetRequestsByUserId(userId);
                if (requests == null || !requests.Any())
                {
                    return NotFound("No requests found for the specified user.");
                }
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request." + ex);
            }
        }

    }
}
