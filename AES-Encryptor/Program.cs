using AES_Encryptor.Decrypt;
using AES_Encryptor.Encrypt;
using System.Security.Cryptography;
using System.Text;

// Read user input for originalData
Console.Write("Enter the text to encrypt: ");
string? originalData = Console.ReadLine();

// Check if input is null or empty
if (string.IsNullOrEmpty(originalData))
{
    throw new ArgumentNullException(nameof(originalData), "No text provided. Please enter valid text to encrypt.");
}

// Using RandomNumberGenerator to generate a random key
byte[] key = new byte[16];
using (var rng = RandomNumberGenerator.Create())
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

// Press any key to close this CMD
Console.ReadLine();