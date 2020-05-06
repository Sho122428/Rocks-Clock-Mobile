using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace RockClockMobile.Security
{
    public static class Security
    {
        private static SecureString Seed = Security.ConvertToSecureString("r0ck$cl0ck");

        public static string ConvertToUnsecureString(this SecureString securePassword)
        {
            try
            {
                if (securePassword == null)
                    throw new ArgumentNullException("Secure password cannot be null");

                IntPtr unmanangedString = IntPtr.Zero;
                try
                {
                    unmanangedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                    return Marshal.PtrToStringUni(unmanangedString);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanangedString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SecureString ConvertToSecureString(this string password)
        {
            try
            {
                if (password == null)
                    throw new ArgumentNullException("Password cannot be null");

                SecureString sc = new SecureString();

                foreach (char item in password)
                {
                    sc.AppendChar(item);
                }

                sc.MakeReadOnly();
                return sc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string ReturnEncryptedPassword(this SecureString unencryptedPassword, string salt)
        {
            try
            {
                return CryptoAES.Encrypt(unencryptedPassword, salt, Seed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SecureString ReturnDecryptedPassword(this string password)
        {
            try
            {
                return CryptoAES.Decrypt(password, Seed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public static class CryptoAES
    {
        public static string Encrypt(this SecureString strText, string salt, SecureString strEncrKey)
        {
            try
            {
                AesCryptoServiceProvider aesSP = new AesCryptoServiceProvider();

                SHA256 sha = SHA256.Create();

                aesSP.Key = sha.ComputeHash(UnicodeEncoding.Unicode.GetBytes(strEncrKey.ConvertToUnsecureString()));
                aesSP.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, 0x2d, 0x3a, 0x5e, 0x17, 0xcb, 0xf, 0x41, 0x44 };

                byte[] bite = UnicodeEncoding.Unicode.GetBytes(salt + strText.ConvertToUnsecureString());
                ICryptoTransform t = aesSP.CreateEncryptor();
                return Convert.ToBase64String(t.TransformFinalBlock(bite, 0, bite.Length));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SecureString Decrypt(this string strText, SecureString strDecrKey)
        {
            try
            {
                AesCryptoServiceProvider aesSP = new AesCryptoServiceProvider();
                SHA256 sha = SHA256.Create();

                aesSP.Key = sha.ComputeHash(UnicodeEncoding.Unicode.GetBytes(strDecrKey.ConvertToUnsecureString()));
                aesSP.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, 0x2d, 0x3a, 0x5e, 0x17, 0xcb, 0xf, 0x41, 0x44 };

                byte[] bite = Convert.FromBase64String(strText);

                ICryptoTransform t = aesSP.CreateDecryptor();
                byte[] bite2 = t.TransformFinalBlock(bite, 0, bite.Length);
                return UnicodeEncoding.Unicode.GetString(bite2).ConvertToSecureString();

            }
            catch (CryptographicException)
            {
                throw new Exception("Invalid Entry (l1)");
            }
            catch (FormatException)
            {
                throw new Exception("Invalid Entry (f1)");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private const int SALT_BYTE_SIZE = 24;
        public static string CreateSalt()
        {
            byte[] salt = new byte[SALT_BYTE_SIZE];
            using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
            {
                csprng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}
