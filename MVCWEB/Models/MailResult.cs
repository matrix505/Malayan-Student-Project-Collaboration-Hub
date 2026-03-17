namespace MVCWEB.Models
{
    public class MailResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public static MailResult Ok() => new() { Success = true };
        public static MailResult Fail(string error) => new() { Success = false, ErrorMessage = error };
    }
}
