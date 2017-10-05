using FionaFrontoffice_V3.Api;
using FionaFrontoffice_V3.Api.Command;
using FionaFrontoffice_V3.common;

namespace FionaFrontoffice_V3.EventListener
{
    internal class CustomerPublishedEventListener
    {
        private readonly IDomainEventPublisher _domainEventPublisher;
        private readonly ICustomerCommandService _customerCommandService;


        public CustomerPublishedEventListener(IDomainEventPublisher domainEventPublisher, ICustomerCommandService customerCommandService)
        {
            _domainEventPublisher = domainEventPublisher;
            _customerCommandService = customerCommandService;

            _domainEventPublisher.SubscribeOn<NewCustomerPublished>(ApplyNewCustomer);
        }

        private void ApplyNewCustomer(NewCustomerPublished @event)
        {
            var cmd = new ApplyNewCustomerCommand { Residence = @event.Address };

            _customerCommandService.Handle(cmd);
        }
    }
}