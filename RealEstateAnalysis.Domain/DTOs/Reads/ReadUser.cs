using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadUser
    {
        public ReadUser(string id, string firstName, string lastName, string email, List<string> roles)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Roles = roles;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public List<string> Roles { get; }
    }
}