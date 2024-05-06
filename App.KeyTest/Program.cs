using System.Security.Cryptography;
using System.Text;

namespace App.KeyTest;

class Program
{
    static void Main()
    {
        //GenerateKeyPair();
        //Console.WriteLine();


        string serverPublicKey = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPjBDVGJENUZNd0ZqTTY2dkVHUWtPZmNhQ1prbE92elByN01HQWdyczZiWlFNR2dDQVR4TlJBRnBQeUpJY2dXK0I3dUVDTjZQVGpYMWhPY0FPNVU2a2R5TEM0bTdlSlZQV0lBWGowMkZHNG5tTnpaa1B1akZROGJlMzVZcEpnVGdIY2ZzdUI1czZlZmZOWTlMWS9UQXBqT1hVMnI3OSsxbEQraWJrb0tSV1RERT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";
        string privateKey = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPjBDVGJENUZNd0ZqTTY2dkVHUWtPZmNhQ1prbE92elByN01HQWdyczZiWlFNR2dDQVR4TlJBRnBQeUpJY2dXK0I3dUVDTjZQVGpYMWhPY0FPNVU2a2R5TEM0bTdlSlZQV0lBWGowMkZHNG5tTnpaa1B1akZROGJlMzVZcEpnVGdIY2ZzdUI1czZlZmZOWTlMWS9UQXBqT1hVMnI3OSsxbEQraWJrb0tSV1RERT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPjhvUGp5ZXN6RjZlZzU4T3p0YlF2cnp0bzlEenBLRzhTQmJ3Z1Rpc0psRk1odWxWMGVUbi9HeWdvOThFT2ozdXVTci9EOEtidlZRdGRKdGVnNUxoNVhRPT08L1A+PFE+MjdlMGF1VUxPMHhsUkZEOE4wWWpqbVc4ZStRQnpwenU1SUdJT085MEdQOEN3ZTJrWUR2aFh1RHAxMDlLYjUvYlRHVXhLRU9OeTRNN29OVUtnakRzNVE9PTwvUT48RFA+MFJQOTdaWDFuWDRxVGNXK0NIaEk2QVVMTGczWlliK29SUU4wd285K0diODJBOFFxdjF6TjMrYldzd3BnK0RqRUFiT2tSTFJXLy9YbElGaWtZS3ZlRFE9PTwvRFA+PERRPlpUTm1MRjQzSUNiOTljdC9nRTRVRG9DWm9rN1E3a09tWFNXMGd0VDRHcE5LeDl3WGZDUFltSVRyOERvYlJQTFNaWXV1YXFkQzVsd1oyT2lIb0pPOGFRPT08L0RRPjxJbnZlcnNlUT52N2dBSGVtc3c0UW90N2trTHE3Z0dtbmF0dVpTODlJMTd6OWJHNmxzczM0MzVnVURybFB3dGE5UnpDTzRPdXRMWG5lZlhaYTdkdE1FU3UyK1d2MGR2Zz09PC9JbnZlcnNlUT48RD5KbUs1cEk0VkU3NzhzYlRJY2g2RnRlUGpYZ1pFZDhZVlN5RW5lRlNia3FSVU5BSm1KMDhtWjlSLzlMc2ZkeTV2amJTZzRrNThBQkxBbUxRbSt4cU5BVmRncU90NmIyRHVFYnJsdE1Ib3NaeXBTUnpkWjhtMDk1ekpYNUdqL0xqNnRTdEVHOXFQellmUWFlbHkrZmJUQVFDa3Qwc3d1cDB1TkNJcGtYaHR5aDA9PC9EPjwvUlNBS2V5VmFsdWU+";

        byte[] passwordByte = Encoding.UTF8.GetBytes($"1234**");

        string encodedString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(serverPublicKey));        
        byte[] encryptedKey = EncryptData(passwordByte, encodedString);

        
        Console.WriteLine("Encryped Password: " + BitConverter.ToString(encryptedKey).Replace("-", ""));
        Console.WriteLine();


        byte[] decryptedData = DecryptData(encryptedKey, System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(privateKey)));
        var decryptedText = Encoding.UTF8.GetString(decryptedData);

        Console.WriteLine("Decryped Password: " + decryptedText);
        Console.Read();
    }


    static byte[] EncryptData(byte[] data, string publicKey)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(publicKey);

        byte[] encryptedData = rsa.Encrypt(data, false);
        return encryptedData;
    }

    static byte[] DecryptData(byte[] data, string privateKey)
    {     
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);

        byte[] decryptedData = rsa.Decrypt(data, false);
        return decryptedData;
    }



    static void GenerateKeyPair()
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            //Get public key
            string publicKey = rsa.ToXmlString(false);
            Console.WriteLine("Public Key:");

            byte[] bytes_public = Encoding.UTF8.GetBytes(publicKey);
            string base64_public = Convert.ToBase64String(bytes_public);
            string xmlResult_public = publicKey;

            Console.WriteLine(base64_public);


            //Get private key
            string privateKey = rsa.ToXmlString(true);
            Console.WriteLine("\nPrivate Key:");

            byte[] bytes_private = Encoding.UTF8.GetBytes(privateKey);
            string base64_private = Convert.ToBase64String(bytes_private);
            string xmlResult_private = privateKey;

            Console.WriteLine(base64_private);
        }
    }


}