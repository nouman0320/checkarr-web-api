using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Classes
{
    public class CodeGenerator {

        public String ActivationCodeGenerator()
        {
            // Use this function to get activation code
            String activationCode = GetUniqueKey(6);
            return activationCode;
        }

        public String RecoveryCodeGenerator()
        {
            // Use this function to get recovey code
            String recoveryCode = GetUniqueKey(6);
            return recoveryCode;
        }


        private static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString().ToUpper();
        }

    }


    


}
