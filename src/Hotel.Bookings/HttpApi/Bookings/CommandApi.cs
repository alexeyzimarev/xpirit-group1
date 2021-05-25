using System.Threading;
using System.Threading.Tasks;
using Hotel.Bookings.Application.Bookings;
using Microsoft.AspNetCore.Mvc;
using static Hotel.Bookings.Application.Bookings.BookingCommands;

namespace Hotel.Bookings.HttpApi.Bookings {
    [Route("/booking")]
    public class CommandApi : ControllerBase {
        readonly BookingsCommandService _service;

        public CommandApi(BookingsCommandService service) => _service = service;

    }
}
