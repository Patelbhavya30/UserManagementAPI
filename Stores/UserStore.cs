using UserManagementAPI.Models;

namespace UserManagementAPI.Stores
{
    public static class UserStore
    {
        public static List<User> Users { get; } = new();
    }
}
