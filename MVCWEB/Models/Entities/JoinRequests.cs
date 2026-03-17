namespace MVCWEB.Models.Entities
{
    public class JoinRequests
    {
        public int? Request_id { get; set; }
        public int? Project_id { get; set; }
        public int? User_id { get; set; }
        public string? Status { get; set; }
        public DateTime? RequestedAt { get; set; }
    }
}
