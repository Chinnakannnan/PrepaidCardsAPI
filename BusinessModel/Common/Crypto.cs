using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BusinessModel.Common
{
    public static class Crypto
    {
        public static string AES_ENCRYPT(string clearText, string EncryptionKey)
            {
                try
                {
                    byte[] clearBytes = Encoding.ASCII.GetBytes(clearText);
                    byte[] bENVVALUE;
                    using (Aes encryptor = Aes.Create())
                    {
                        byte[] bKey = Encoding.ASCII.GetBytes(EncryptionKey);// util.HexStringToByte(EncryptionKey);//Encoding.ASCII.GetBytes(EncryptionKey);//

                        encryptor.Key = bKey;
                        encryptor.Mode = CipherMode.ECB;
                        encryptor.Padding = PaddingMode.PKCS7;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(clearBytes, 0, clearBytes.Length);
                                cs.Close();
                            }
                            bENVVALUE = ms.ToArray();
                            clearText = Convert.ToBase64String(bENVVALUE);//util.ByteArrayToHexString(bENVVALUE);   //Convert.ToBase64String(bENVVALUE);
                        }
                    }
                }
                catch (Exception)
                {

                    return "";
                }
                finally
                {

                }
                return clearText;
            }

            public static string AES_DECRYPT(string cipherText, string EncryptionKey)
            {
                try
                {
                    byte[] bDECVALUE;
                    byte[] cipherBytes = Convert.FromBase64String(cipherText); //util.HexStringToByte(cipherText);
                    using (Aes encryptor = Aes.Create())
                    {
                        byte[] bKey = Encoding.ASCII.GetBytes(EncryptionKey);// util.HexStringToByte(EncryptionKey);  //Encoding.ASCII.GetBytes(EncryptionKey);// 
                        encryptor.Key = bKey;
                        encryptor.Mode = CipherMode.ECB;
                        encryptor.Padding = PaddingMode.PKCS7;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                                cs.Close();
                            }
                            bDECVALUE = ms.ToArray();
                            cipherText = Encoding.ASCII.GetString(ms.ToArray());
                        }
                    }
                    return cipherText;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

            public static string AES_ENCRYPT_CBC(string clearText, string EncryptionKey, string IV)
            {
                try
                {
                    byte[] clearBytes = Encoding.ASCII.GetBytes(clearText);
                    byte[] bENVVALUE;
                    using (Aes encryptor = Aes.Create())
                    {
                        byte[] bKey = ASCIIEncoding.UTF8.GetBytes(EncryptionKey);//Encoding.ASCII.GetBytes(EncryptionKey);//
                        byte[] bIV = ASCIIEncoding.UTF8.GetBytes(IV);

                        encryptor.Key = bKey;
                        encryptor.IV = bIV;
                        encryptor.Mode = CipherMode.CBC;
                        encryptor.Padding = PaddingMode.PKCS7;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(clearBytes, 0, clearBytes.Length);
                                cs.Close();
                            }
                            bENVVALUE = ms.ToArray();
                            string d = BitConverter.ToString(bENVVALUE);
                            clearText = Convert.ToBase64String(bENVVALUE);//util.ByteArrayToHexString(bENVVALUE);   //Convert.ToBase64String(bENVVALUE);
                        }
                    }
                }
                catch (Exception)
                {

                    return "";
                }
                finally
                {

                }
                return clearText;
            }

            public static string AES_DECRYPT_CBC(string cipherText, string EncryptionKey)
            {
                try
                {
                    byte[] bDECVALUE;
                    byte[] cipherBytes = Convert.FromBase64String(cipherText); //util.HexStringToByte(cipherText);
                    using (Aes encryptor = Aes.Create())
                    {
                        byte[] bKey = ASCIIEncoding.UTF8.GetBytes(EncryptionKey);  //Encoding.ASCII.GetBytes(EncryptionKey);// 
                        byte[] bIV = new byte[16];

                        Array.Copy(cipherBytes, bIV, 16);

                        byte[] bData = new byte[cipherBytes.Length - 16];

                        Array.Copy(cipherBytes, 16, bData, 0, cipherBytes.Length - 16);

                        encryptor.Key = bKey;
                        encryptor.IV = bIV;
                        encryptor.Mode = CipherMode.CBC;
                        encryptor.Padding = PaddingMode.PKCS7;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(bData, 0, bData.Length);
                                cs.Close();
                            }
                            bDECVALUE = ms.ToArray();
                            string d = BitConverter.ToString(bDECVALUE);
                            cipherText = Encoding.ASCII.GetString(ms.ToArray());
                        }
                    }
                    return cipherText;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

            private static string getSalt()
            {
                byte[] bytes = new byte[128 / 8];
                using (var keyGenerator = RandomNumberGenerator.Create())
                {
                    keyGenerator.GetBytes(bytes);
                    return BitConverter.ToString(bytes).Replace("-", "").ToLower();
                }
            }

            public static string GETHASH(string text)
            {
                // SHA512 is disposable by inheritance.  
                using (var sha512 = SHA512.Create())
                {
                    // Send a sample text to hash.  
                    var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));
                    // Get the hashed string.  
                    //return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    return Convert.ToBase64String(hashedBytes);
                }
            }

            public static string GetHMACSHA512(string text, string key)
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
               // Log obj = new Log();

                //obj.WriteAppLog(text);
                //obj.WriteAppLog(key);

                byte[] hashValue;
                byte[] keybyt = ASCIIEncoding.ASCII.GetBytes(key);
                byte[] message = ASCIIEncoding.ASCII.GetBytes(text);

                HMACSHA512 hashString = new HMACSHA512(keybyt);
                string hex = "";

                hashValue = hashString.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hex += String.Format("{0:x2}", x);
                }
                return hex;
            }
        
    }
}
