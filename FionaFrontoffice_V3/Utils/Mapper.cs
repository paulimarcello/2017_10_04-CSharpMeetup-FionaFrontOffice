using FionaFrontoffice_V3.Api.Command;
using FionaFrontoffice_V3.Domain;

namespace FionaFrontoffice_V3.Utils
{
    internal class Mapper
    {
        public static Address GetAddressFromAdressDto(AddressDto dto)
        {
            return new Address(dto.Postleitzahl, dto.Ort, dto.Strasse, dto.Hausnummer, dto.HausnummerZusatz);
        }

        public static AddressDto GetAdressDtoFromAddress(Address address)
        {
            return new AddressDto
            {
                Postleitzahl = address.Postleitzahl,
                Ort = address.Ort,
                Strasse = address.Strasse,
                Hausnummer = address.Hausnummer,
                HausnummerZusatz = address.HausnummerZusatz
            };
        }
    }
}