namespace FionaFrontoffice_V3.Api.Command
{
    public class AnnounceNewResidenceCommand : common.Command
    {
        public string CustomerId { get; set; }
        public AddressDto NewResidence { get; set; }
    }
}