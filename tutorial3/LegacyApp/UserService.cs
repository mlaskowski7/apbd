using System;
using LegacyApp.DateTimeProviders;
using LegacyApp.Exceptions;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IDateTimeProvider _dateTime;
        
        private readonly IClientRepository _clientRepository;

        public UserService() : this(new StdDateTimeProvider(), new ClientRepository())
        {
        }

        public UserService(IDateTimeProvider dateTime, IClientRepository clientRepository)
        {
            _dateTime = dateTime;
            _clientRepository = clientRepository;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            User user;
            try
            {
                user = TryCreateUser(firstName, lastName, email, dateOfBirth, clientId);
            }
            catch (UserValidationException userValidationException)
            {
                Console.WriteLine("User details validation due to the following reason - " + userValidationException.Message);
                return false;
            }
            
            if (user.Client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (user.Client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private User TryCreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            ValidateUserDetails(firstName, lastName, email, dateOfBirth);
            
            Client client;
            try
            {
                client = _clientRepository.GetById(clientId);
            }
            catch (ArgumentException)
            {
                throw new UserValidationException($"Client with provided id({clientId}) is not present in the database.");
            }
            
            return new User()
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }

        private void ValidateUserDetails(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new UserValidationException("Cannot create a user with empty first or/and last name.");
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                throw new UserValidationException($"Invalid email address was passed ({email}).");
            }
            
            var now = _dateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                throw new UserValidationException("Age of the user must be under 21.");
            }
        }
    }
}
