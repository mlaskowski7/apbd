﻿namespace LegacyApp.Models
{
    public class Client
    {
        public Client(string name, int clientId, string email, string address, string type)
        {
            Name = name;
            ClientId = clientId;
            Email = email;
            Address = address;
            Type = type;
        }

        public Client()
        {
        }

        public string Name { get; internal set; }
        public int ClientId { get; internal set; }
        public string Email { get; internal set; }
        public string Address { get; internal set; }
        public string Type { get; set; }
    }
}