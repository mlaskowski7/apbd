using System;
using LegacyApp.DataAccessLayer;
using LegacyApp.DateTimeProviders;
using LegacyApp.Exceptions;
using LegacyApp.Models;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        
        private readonly IClientRepository _clientRepository;
        
        private readonly IUserCreditService _userCreditService;

        public UserService() 
            : this(new StdDateTimeProvider(), new ClientRepository(), new UserCreditService())
        {
        }

        public UserService(IDateTimeProvider dateTimeProvider, IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            _dateTimeProvider = dateTimeProvider;
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
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
            
            UpdateCreditLimitBasedOnUsersClientType(user);
            return AddUserIfEnoughCreditLimit(user);
        }

        private bool AddUserIfEnoughCreditLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private void UpdateCreditLimitBasedOnUsersClientType(User user)
        {
            var clientType = user.Client.Type;

            if (clientType == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                var creditLimit = GetCreditLimitForUser(user);
                user.HasCreditLimit = true;
                user.CreditLimit = clientType == "ImportantClient" ? 
                                                    creditLimit * 2 : 
                                                    creditLimit;
            }
        }

        private int GetCreditLimitForUser(User user)
        {
            try
            {
                var creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                return creditLimit;
            }
            catch (ArgumentException argumentException)
            {
                throw new UserCreditNotFoundException("Could not find user's credit limit by his last name", argumentException);
            }
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
            
            var now = _dateTimeProvider.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                throw new UserValidationException("Age of the user must be over 21.");
            }
        }
    }
}
