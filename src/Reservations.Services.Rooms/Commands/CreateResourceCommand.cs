namespace Reservations.Services.Rooms.Commands
{
    public class CreateResourceCommand
    {
            public bool Specific { get; set; }

            public string Name { get; set; }

            public int TotalQuantity { get; set; }
    }
}
