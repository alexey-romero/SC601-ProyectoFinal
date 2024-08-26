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
    }
}
