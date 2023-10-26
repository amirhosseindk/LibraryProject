﻿namespace Domain.Entities
{
    public class User
    {
        public int ID { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    public enum UserRole
    {
        Customer = 1,
        Member = 2,
        Staff = 3
    }
}