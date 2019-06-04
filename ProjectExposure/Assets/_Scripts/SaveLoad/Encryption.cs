using System;
using System.Security.Cryptography;
using System.Text;

public class Encryption
{
    private static readonly string hash = "123@321!";

    public static string Encrypt(string original)
    {
        var data = Encoding.UTF8.GetBytes(original);

        using (var md5 = new MD5CryptoServiceProvider())
        {
            var key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));

            using (var tDesc =
                new TripleDESCryptoServiceProvider {Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7})
            {
                var tr = tDesc.CreateEncryptor();
                var res = tr.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(res, 0, res.Length);
            }
        }
    }

    public static string Decrypt(string input)
    {
        var data = Convert.FromBase64String(input);

        using (var md5 = new MD5CryptoServiceProvider())
        {
            var key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));

            using (var tDesc =
                new TripleDESCryptoServiceProvider {Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7})
            {
                var tr = tDesc.CreateDecryptor();
                var res = tr.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(res);
            }
        }
    }
}