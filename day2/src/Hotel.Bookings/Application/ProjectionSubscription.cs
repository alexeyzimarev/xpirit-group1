using System.Collections.Generic;
using EventStore.Client;
using Eventuous;
using Eventuous.Subscriptions;
using Eventuous.Subscriptions.EventStoreDB;
using Microsoft.Extensions.Logging;

namespace Hotel.Bookings.Application {
    public class ProjectionSubscription : AllStreamSubscription {
        public const string Id = "Projections";

        public ProjectionSubscription(
            EventStoreClient           eventStoreClient,
            ICheckpointStore           checkpointStore,
            IEnumerable<IEventHandler> eventHandlers,
            IEventSerializer           eventSerializer,
            ILoggerFactory             loggerFactory
        ) : base(
            eventStoreClient,
            Id,
            checkpointStore,
            eventHandlers,
            eventSerializer,
            loggerFactory
        ) { }
    }
}