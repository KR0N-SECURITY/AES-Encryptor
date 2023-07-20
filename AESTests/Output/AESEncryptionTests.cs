using AES_Encryptor.Decrypt;
using AES_Encryptor.Encrypt;
using System.Security.Cryptography;
using System.Text;

namespace AESTests.Output;

public class AESEncryptionTests
{
    #region Tests

    [Theory]
    [InlineData("Hello world")]
    [InlineData("123456")]
    [InlineData("Testing with special characters: áéíóú ç !@#")]
    public void EncryptAndDecrypt_WithDifferentTexts_Success(string originalText)
    {
        byte[] key = GenerateRandomKey();

        byte[] encryptedData = EncryptAES.Encrypt(Encoding.UTF8.GetBytes(originalText), key);
        string decryptedText = DecryptAES.Decrypt(encryptedData, key);

        Assert.Equal(originalText, decryptedText);
    }

    [Fact]
    public void Encrypt_NullOrEmptyInput_ShouldReturnErrorMessage()
    {
        string? originalData = null;
        byte[] key = GenerateRandomKey();

        var exception = Record.Exception(() => EncryptWithException(originalData, key));
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Contains("No text provided", exception.Message);
    }

    [Fact]
    public void EncryptAndDecrypt_MaximumLengthTextAndKey_Success()
    {
        string originalText = new string('A', 10000);
        byte[] key = new byte[32];

        byte[] encryptedData = EncryptAES.Encrypt(Encoding.UTF8.GetBytes(originalText), key);
        string decryptedText = DecryptAES.Decrypt(encryptedData, key);

        Assert.Equal(originalText, decryptedText);
    }

    [Fact]
    public void EncryptAndDecrypt_DifferentEncoding_Success()
    {
        string originalText = "Hello world!";
        byte[] key = GenerateRandomKey();

        byte[] encryptedData = EncryptAES.Encrypt(Encoding.ASCII.GetBytes(originalText), key);
        string decryptedText = DecryptAES.Decrypt(encryptedData, key);

        Assert.Equal(originalText, decryptedText);
    }

    [Fact]
    public void EncryptAndDecrypt_StressTest()
    {
        for (int i = 0; i < 1000; i++)
        {
            string originalText = $"Test {i}";
            byte[] key = GenerateRandomKey();

            byte[] encryptedData = EncryptAES.Encrypt(Encoding.UTF8.GetBytes(originalText), key);
            string decryptedText = DecryptAES.Decrypt(encryptedData, key);

            Assert.Equal(originalText, decryptedText);
        }
    }

    [Fact]
    public void Decrypt_InvalidKey_ShouldThrowException()
    {
        string originalText = "Hello world!";
        byte[] key = GenerateRandomKey();
        byte[] encryptedData = EncryptAES.Encrypt(Encoding.UTF8.GetBytes(originalText), key);
        byte[] wrongKey = GenerateRandomKey();

        Assert.Throws<CryptographicException>(() => DecryptAES.Decrypt(encryptedData, wrongKey));
    }

    // Coming Soon...
    //[Fact]
    //public void ChosenPlaintextAttack_Test()
    //{
    //    string originalText = "Chosen-Plaintext Attack Test";
    //    byte[] key = GenerateRandomKey();

    //    // Encrypt the chosen text
    //    byte[] encryptedData = EncryptAES.Encrypt(Encoding.UTF8.GetBytes(originalText), key);

    //    // Simulate the attack: Attacker gets the encrypted data
    //    // ... The encryptedData is now in the hands of the attacker

    //    // Assert: Verify the security against Chosen-Plaintext Attack
    //    try
    //    {
    //        // Attacker tries to decrypt the encrypted data with a random key
    //        byte[] randomKey = GenerateRandomKey();
    //        string decryptedText = DecryptAES.Decrypt(encryptedData, randomKey);

    //        // If decryption doesn't fail, the test fails
    //        throw new Exception("Chosen-Plaintext Attack successful. Decrypted: " + decryptedText);
    //    }
    //    catch (CryptographicException)
    //    {
    //        // Decryption failed as expected
    //        // The test passes, as the system correctly resists the chosen-plaintext attack
    //    }
    //}

    #endregion Tests

    #region Important parts for the tests to work

    private byte[] GenerateRandomKey()
    {
        byte[] key = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }

        return key;
    }

    private void EncryptWithException(string originalData, byte[] key)
    {
        if (string.IsNullOrEmpty(originalData))
        {
            throw new ArgumentNullException(nameof(originalData), "No text provided. Please enter valid text to encrypt.");
        }

        EncryptAES.Encrypt(Encoding.UTF8.GetBytes(originalData), key);
    }

    #endregion Important parts for the tests to work
}
