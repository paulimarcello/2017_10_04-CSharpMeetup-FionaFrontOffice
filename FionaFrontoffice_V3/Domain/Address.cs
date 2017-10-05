namespace FionaFrontoffice_V3.Domain
{
    internal class Address
    {
        public int Postleitzahl { get; private set; }
        public string Ort { get; private set; }
        public string Strasse { get; private set; }
        public int Hausnummer { get; private set; }
        public string HausnummerZusatz { get; private set; }


        public Address(int postleitzahl, string ort, string strasse, int hausnummer, string hausnummerZusatz)
        {
            Postleitzahl = postleitzahl;
            Ort = ort;
            Strasse = strasse;
            Hausnummer = hausnummer;
            HausnummerZusatz = hausnummerZusatz;

            // consider put all your validation logic here
        }
    }
}