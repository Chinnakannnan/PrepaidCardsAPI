/***************************************************************************
'* Application  	:   Yappay Client
'* Module       	:   Encryption
'* File name    	:   Encryption.cs
'* Purpose      	:   Encrypt, Decrypt and Process Transaction
'*                     
'* Author       	:   
'* Date Created 	:   
'* End Created 	    :   
'* Modified History	:   
'*==========================================================================
'* S.No  RFC No/Bug ID  Date    Author      Description
'* 
'***************************************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BusinessModel.Common;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BusinessModel.Common
{
    /// <summary>
    /// Encrypt, Decrypt and Process Transaction
    /// </summary>
    public class M2BEncrypt
    {
        string privateKey = "";
        string publicKey = "";
        public M2BEncrypt()
        {
            privateKey = CommonConstants.AccupaydPrivateCerticateURL;
            publicKey = CommonConstants.M2BPublicCerticateURL;
        }
        #region Encrypt Request
        /// <summary>
        /// Encrypt Data
        /// </summary>
        /// <param name="requestData">Request Data in plain text</param>
        /// <param name="messageRefNo">Message reference number</param>
        /// <param name="entity">Entity code of the business</param>
        /// <returns>Encrypted Data</returns>
        public string encodeRequest(string requestData, string messageRefNo, string entity)
        {
            string encodedRequest = null;
            try
            {
                YappayEncryptedRequest request = new YappayEncryptedRequest();
                byte[] sessionKeyByte = this.generateToken();
                request.token = this.generateDigitalSignedToken(requestData);
                request.body = this.encryptData(requestData, sessionKeyByte, messageRefNo);
                request.key = this.encryptKey(sessionKeyByte);
                request.entity = this.encryptKey(Encoding.ASCII.GetBytes(entity));
                request.refNo = messageRefNo;

                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(YappayEncryptedRequest));
                MemoryStream ms = new MemoryStream();
                js.WriteObject(ms, request);

                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                encodedRequest = sr.ReadToEnd();
                sr.Close();
                ms.Close();
            }
            catch
            {
                throw;
            }
            return encodedRequest;
        }

        /// <summary>
        /// Sign the request data using private Key of the Business Entity and generate digital signature token
        /// Yappay will verify the signature using the public key from Business Entity 
        /// </summary>
        /// <param name="requestData">Request data in plain text</param>
        /// <returns>Digital signature token</returns>
        public string generateDigitalSignedToken(string requestData)
        {
            string signedToken = null;
            try
            {
                byte[] requestDataBytes = Encoding.ASCII.GetBytes(requestData);

                RSACryptoServiceProvider rsa = readPrivateKeyFromFile(privateKey);
                SHA1Managed sha = new SHA1Managed();
                byte[] signedTokenBytes = rsa.SignData(requestDataBytes, sha);

                signedToken = base64urlencode(signedTokenBytes);
            }
            catch
            {
                throw;
            }
            return signedToken;
        }

        /// <summary>
        /// Generate encryption key dynamically using Symmetric Key alogorithm
        /// </summary>
        /// <returns>Encryption key</returns>
        public byte[] generateToken()
        {
            byte[] symmetricKey = null;
            try
            {
                AesManaged objAesKey = new AesManaged();
                objAesKey.KeySize = 128;
                objAesKey.Mode = CipherMode.CBC;
                objAesKey.Padding = PaddingMode.PKCS7;

                objAesKey.GenerateKey();
                symmetricKey = objAesKey.Key;
            }
            catch
            {
                throw;
            }

            return symmetricKey;
        }

        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <param name="requestData">Request data in plain text</param>
        /// <param name="sessionKey">Encryption key</param>
        /// <param name="messageRefNo">Salt value for encryption</param>
        /// <returns>Encrypted text</returns>
        public string encryptData(string requestData, byte[] sessionKey,
                                  string messageRefNo)
        {
            string encryptedText = null;
            try
            {
                AesManaged objAesKey = new AesManaged();
                objAesKey.KeySize = 128;
                objAesKey.Mode = CipherMode.CBC;
                objAesKey.Padding = PaddingMode.PKCS7;

                objAesKey.Key = sessionKey;
                objAesKey.IV = Encoding.ASCII.GetBytes(messageRefNo);

                byte[] requestDataBytes = Encoding.ASCII.GetBytes(requestData);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, objAesKey.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(requestDataBytes, 0, requestDataBytes.Length);
                if (cs != null)
                {
                    cs.Close();
                }
                byte[] encryptedTextBytes = ms.ToArray();

                encryptedText = base64urlencode(encryptedTextBytes);

            }
            catch
            {
                throw;
            }
            return encryptedText;
        }

        /// <summary>
        /// Encrypt text using public key from Yappay
        /// Yappay will decrypt it using its private key
        /// </summary>
        /// <param name="sessionKey">Key value</param>
        /// <returns>Encrypted text</returns>
        public string encryptKey(byte[] sessionKey)
        {
            string encryptedKey = null;
            try
            {
                RSACryptoServiceProvider rsa = readPublicKeyFromFile(publicKey);

                byte[] encryptedKeyBytes = rsa.Encrypt(sessionKey, false);
                encryptedKey = base64urlencode(encryptedKeyBytes);
            }
            catch
            {
                throw;
            }

            return encryptedKey;
        }
        #endregion

        #region Decrypt Response
        /// <summary>
        /// Decrypt transaction result
        /// </summary>
        /// <param name="response">Yappay Response Data</param>
        /// <returns>Transaction result in plain text</returns>
        public string decodeResponse(YappayEncryptedResponse response)
        {
            string sessionKey = response.headers.key;
            string token = response.headers.hash;
            string messageRefNo = response.headers.refNo;
            return this.decryptMessage(response.body,
                    sessionKey, token, messageRefNo);
        }

        /// <summary>
        /// Decrypt transaction result
        /// </summary>
        /// <param name="responseData">Response body text</param>
        /// <param name="encSessionKey">Encryption key value</param>
        /// <param name="hash">Digital signature token</param>
        /// <param name="messageRefNo">Salt value</param>
        /// <returns>Transaction result in plain text</returns>
        public string decryptMessage(string responseData, string encSessionKey, string hash, string messageRefNo)
        {

            byte[] sessionKey = this.decryptSessionKey(encSessionKey);
            String data = decryptWithAESKey(responseData, sessionKey, Encoding.ASCII.GetBytes(messageRefNo));
            if (verifyDigitalSignedToken(data, hash))
                return data;
            else
                return "Token Verification failed";

        }

        /// <summary>
        /// Yappay will sign the response data using private Key of the YAP and generate digital signature token
        /// Business Entity will verify the signature using the public key from YAP
        /// </summary>
        /// <param name="responseData">Response body text</param>
        /// <param name="hash">Digital signature token</param>
        /// <returns>Validation success</returns>
        public bool verifyDigitalSignedToken(string responseData, string hash)
        {
            bool status = false;
            try
            {

                byte[] responseDataBytes = Encoding.ASCII.GetBytes(responseData);
                byte[] hashBytes = UrlDecode(hash);

                RSACryptoServiceProvider rsa = readPublicKeyFromFile(publicKey);
                SHA1Managed sha = new SHA1Managed();
                status = rsa.VerifyData(responseDataBytes, sha, hashBytes);

            }
            catch
            {
                throw;
            }
            return status;
        }

        /// <summary>
        /// Decrypt data
        /// </summary>
        /// <param name="responseData">Encrypted data</param>
        /// <param name="key">Encryption key</param>
        /// <param name="iv">Salt value</param>
        /// <returns>Plain text</returns>
        public string decryptWithAESKey(string responseData, byte[] key, byte[] iv)
        {
            string plainText = null;
            try
            {
                AesManaged objAesKey = new AesManaged();
                objAesKey.KeySize = 128;
                objAesKey.Mode = CipherMode.CBC;
                objAesKey.Padding = PaddingMode.PKCS7;

                objAesKey.Key = key;
                objAesKey.IV = iv;


                byte[] responseDataBytes = Convert.FromBase64String(responseData);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, objAesKey.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(responseDataBytes, 0, responseDataBytes.Length);
                if (cs != null)
                {
                    cs.Close();
                }
                byte[] plainTextBytes = ms.ToArray();

                plainText = Encoding.ASCII.GetString(plainTextBytes);

            }
            catch
            {
                throw;
            }
            return plainText;
        }

        /// <summary>
        /// Decrypt Key value using private key of the Business Entity
        /// Data was orignally encrypted by Yappay using public key of the Business entity
        /// </summary>
        /// <param name="encryptedKey">Encrypted key value</param>
        /// <returns>Key value</returns>
        public byte[] decryptSessionKey(string encryptedKey)
        {
            byte[] sessionKey = null;
            try
            {
                RSACryptoServiceProvider rsa = readPrivateKeyFromFile(privateKey);
                // converting base 64 string to byte array
                byte[] encryptedKeyBytes = Convert.FromBase64String(encryptedKey);
                sessionKey = rsa.Decrypt(encryptedKeyBytes, false);
            }
            catch
            {
                throw;
            }

            return sessionKey;
        }
        #endregion

        #region Helper Methods

        static string base64urlencode(byte[] arg)
        {
            string s = Convert.ToBase64String(arg); // Regular base64 encoder
            //s = s.Split('=')[0]; // Remove any trailing '='s
            //s = s.Replace('+', '-'); // 62nd char of encoding
            //s = s.Replace('/', '_'); // 63rd char of encoding
            return s;
        }
        public static byte[] UrlDecode(string input)
        {
            var output = input;

            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding

            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0:
                    break; // No pad chars in this case
                case 2:
                    output += "==";
                    break; // Two pad chars
                case 3:
                    output += "=";
                    break; // One pad char
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(input), "Illegal base64url string!");
            }

            var converted = Convert.FromBase64String(output); // Standard base64 decoder

            return converted;
        }
        #endregion

        #region Methods for reading key files
        private RSACryptoServiceProvider readPrivateKeyFromFile(String keyFileName)
        {
            RSACryptoServiceProvider rsa = null;
            try
            {

                byte[] keyblob = GetFileBytes(keyFileName);
                if (keyblob != null)
                {
                    rsa = DecodePrivateKeyInfo(keyblob);
                }
            }
            catch
            {
                throw;
            }
            return rsa;
        }
        private RSACryptoServiceProvider readPublicKeyFromFile(String keyFileName)
        {
            RSACryptoServiceProvider rsa = null;
            try
            {

                byte[] keyblob = GetFileBytes(keyFileName);
                if (keyblob != null)
                {
                    rsa = DecodeX509PublicKey(keyblob);
                }
            }
            catch
            {
                throw;
            }
            return rsa;
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        public static byte[] GetFileBytes(String filename)
        {
            if (!File.Exists(filename))
                return null;
            Stream stream = new FileStream(filename, FileMode.Open);
            int datalen = (int)stream.Length;
            byte[] filebytes = new byte[datalen];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(filebytes, 0, datalen);
            stream.Close();
            return filebytes;
        }
        //------- Parses binary asn.1 X509 SubjectPublicKeyInfo; returns RSACryptoServiceProvider ---
        public static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(x509key);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {
                //r
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15);		//read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))	//make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103)	//data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)		//expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102)	//data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();	// read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte();	//advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {	//if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();	//skip this null byte
                    modsize -= 1;	//reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize);	//read the modulus bytes

                if (binr.ReadByte() != 0x02)			//expect an Integer for the exponent data
                    return null;
                int expbytes = (int)binr.ReadByte();		// should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }
        //------- Parses binary asn.1 PKCS #8 PrivateKeyInfo; returns RSACryptoServiceProvider ---
        public static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            // this byte[] includes the sequence byte and terminal encoded null 
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(pkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;


                bt = binr.ReadByte();
                if (bt != 0x02)
                    return null;

                twobytes = binr.ReadUInt16();

                if (twobytes != 0x0001)
                    return null;

                seq = binr.ReadBytes(15);		//read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))	//make sure Sequence for OID is correct
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x04)	//expect an Octet string 
                    return null;

                bt = binr.ReadByte();		//read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
                if (bt == 0x81)
                    binr.ReadByte();
                else
                    if (bt == 0x82)
                    binr.ReadUInt16();
                //------ at this stage, the remaining sequence should be the RSA private key

                byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
                return rsacsp;
            }

            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }
        //------- Parses binary ans.1 RSA private key; returns RSACryptoServiceProvider  ---
        public static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)	//version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }
            finally { binr.Close(); }
        }
        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();	// data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }



            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
        #endregion

    }
}
