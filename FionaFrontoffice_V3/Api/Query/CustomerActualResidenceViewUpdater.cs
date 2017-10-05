using FionaFrontoffice_V3.Domain;
using FionaFrontoffice_V3.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace FionaFrontoffice_V3.Api.Query
{
    internal class CustomerActualResidenceViewUpdater : ISearchCustomerView
    {
        public IEnumerable<CustomerActualResidenceView> CoolSearchEngine { get { return _databaseTable; } }

        private readonly DomainEventPublisher _domainEventPublisher;
        private readonly List<CustomerActualResidenceView> _databaseTable = new List<CustomerActualResidenceView>();


        public CustomerActualResidenceViewUpdater(DomainEventPublisher domainEventPublisher)
        {
            _domainEventPublisher = domainEventPublisher;

            _domainEventPublisher.SubscribeOn<NewCustomerApplied>(UpdateSearchCustomerView);
            _domainEventPublisher.SubscribeOn<NewResidenceApplied>(UpdateSearchCustomerView);
        }


        private void UpdateSearchCustomerView(NewCustomerApplied @event)
        {
            _databaseTable.Add(new CustomerActualResidenceView
            {
                CustomerId = @event.CustomerId.ToString(),
                Postleitzahl = @event.Address.Postleitzahl,
                Wohnort = @event.Address.Ort,
                Strasse = @event.Address.Strasse,
                Hausnummer = @event.Address.Hausnummer,
                HausnummerZusatz = @event.Address.HausnummerZusatz,
            });
        }

        private void UpdateSearchCustomerView(NewResidenceApplied @event)
        {
            var knownCustomer = _databaseTable.Single(c => c.CustomerId.Equals(@event.CustomerId.ToString()));

            knownCustomer.Postleitzahl = @event.NewResidence.Postleitzahl;
            knownCustomer.Wohnort = @event.NewResidence.Ort;
            knownCustomer.Strasse = @event.NewResidence.Strasse;
            knownCustomer.Hausnummer = @event.NewResidence.Hausnummer;
            knownCustomer.HausnummerZusatz = @event.NewResidence.HausnummerZusatz;
        }
    }
}