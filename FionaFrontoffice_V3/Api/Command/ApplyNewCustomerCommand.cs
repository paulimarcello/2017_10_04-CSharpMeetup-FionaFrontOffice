namespace FionaFrontoffice_V3.Api.Command
{
    public class ApplyNewCustomerCommand : common.Command
    {
        public AddressDto Residence { get; set; }
    }
}