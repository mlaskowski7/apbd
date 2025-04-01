using LegacyApp.Models;
using System;

namespace LegacyApp;

public interface IUserCreditService : IDisposable
{
    int GetCreditLimit(string lastName, DateTime dateOfBirth);

    void UpdateCreditLimit(User user, Client client);
}