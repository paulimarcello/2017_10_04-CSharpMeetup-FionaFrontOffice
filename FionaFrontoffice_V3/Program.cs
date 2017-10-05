using System;
using FionaFrontoffice_V3.Api.Command;
using FionaFrontoffice_V3.EventListener;
using System.Linq;
using FionaFrontoffice_V3.Domain;

namespace FionaFrontoffice_V3
{
    class Program
    {
        private static App _app = new App();

        static void Main(string[] args)
        {

            //Demo1();

            // Demo1b();

            Demo2();

            Console.ReadLine();
        }

        private static void Demo1()
        {
            var newCustomerCmd = new ApplyNewCustomerCommand
            {
                Residence = new AddressDto
                {
                    Postleitzahl = 22880,
                    Ort = "Wedel",
                    Strasse = "Hacker Str.",
                    Hausnummer = 2,
                    HausnummerZusatz = "b"
                }
            };

            _app.CustomerCommandService.Handle(newCustomerCmd);
        }

        private static void Demo1b()
        {
            Demo1();

            // this is just for demo to simulate a call on a new instantiated process manager
            _app.CustomerAccountManagerMatchingProcessManager.Reset();

            var customerId = _app.SearchCustomerView.CoolSearchEngine.Single(c => c.Postleitzahl == 22880).CustomerId;

            var newResidendeCmd = new AnnounceNewResidenceCommand()
            {
                CustomerId = customerId,
                NewResidence = new AddressDto()
                {
                    Postleitzahl = 22880,
                    Ort = "Wedel",
                    Strasse = "Hacker-Str.",
                    Hausnummer = 2,
                    HausnummerZusatz = "b"
                }
            };

            _app.CustomerCommandService.Handle(newResidendeCmd);
        }

        private static void Demo2()
        {
            var publisher = _app.BullshitEnterpriseServiceBus;


            var @event = new NewCustomerPublished
            {
                Address = new AddressDto
                {
                    Postleitzahl = 22880,
                    Ort = "Wedel",
                    Strasse = "Hacker Str.",
                    Hausnummer = 2,
                    HausnummerZusatz = "b"
                }
            };

            publisher.Publish(@event);
        }
    }
}
