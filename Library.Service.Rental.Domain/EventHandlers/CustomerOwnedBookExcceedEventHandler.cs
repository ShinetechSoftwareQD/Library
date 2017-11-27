using Library.Domain.Core;
using Library.Domain.Core.DataAccessor;
using Library.Domain.Core.Messaging;
using Library.Infrastructure.Core;
using Library.Service.Rental.Domain.DataAccessors;
using Library.Service.Rental.Domain.Events;
using System;
using System.Threading.Tasks;
using Library.Domain.Core.Attributes;
using Library.Domain.Core.Models;

namespace Library.Service.Rental.Domain.EventHandlers
{
    public class CustomerOwnedBookExcceedEventHandler : BaseRentalEventHandler<CustomerOwnedBookExcceedEvent>
    {
        public CustomerOwnedBookExcceedEventHandler(IRentalReportDataAccessor reportDataAccessor, ICommandTracker commandTracker, ILogger logger, IDomainRepository domainRepository, IEventPublisher eventPublisher) : base(reportDataAccessor, commandTracker, logger, domainRepository, eventPublisher)
        {

        }
        
        public override void Handle(CustomerOwnedBookExcceedEvent evt)
        {
            try
            {
                AddEventLogAndSendToTracker(evt, "CUSTOMEOWNEDBOOK_EXCCEED");
            }
            catch (Exception ex)
            {
                AddEventLog(evt, "SERVER_ERROR", ex.ToString());
            }
        }
    }
}