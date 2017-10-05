namespace FionaFrontoffice_V3.Api.Command
{
    public class AddressDto
    {
        public int Postleitzahl { get; set; }
        public string Ort { get; set; }
        public string Strasse { get; set; }
        public int Hausnummer { get; set; }
        public string HausnummerZusatz { get; set; }
    }
}