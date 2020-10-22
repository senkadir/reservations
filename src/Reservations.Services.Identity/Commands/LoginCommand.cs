namespace Reservations.Services.Identity.Commands
{
    public class LoginCommand
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Location { get; set; }
    }
}
