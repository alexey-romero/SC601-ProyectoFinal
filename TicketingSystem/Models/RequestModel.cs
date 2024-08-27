namespace TicketingSystem.Models
{
    public class RequestModel
    {
        //public int RequestTypeId { get; set; } 
        //public string RequestType { get; set; } 
        public string Status { get; set; }
        public string Title { get; set; } 
        public string SubjectManager { get; set; } 
        public string Description { get; set; }
        //public DateTime CreationDate { get; set; }
        public DateTime? EstimatedDueDate { get; set; }
        public IFormFile Attachment { get; set; } 
        public DateTime? EstimatedRevokingPermissionDate { get; set; }
        public string Notes { get; set; }
        public string ResolutionInformation { get; set; }
    }

}
