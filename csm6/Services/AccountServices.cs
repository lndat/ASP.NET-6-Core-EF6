using csm6.Data;
using csm6.Models;
using csm6.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace csm6.Services
{
    public class AccountServices
    {
        private static readonly CSMContext _context = new CSMContext();

        public static string CreatePasswordHash(string password)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha512.ComputeHash(bytes);

            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) result.Append(hash[i].ToString("X2"));
            return result.ToString();
        }

        public static string CreateSalt(int saltLength)
        {
            var s = "";
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            using (var provider = new RNGCryptoServiceProvider())
            {
                while (s.Length != saltLength)
                {
                    var oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    var character = (char)oneByte[0];
                    if (validChars.Contains(character)) s += character;
                }
            }
            return s;
        }

        public static bool EmailCheck(string email)
        {
            var checkEmail = _context.Members.Where(x => x.Email == email);
            if (checkEmail.Count() > 0) return true;
            return false;
        }

        public static Member RegisterMember(MemberViewModel memberVM)
        {
            var newMember = new Member();
            var salt = CreateSalt(20);

            newMember.FirstName = memberVM.FirstName;
            newMember.LastName = memberVM.LastName;
            newMember.Email = memberVM.Email;
            newMember.PasswordSalt = salt;
            newMember.PasswordHash = AccountServices.CreatePasswordHash(memberVM.Password + salt);

            return newMember;
        }

        public static bool CanLogin(string password, string email)
        {
            var findEmail = _context.Members.Where(x => x.Email == email);

            if (findEmail.Count() > 0)
            {
                var salt = _context.Members.Where(x => x.Email == email).Select(s => s.PasswordSalt).Single();
                var hashPassword = CreatePasswordHash(password + salt);
                var checkPasswordHash = _context.Members.Where(x => x.PasswordHash == hashPassword);

                if (checkPasswordHash.Count() > 0) 
                    return true;

                return false;
            }
            return false;          
        }
    }
}