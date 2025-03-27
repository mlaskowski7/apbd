using LegacyApp.Models;

namespace LegacyApp.DataAccessLayer;

public interface IClientRepository
{ 
    Client GetById(int clientId);
}