using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Models;
using ServiceLayer;
using System.Security.Claims;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class CreateRequestController(IRequestService requestService) : Controller
    {
        private readonly IRequestService _requestService = requestService;

        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestModel model)
        {
            //var requestTypes = await _requestService.GetRequestTypes();
            //if (requestTypes != null)
            //{
            //    ViewBag.RequestTypes = requestTypes;
            //}

            Console.WriteLine(model);

            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID not found or invalid.");
            }

            
                var request = new RepositoryLayer.Models.Request
                {
                    RequestType = 1,
                    RequestStatus = 1,
                    Title = model.Title,
                    Description = model.Description,
                    IdUser = userId,
                };

                bool result = await _requestService.CreateRequest(request);

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }

            ModelState.AddModelError("", "An error occurred while creating the request.");
            return View(model);
        }

        /**
         * ENDPOINTS PARA PROBAR LA DATA SIN TENER QUE ACCEDER AL SISTEMA COMO TAL
         * **/

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

        // TEST THAT THE PROGRAM RECOGNIZES BOTH ALL REQUESTS AND UNASSIGNED REQUESTS WITH QUERY PARAM
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

        // TEST THAT THE PROGRAM RECOGNIZES THE REQUESTS BASED ON USERID
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
