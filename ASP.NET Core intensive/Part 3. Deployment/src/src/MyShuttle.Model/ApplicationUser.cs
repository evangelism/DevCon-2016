namespace MyShuttle.Model
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationUser : IdentityUser
    {
        public int CarrierId { get; set; }
    }
}