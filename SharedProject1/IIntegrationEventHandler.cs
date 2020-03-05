using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreTransactionsReceiver.IntegrationEvents
{
    public interface IIntegrationEventHandler<TEvent>
        where TEvent : IntegrationEvent
    {
        Task Handle(TEvent @event);
        
    }
}
