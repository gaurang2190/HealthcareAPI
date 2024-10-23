using HealthcareAPI.DataAccessLayer.Contract;
using HealthcareAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAPI.DataAccessLayer.Implementation
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RegisterRepository(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public bool RegisterUser(User user)
        {
            var result = false;
            if (user != null)
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                result = true;
            }
            return result;
        }
        public bool UserExists(string email)
        {
            if (_dbContext.Users.Where(m => m.Email == email).Any())
            {
                return true;
            }
            return false;
        }
        public User? ValidateUser(string username)
        {
            User? user = _dbContext.Users.Where(m => m.Email == username).FirstOrDefault();
            return user;
        }

        public bool UpdateProfile(User userProfile)
        {
            var result = false;
            if (userProfile != null)
            {
                _dbContext.Entry(userProfile).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public User GetUserById(int id)
        {
            var ressult = _dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            return ressult != null ? ressult : new User();
        }
    }
}
