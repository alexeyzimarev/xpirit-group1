using System.Collections.Immutable;
using Eventuous;
using static Hotel.Bookings.Domain.Bookings.BookingEvents;

namespace Hotel.Bookings.Domain.Bookings {
    public record BookingState : AggregateState<BookingState, BookingId> {
        public StayPeriod Period      { get; init; }
        public Money      Price       { get; init; }
        public Money      Outstanding { get; init; }
        public bool       Paid        { get; init; }

        public ImmutableList<PaymentRecord> PaymentRecords { get; init; } = ImmutableList<PaymentRecord>.Empty;

        public override BookingState When(object evt) {
            return evt switch {
                V1.RoomBooked booked => this with {
                    Id = new BookingId(booked.BookingId),
                    Period = new StayPeriod(booked.CheckInDate, booked.CheckOutDate),
                    Price = new Money(booked.BookingPrice, booked.Currency),
                    Outstanding = new Money(booked.OutstandingAmount, booked.Currency),
                },
                V1.PaymentRecorded p => this with {
                    Outstanding = new Money(Outstanding.Amount - p.AmountPaid, p.Currency),
                    PaymentRecords = PaymentRecords.Add(
                        new PaymentRecord(p.PaymentId, new Money(p.AmountPaid, p.Currency))
                    )
                },
                V2.PaymentRecorded p => this with {
                    Outstanding = new Money(p.Outstanding, p.Currency),
                    PaymentRecords = PaymentRecords.Add(
                        new PaymentRecord(p.PaymentId, new Money(p.AmountPaid, p.Currency))
                    )
                },
                V1.BookingFullyPaid _ => this with {Paid = true},
                _                     => this
            };
        }
    }

    public record PaymentRecord(string PaymentId, Money Amount);
}