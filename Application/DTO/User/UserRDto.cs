using Domain.Entities;

namespace Application.DTO.User
{
    public class UserRDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRoles UserRole { get; set; }
    }
}