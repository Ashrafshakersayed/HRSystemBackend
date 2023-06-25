using AutoMapper;
using HR_Employees.Dtos;
using HR_Employees.Entities;
using HR_Employees.Helpers;
using Microsoft.CodeAnalysis.Scripting;

namespace HR_Employees.Services
{

    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private DBContext _context;
        private readonly IMapper _mapper;

        public UserService(
            DBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}