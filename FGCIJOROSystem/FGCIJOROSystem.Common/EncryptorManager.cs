using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FGCIJOROSystem.Common
{
    public class EncryptorManager
    {

        static string passPhrase = "FGurrea";
        static string initVector = "@1B2c3D4e5F6g7E1";// must be 16 bytes

        public static string EncryptPassword(string t)
        {
            Encryptor sm = new Encryptor(passPhrase, initVector);

            return sm.Encrypt(t);
        }

        public static string DecryptPassword(string t)
        {
            Encryptor sm = new Encryptor(passPhrase, initVector);
            return sm.Decrypt(t);
        }


        //public static string EncryptPassword(string clearText)
        //    {
        //        string EncryptionKey = "MAKV2SPBNI99212";
        //        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //        using (Aes encryptor = Aes.Create())
        //        {
        //            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
        //    0x49,
        //    0x76,
        //    0x61,
        //    0x6e,
        //    0x20,
        //    0x4d,
        //    0x65,
        //    0x64,
        //    0x76,
        //    0x65,
        //    0x64,
        //    0x65,
        //    0x76
        //});
        //            encryptor.Key = pdb.GetBytes(32);
        //            encryptor.IV = pdb.GetBytes(16);
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //                {
        //                    cs.Write(clearBytes, 0, clearBytes.Length);
        //                    cs.Close();
        //                }
        //                clearText = Convert.ToBase64String(ms.ToArray());
        //            }
        //        }
        //        return clearText;
        //    }

        //public static string DecryptPassword(string cipherText)
        //    {
        //        string EncryptionKey = "MAKV2SPBNI99212";
        //        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //        using (Aes encryptor = Aes.Create())
        //        {
        //            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
        //        0x49,
        //        0x76,
        //        0x61,
        //        0x6e,
        //        0x20,
        //        0x4d,
        //        0x65,
        //        0x64,
        //        0x76,
        //        0x65,
        //        0x64,
        //        0x65,
        //        0x76
        //    });
        //            encryptor.Key = pdb.GetBytes(32);
        //            encryptor.IV = pdb.GetBytes(16);
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //                {
        //                    cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                    cs.Close();
        //                }
        //                cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //            }
        //        }

        //        return cipherText;
        //    }
        }
}
