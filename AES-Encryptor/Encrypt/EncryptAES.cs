using System.Security.Cryptography;

namespace AES_Encryptor.Encrypt;

public static class EncryptAES
{
    public static byte[] Encrypt(byte[] data, byte[] key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.GenerateIV();

            using (var encryptor = aes.CreateEncryptor())
            using (var ms = new System.IO.MemoryStream())
            {
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }
    }
}
