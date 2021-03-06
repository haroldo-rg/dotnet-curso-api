using System;
using System.IO;
using System.Security.Cryptography;

namespace Curso.Api.Infrastructure.Tools
{
    public static class RijndaelManagedCryptography
    {

        public static byte[] EncryptString(string plainText, string Key, string IV)
        {
            // Check arguments
            if (String.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");

            if (String.IsNullOrEmpty(Key))
                throw new ArgumentNullException("Key");

            if (String.IsNullOrEmpty(IV))
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            // Create an Rijndael object with the specified key and IV
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Convert.FromBase64String(Key);
                rijAlg.IV = Convert.FromBase64String(IV);

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static string DecryptString(byte[] cipherText, string Key, string IV)
        {
            // Check arguments
            if (cipherText == null)
                throw new ArgumentNullException("cipherText");

            if (String.IsNullOrEmpty(Key))
                throw new ArgumentNullException("Key");

            if (String.IsNullOrEmpty(IV))
                throw new ArgumentNullException("IV");

            // Declare the string used to hold the decrypted text
            string plaintext = null;

            // Create an Rijndael object with the specified key and IV
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Convert.FromBase64String(Key);
                rijAlg.IV = Convert.FromBase64String(IV);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
