namespace TicketingSystem.Models
{
    public class RequestModel
    {
        public int RequestTypeId { get; set; } // To hold the selected request type ID
        public string RequestType { get; set; } // Optional: To display the selected request type name

        public string Status { get; set; } // To hold the request status
        public string Title { get; set; } // To hold the request title
        public string SubjectManager { get; set; } // To hold the manager's name
        public string Description { get; set; } // To hold the request description
        public DateTime? DueDate { get; set; } // To hold the estimated due date
        public IFormFile Attachment { get; set; } // For file attachments
        public DateTime? RevokingDate { get; set; } // To hold the estimated revoking permission date
        public string Notes { get; set; } // For additional notes
        public string ResolutionInformation { get; set; } // For resolution information
    }

}
