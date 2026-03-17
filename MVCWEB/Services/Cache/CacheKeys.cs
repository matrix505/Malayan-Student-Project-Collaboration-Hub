namespace MVCWEB.Services.Cache
{
    public class CacheKeys
    {
        // User
        public static string EmailExists(string email)
            => $"email:exists:{email.ToLower().Trim()}";

        public static string UserProfile(int userId)
            => $"user:profile:{userId}";

        public static string UserRoles(string userId)
            => $"user:roles:{userId}";

        // Notifications
        public static string UnreadCount(string userId)
            => $"notif:unread:{userId}";

        // Chat
        public static string RecentMessages(string teamId)
            => $"chat:recent:{teamId}";

        // App
        public static string AppSettings
            => "app:settings";

        public static string Categories
            => "categories:all";
    }
}
