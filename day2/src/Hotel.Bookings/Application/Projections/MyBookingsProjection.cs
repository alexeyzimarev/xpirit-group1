using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eventuous.Projections.MongoDB;
using Eventuous.Projections.MongoDB.Tools;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using static Hotel.Bookings.Domain.Bookings.BookingEvents;

namespace Hotel.Bookings.Application.Projections {
    public class MyBookingsProjection : MongoProjection<MyBookings> {
        public MyBookingsProjection(IMongoDatabase database, ILoggerFactory loggerFactory) :
            base(database, ProjectionSubscription.Id, loggerFactory) { }

        protected override ValueTask<Operation<MyBookings>> GetUpdate(object evt) {
            return evt switch {
                V1.RoomBooked e => UpdateOperationTask(
                    e.GuestId,
                    update => update.SetOnInsert(x => x.Id, e.GuestId)
                        .AddToSet(
                            x => x.Bookings,
                            new MyBooking(
                                e.RoomId,
                                e.CheckInDate,
                                e.CheckOutDate,
                                e.BookingPrice,
                                e.OutstandingAmount,
                                false
                            )
                        )
                ),
                _ => NoOp
            };
        }
    }

    public record MyBookings(string GuestId) : ProjectedDocument(GuestId) {
        public List<MyBooking> Bookings { get; init; } = new();
    }

    public record MyBooking(
        string         RoomId,
        DateTimeOffset CheckInDate,
        DateTimeOffset CheckOutDate,
        float          Price,
        float          Outstanding,
        bool           FullyPaid
    );
}