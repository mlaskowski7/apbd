using System;
using LegacyApp.DataAccessLayer;
using LegacyApp.DateTimeProviders;
using LegacyApp.Models;
using LegacyApp.Validators;


namespace LegacyApp
{
    public class UserService
    {
        
        private readonly IClientRepository _clientRepository;
        
        private readonly IUserCreditService _userCreditService;

        private readonly IUserValidator _userValidator;

        private readonly IUserRepository _userRepository;

        public UserService() 
            : this(
                  new ClientRepository(), 
                  new UserCreditService(), 
                  new UserValidator([new EmailValidator(), new NameValidator(), new AgeValidator(new StdDateTimeProvider())]),
                  new UserRepository())
        {
        }

        public UserService(
            IClientRepository clientRepository, 
            IUserCreditService userCreditService, 
            IUserValidator userValidator, 
            IUserRepository userRepository)
        {
            _userValidator = userValidator;
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
            _userRepository = userRepository;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var userDetails = new UserDetails(firstName, lastName, email, dateOfBirth, clientId);
            if (!_userValidator.ValidateUserDetails(userDetails))
            {
                return false;
            }

            var user = CreateUser(userDetails);
            _userCreditService.UpdateCreditLimit(user, user.Client);
             if (!_userValidator.ValidateUserCreditLimit(user)) return false;
            _userRepository.AddUser(user);
            return true;
        }

        private User CreateUser(UserDetails userDetails)
        {
            var client = _clientRepository.GetById(userDetails.ClientId);
            
            return new User()
            {
                Client = client,
                DateOfBirth = userDetails.DateOfBirth,
                EmailAddress = userDetails.Email,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName
            };
        }
    }
}
