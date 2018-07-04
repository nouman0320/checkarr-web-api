using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Classes
{
    public class Mailer
    {


        public void sendRecoveryMail(String email_to, String recoveryToken, String recoveryCode)
        {
            // this mail function should be called when user is recovering its account
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
            System.Diagnostics.Debug.WriteLine("sendRecoveryMail Method");
            System.Diagnostics.Debug.WriteLine("EMAIL TO => " + email_to);
            System.Diagnostics.Debug.WriteLine("RECOVERY CODE => " + recoveryCode);
            System.Diagnostics.Debug.WriteLine("RECOVERY TOKEN => " + recoveryToken);
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
        }

        public void sendActivationMail(String email_to, String activationToken, String activationCode)
        {
            // this mail function should be called when user is registering
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
            System.Diagnostics.Debug.WriteLine("sendActivationMail Method");
            System.Diagnostics.Debug.WriteLine("EMAIL TO => "+email_to);
            System.Diagnostics.Debug.WriteLine("ACTIVATION CODE => " + activationCode);
            System.Diagnostics.Debug.WriteLine("ACTIVATION TOKEN => " + activationToken);
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
        }

    }
}
