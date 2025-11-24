using Microsoft.AspNetCore.Identity;

namespace CarManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Можеш да добавиш допълнителни полета
        public string? FullName { get; set; }
    }
}