namespace MVCWEB.ViewModel.ForProject
{
    public class ProjectDiscussionViewModel
    {
        public int? Topic_id { get; set; }
        public string? Title { get; set; }
        public string? CreatedBy { get; set; }
        public string? Description { get; set; }
        public bool? IsClosed { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
