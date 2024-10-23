using AutoMapper;
using HealthcareAPI.BusinessAccessLayer.Models;
using HealthcareAPI.DataAccessLayer.Contract;
using HealthcareAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using HealthcareAPI.BusinessAccessLayer.Contract;

namespace HealthcareAPI.BusinessAccessLayer.Implementation
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RegisterService(IRegisterRepository repository, IConfiguration configuration, IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="register">Register user information</param>
        /// <returns></returns>
        public ServiceResponse<string> RegisterUserService(UserModel register)
        {
            var response = new ServiceResponse<string>();
            var message = string.Empty;
            if (register != null)
            {
                message = CheckPasswordStrength(register.Password);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    response.Success = false;
                    response.Message = message;
                    return response;
                }
                else if (_repository.UserExists(register.Email))
                {
                    response.Success = false;
                    response.Message = "User already exists.";
                    return response;
                }
                else
                {
                    using var memoryStream = new MemoryStream();


                    var user = _mapper.Map<User>(register);

                    CreatePasswordhash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    var result = _repository.RegisterUser(user);
                    response.Success = result;
                    response.Message = result ? "Registration successfully..!!" : "Something went wrong, please try after sometime.";
                }
            }

            return response;
        }

        public ServiceResponse<string> LoginUserService(Login login)
        {
            var response = new ServiceResponse<string>();

            var user = _repository.ValidateUser(login.Email);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid username or password!";
                return response;
            }
            else if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Invalid username or password!";
                return response;
            }

            string token = CreateToken(user);
            response.Data = token;
            return response;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Email,user.Email.ToString()),
                 new Claim("id", user.Id.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (password.Length < 8)
            {
                stringBuilder.Append("Mininum password length should be 8" + Environment.NewLine);
            }
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                stringBuilder.Append("Password should be apphanumeric" + Environment.NewLine);
            }
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,*,(,),_,]"))
            {
                stringBuilder.Append("Password should contain special characters" + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        private void CreatePasswordhash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
