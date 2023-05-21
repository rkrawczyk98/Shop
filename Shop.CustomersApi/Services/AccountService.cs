//using Shop.UsersApi.Interfaces;
//using Shop.UsersApi.Models;

//namespace Shop.UsersApi.Services
//{
//    public class AccountService : IAccountService
//    {
//        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> CreateRoleAsync(ApplicationRole role)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> CreateUserAsync(ApplicationUser user, IEnumerable<string> roles, string password)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> DeleteRoleAsync(ApplicationRole role)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> DeleteRoleAsync(string roleName)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> DeleteUserAsync(ApplicationUser user)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> DeleteUserAsync(string userId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApplicationRole> GetRoleByIdAsync(string roleId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApplicationRole> GetRoleByNameAsync(string roleName)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApplicationRole> GetRoleLoadRelatedAsync(string roleName)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<ApplicationRole>> GetRolesLoadRelatedAsync(int page, int pageSize)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<ApplicationUser, string[]>> GetUserAndRolesAsync(string userId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApplicationUser> GetUserByEmailAsync(string email)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApplicationUser> GetUserByIdAsync(string userId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ApplicationUser> GetUserByUserNameAsync(string userName)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Tuple<ApplicationUser, string[]>>> GetUsersAndRolesAsync(int page, int pageSize)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> ResetPasswordAsync(ApplicationUser user, string newPassword)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> UpdatePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> UpdateRoleAsync(ApplicationRole role, IEnumerable<string> claims)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> UpdateUserAsync(ApplicationUser user)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Tuple<bool, string[]>> UpdateUserAsync(ApplicationUser user, IEnumerable<string> roles)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
