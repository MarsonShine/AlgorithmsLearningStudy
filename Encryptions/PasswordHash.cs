using System;
using System.Security.Cryptography;

namespace Encryptions {
    /// <summary>
    /// 基于密码密钥拓展函数算法   
    /// 用 PBKDF2-SHA1（Password-Based Key Derivation Function 2） salt(盐)加密
    /// </summary>
    public class PasswordHash {
        //在不破坏散列的情况下，下面常数是可以修改的
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        public static string CreateHash (string password) {
            //生成密码salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider ();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes (salt);
            //hash加密 以及参数转码
            byte[] hash = PKBDF2 (password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return PBKDF2_ITERATIONS + ":" +
                Convert.ToBase64String (salt) + ":" +
                Convert.ToBase64String (hash);
        }

        public static bool ValidatePassword (string password, string correctHash) {
            // 从 hash 中获取参数
            char[] delimiter = { ':' };
            string[] split = correctHash.Split (delimiter);
            int iterations = Int32.Parse (split[ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String (split[SALT_INDEX]);
            byte[] hash = Convert.FromBase64String (split[PBKDF2_INDEX]);

            byte[] testHash = PKBDF2 (password, salt, iterations, hash.Length);
            return SlowEquals (hash, testHash);
        }

        private static bool SlowEquals (byte[] a, byte[] b) {
            uint diff = (uint) a.Length ^ (uint) b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++) {
                diff |= (uint) (a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] PKBDF2 (string password, byte[] salt, int iterations, int outputBytes) {
            Rfc2898DeriveBytes pkbdf2 = new Rfc2898DeriveBytes (password, salt);
            pkbdf2.IterationCount = iterations;
            return pkbdf2.GetBytes (outputBytes);
        }
    }
}