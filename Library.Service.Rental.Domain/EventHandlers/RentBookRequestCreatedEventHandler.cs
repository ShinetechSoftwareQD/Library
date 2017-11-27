
using Library.Domain.Core;
using Library.Domain.Core.DataAccessor;
using Library.Domain.Core.Messaging;
using Library.Infrastructure.Core;
using Library.Service.Rental.Domain.DataAccessors;
using Library.Service.Rental.Domain.EventHandlers;
using Library.Service.Rental.Domain.Events;
using System;
using System.Threading.Tasks;

namespace Library.Service.Rental.Domain
{
    public class RentBookRequestCreatedEventHandler : BaseRentalEventHandler<RentBookRequestCreatedEvent>
    {
        public RentBookRequestCreatedEventHandler(IRentalReportDataAccessor reportDataAccessor, ICommandTracker commandTracker, ILogger logger, IDomainRepository domainRepository, IEventPublisher eventPublisher) : base(reportDataAccessor, commandTracker, logger, domainRepository, eventPublisher)
        {

        }

        public override void Handle(RentBookRequestCreatedEvent evt)
        {
            try
            {
                _reportDataAccessor.CreateRentBookRequest(evt.BookInventoryId, evt.BookName, evt.ISBN, evt.AggregateId, evt.Name, evt.RentDate);
                _reportDataAccessor.Commit();

                _eventPublisher.Publish(new RentBookRequestAcceptedEvent
                {
                    AggregateId = evt.BookInventoryId,
                    CommandUniqueId = evt.CommandUniqueId,
                    Notes = $"Rent by {evt.Name.FirstName} {evt.Name.LastName} at {evt.RentDate.ToString("yyyy-MM-dd HH:mm:ss")}",
                    CustomerId = evt.AggregateId
                });

                AddEventLog(evt, "RENTBOOKREQUEST_CREATED");
            }
            catch (Exception ex)
            {
                AddEventLog(evt, "SERVER_ERROR", ex.ToString());
            }
        }
    }
}