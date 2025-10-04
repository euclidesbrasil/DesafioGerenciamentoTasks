namespace ArquiteturaDesafio.Core.Application.UseCases.DTOs
{
    public class AddressDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Zipcode { get; set; }
        public GeolocationDto Geolocation { get; set; }
    }
} 