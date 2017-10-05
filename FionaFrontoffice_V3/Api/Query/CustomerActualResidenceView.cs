namespace FionaFrontoffice_V3.Api.Query
{
    // this view matches a screen on the UI
    public class CustomerActualResidenceView
    {
        public string CustomerId { get; set; }
        public int Postleitzahl { get; set; }
        public string Wohnort { get; set; }
        public string Strasse { get; set; }
        public int Hausnummer { get; set; }
        public string HausnummerZusatz { get; set; }

    }
}