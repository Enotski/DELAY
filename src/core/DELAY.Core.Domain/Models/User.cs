using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DELAY.Core.Domain.Models
{
    public class User : KeyModel, IName
    {
        public string? Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string? Password { get; set; }

        public DateTime ChangedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string ChangedBy { get; set; }
        public RoleType Role { get; set; }

        public bool IsValidCredentials()
        {
            if (string.IsNullOrWhiteSpace(Password))
                return false;

            bool isValidLogin = false;

            if (!string.IsNullOrWhiteSpace(Email))
            {
                isValidLogin = IsValidEmail(Email);
            }

            if (!string.IsNullOrWhiteSpace(PhoneNumber))
            {
                isValidLogin = IsValidPhoneNumber(PhoneNumber);
            }

            return isValidLogin;
        }

        bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            try
            {
                return Regex.IsMatch(phoneNumber,
                    @"^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public User(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public User(string name, string email, string phoneNumber, string password, string changedBy)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            CreateDate = DateTime.UtcNow;
            ChangedDate = CreateDate;
            ChangedBy = changedBy;
            Role = RoleType.User;
        }
        public User(string name, string email, string phoneNumber, string password)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            CreateDate = DateTime.UtcNow;
            ChangedDate = CreateDate;
            Role = RoleType.User;
        }

        public User()
        {
        }

        public void Update(string name, string email, string phoneNumber, RoleType role, string changedBy)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            ChangedBy = changedBy;
            ChangedDate = DateTime.UtcNow;
            Role = role;
            Name = name;
        }
    }
}
