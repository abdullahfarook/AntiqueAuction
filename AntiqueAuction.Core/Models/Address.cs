using System.Collections.Generic;
using AntiqueAuction.Shared.Domain;

namespace AntiqueAuction.Core.Models
{
    public class Address : ValueObject
    {
        public string Street { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string Country { get; protected set; }
        public string ZipCode { get; protected set; }

        public Address() { }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
