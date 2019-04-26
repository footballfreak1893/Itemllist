using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ItemList.Classes
{
    public static class AES
    {
        public static string key = "00000000000000000000000000000000";

        private static byte[] DoExtendKey(string key, int newKeyLength)
        {
            byte[] bkey = new byte[newKeyLength];
            byte[] tmpKey = Encoding.UTF8.GetBytes(key);

            for (int i = 0; i < key.Length; i++)
            {
                bkey[i] = tmpKey[i];
            }
            
            return bkey;
        }

        private static byte[] DoCreateBlocksize(int newBlockSize)
        {
            byte[] block = new byte[newBlockSize];
            for (byte i = 0; i < newBlockSize; i++)
            {
                block[i] = i;
            }

            return block;
        }

        public static string Encrypt(string cleartext, string key)
        {
            Aes AESCrypto = Aes.Create();
            AESCrypto.Key = DoExtendKey(key, 32);
            AESCrypto.IV = DoCreateBlocksize(16);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, AESCrypto.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] plainBytes = Encoding.UTF8.GetBytes(cleartext);
            cs.Write(plainBytes, 0, plainBytes.Length);
            cs.FlushFinalBlock();

            byte[] encryptedBytes = ms.ToArray();
           var crypttext = Convert.ToBase64String(encryptedBytes);

            ms.Close();
            cs.Close();
            return crypttext;
        }

        public static string Decrypt(string crypttext, string key)
        {
            string cleartext = null;
            Aes AESCrypto = Aes.Create();
            AESCrypto.Padding = PaddingMode.PKCS7;
            AESCrypto.Key = DoExtendKey(key, 32);
            AESCrypto.IV = DoCreateBlocksize(16);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, AESCrypto.CreateDecryptor(), CryptoStreamMode.Write);
            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(crypttext);
                cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                cs.FlushFinalBlock();

                byte[] plainBytes = ms.ToArray();
               cleartext = Encoding.UTF8.GetString(plainBytes);

            }
            catch (Exception)
            {

                Console.WriteLine("Decrption failed");
                return null;
            }

            ms.Close();
            cs.Close();
            return cleartext;
        }

        public static string CryptMenu(string text, char typ)
        {
            if (typ == 'e')
            {
                var crypttext = Encrypt(text, key);
                return crypttext;
            }
            if (typ == 'd')
            {
               var cleartext = Decrypt(text, key);
                return cleartext;
            }
            return null;
        }
    }
}
