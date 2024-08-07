using DELAY.Core.Domain.Models.Base;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DELAY.Core.Domain.Models
{
    public class User : KeyNamedModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public DateTime ChangedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string ChangedBy {  get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Password) && IsValidEmail(Email) && IsValidPhoneNumber(PhoneNumber);
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

        public User(Guid id, string name) : base(id, name)
        {
        }

        public User(string name, string email, string phoneNumber, string password, string changedBy) : base(name)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            CreateDate = DateTime.UtcNow;
            ChangedDate = CreateDate;
            ChangedBy = changedBy;
        }

        public void Update(string name, string email, string phoneNumber, string changedBy)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            ChangedBy = changedBy;
            ChangedDate = DateTime.UtcNow;

            base.Update(name);
        }
    }
}
