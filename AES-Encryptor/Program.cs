using AES_Encryptor.Decrypt;
using AES_Encryptor.Encrypt;
using System.Security.Cryptography;
using System.Text;

// Data to Encrypt
string originalData = "Hello world! I'm a Decrypted Text";

// Using RNG (Random Number Generator) to generate a random key
byte[] key = new byte[16];
using (var rng = new RNGCryptoServiceProvider())
{
    rng.GetBytes(key);
}

// Calling the Encrypt code
byte[] encryptedData = EncryptAES.Encrypt(Encoding.UTF8.GetBytes(originalData), key);

// Calling the Decrypt code
string decryptedData = DecryptAES.Decrypt(encryptedData, key);

// Output with Encrypt && Decrypt data && RNG Key
Console.WriteLine("Original Data: " + originalData);
Console.WriteLine("Encrypted Data: " + Convert.ToBase64String(encryptedData));
Console.WriteLine("Decrypted Data: " + decryptedData);
Console.WriteLine("RNG Key: " + Convert.ToBase64String(key));

