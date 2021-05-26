using System.Threading.Tasks;
using Eventuous.Projections.MongoDB;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using static Hotel.Bookings.Domain.Bookings.BookingEvents;

namespace Hotel.Bookings.Application.Projections {
    public class BookingProjection : MongoProjection<BookingDocument> {
        public BookingProjection(IMongoDatabase database, ILoggerFactory loggerFactory) :
            base(database, ProjectionSubscription.Id, loggerFactory) { }

        protected override ValueTask<Operation<BookingDocument>> GetUpdate(object evt) {
            return evt switch {
                V1.RoomBooked e => UpdateOperationTask(
                    e.BookingId,
                    update => update
                        .SetOnInsert(x => x.Id, e.BookingId)
                        .SetOnInsert(x => x.GuestId, e.GuestId)
                        .Set(x => x.CheckInDate, e.CheckInDate)
                        .Set(x => x.CheckOutDate, e.CheckOutDate)
                        .Set(x => x.Price, e.BookingPrice)
                        .Set(x => x.Outstanding, e.OutstandingAmount)
                ),
                _ => NoOp
            };
        }
    }
}