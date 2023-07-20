using System.Security.Cryptography;
using System.Text;

namespace AES_Encryptor.Decrypt;

public static class DecryptAES
{
    public static string Decrypt(byte[] encryptedData, byte[] key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] iv = new byte[aes.IV.Length];
            Array.Copy(encryptedData, iv, iv.Length);
            aes.IV = iv;

            using (var decryptor = aes.CreateDecryptor())
            using (var ms = new System.IO.MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptedData, aes.IV.Length, encryptedData.Length - aes.IV.Length);
                    cryptoStream.FlushFinalBlock();
                }

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
