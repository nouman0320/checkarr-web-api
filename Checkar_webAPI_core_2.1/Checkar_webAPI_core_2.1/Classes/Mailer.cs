using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Classes
{
    public class Mailer
    {
        public string MailerEmail { get; }
        public string MailerPassword { get; }

        public Mailer(string MailerEmail, string MailerPassword)
        {
            this.MailerEmail = MailerEmail;
            this.MailerPassword = MailerPassword;
        }

        public void send_mail(String ReciverMail, String title, String body)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("no.reply.checkarr@gmail.com");
            msg.To.Add(ReciverMail);
            msg.Subject = title;




            msg.Body = body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(MailerEmail, MailerPassword);
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                System.Diagnostics.Debug.WriteLine("Mail has been successfully sent!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception while sending mail" + ex.Message);
            }
            finally
            {
                msg.Dispose();
            }
        }

        public void sendRecoveryMail(String email_to, String recoveryToken, String recoveryCode)
        {

            String mailTitle = "Having trouble in logging to your account?";
            String bodyBuilder = "";

            bodyBuilder += "<div style = \"font-family: Calibri, Arial; background-color: grey;padding-top: 10%; padding-bottom: 10%;\">";
            bodyBuilder += "	<div style = \"margin: 0 auto; padding-top: 100px; padding-bottom: 100px; width: 90%; border: 1px solid grey; text-align: center; background-color: white; border-radius: 5px; margin-top: 5%; box-shadow: 0 16px 26px 0 rgba(0, 0, 0, 0.2), 0 6px 26px 0 rgba(0, 0, 0, 0.19); \">";
            bodyBuilder += "		<img src=\"https://image.ibb.co/ekXJgy/Checkarr_logo_transparent.png\" style=\"max-width: 30%\" alt=\"Checkarr\" />";
            bodyBuilder += "		<p style = \"padding-left: 5%; padding-right: 5%; \">We are sending this mail to you to inform about important updates for your account</p>";
            bodyBuilder += "		<h2>Let's recover your account</h2>";
            bodyBuilder += "		<p style = \"padding-left: 5%; padding-right: 5%; \">It looks like you asked us to help you get back to your account. Use the following code or button to recover your account.</p>";
            bodyBuilder += "		<h3 style=\"color: grey; border: 3px dashed #ff1017; width: 100px; height: 30px;margin:0 auto; padding-top: 10px; border-radius: 5px\">" + recoveryCode + "</h3>";
            bodyBuilder += "		<p style = \"color: grey\"> OR</p>";
            bodyBuilder += "		<a href=\"http://localhost:4200/redirect/recovery/" + recoveryToken + "/" + email_to + "/" + recoveryCode + "/\"><button style=\"background-color: #ff1017;border: none;color: white;padding: 20px;text-align: center;text-decoration: none;display: inline-block;font-size: 16px;margin: 4px 2px;border-radius: 5px;cursor: pointer;\">Recover Account</button></a>";
            bodyBuilder += "		<p style = \"padding-left: 5%; padding-right: 5%;font-size: 11px; color:red;\">* Ignore this message if you haven't tried to recover your account</p>";
            bodyBuilder += "	<p>Stay awesome!</p><br/><br/>";
            bodyBuilder += "	<p style = \"color: grey; font-size: 10px\">This is auto generated mail. Please don't reply</p>";
            bodyBuilder += "	<p style = \"color: grey; font-size: 10px\">Checkarr © - Islamabad, Pakistan <br/>www.checkarr.pk</p>";
            bodyBuilder += "    </div>";
            bodyBuilder += "</div>";

            this.send_mail(email_to, mailTitle, bodyBuilder);

            // this mail function should be called when user is recovering its account
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
            System.Diagnostics.Debug.WriteLine("sendRecoveryMail Method");
            System.Diagnostics.Debug.WriteLine("EMAIL TO => " + email_to);
            System.Diagnostics.Debug.WriteLine("RECOVERY CODE => " + recoveryCode);
            System.Diagnostics.Debug.WriteLine("RECOVERY TOKEN => " + recoveryToken);
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
        }

        public void sendActivationMail(String email_to, String activationToken, String activationCode, int userId)
        {

            String mailTitle = "Build trust by getting your account verified";
            String bodyBuilder = "";

            bodyBuilder += "<div style = \"font-family: Calibri, Arial; background-color: grey;padding-top: 10%; padding-bottom: 10%;\">";
            bodyBuilder += "	<div style = \"margin: 0 auto; padding-top: 100px; padding-bottom: 100px; width: 90%; border: 1px solid grey; text-align: center; background-color: white; border-radius: 5px; margin-top: 5%; box-shadow: 0 16px 26px 0 rgba(0, 0, 0, 0.2), 0 6px 26px 0 rgba(0, 0, 0, 0.19); \">";
            bodyBuilder += "		<img src=\"https://image.ibb.co/ekXJgy/Checkarr_logo_transparent.png\" style=\"max-width: 30%\" alt=\"Checkarr\" />";
            bodyBuilder += "		<p style = \"padding-left: 5%; padding-right: 5%; \">We are sending this mail to you to inform about important updates for your account</p>";
            bodyBuilder += "		<h2>Account Verification</h2>";
            bodyBuilder += "		<p style = \"padding-left: 5%; padding-right: 5%; \">It looks like it's a great time to verify your account. Verified account are subjected to get best out of our services. <br/>Either use the following code to verify your account or click the button.</p>";
            bodyBuilder += "		<h3 style=\"color: grey; border: 3px dashed #ff1017; width: 100px; height: 30px;margin:0 auto; padding-top: 10px; border-radius: 5px\">" + activationCode + "</h3>";
            bodyBuilder += "		<p style = \"color: grey\"> OR</p>";
            bodyBuilder += "		<a href=\"http://localhost:4200/redirect/activation/" + activationToken + "/" + userId.ToString() + "/" + activationCode + "/\"><button style=\"background-color: #ff1017;border: none;color: white;padding: 20px;text-align: center;text-decoration: none;display: inline-block;font-size: 16px;margin: 4px 2px;border-radius: 5px;cursor: pointer;\">Verify Account</button></a>";
            bodyBuilder += "	<p>Stay awesome!</p><br/><br/>";
            bodyBuilder += "	<p style = \"color: grey; font-size: 10px\">This is auto generated mail. Please don't reply</p>";
            bodyBuilder += "	<p style = \"color: grey; font-size: 10px\">Checkarr © - Islamabad, Pakistan </br>www.checkarr.pk</p>";
            bodyBuilder += "    </div>";
            bodyBuilder += "</div>";

            this.send_mail(email_to, mailTitle, bodyBuilder);

            // this mail function should be called when user is registering
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
            System.Diagnostics.Debug.WriteLine("sendActivationMail Method");
            System.Diagnostics.Debug.WriteLine("EMAIL TO => " + email_to);
            System.Diagnostics.Debug.WriteLine("ACTIVATION CODE => " + activationCode);
            System.Diagnostics.Debug.WriteLine("ACTIVATION TOKEN => " + activationToken);
            System.Diagnostics.Debug.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
        }


        public void sendWelcomeMail(String email_to)
        {
            String mailTitle = "Welcome, Glad to know you!";
            String bodyBuilder = "";

            bodyBuilder += "<div style = \"font-family: Calibri, Arial; background-color: grey;padding-top: 10%; padding-bottom: 10%;\">";
            bodyBuilder += "	<div style = \"margin: 0 auto; padding-top: 100px; padding-bottom: 100px; width: 90%; border: 1px solid grey; text-align: center; background-color: white; border-radius: 5px; margin-top: 5%; box-shadow: 0 16px 26px 0 rgba(0, 0, 0, 0.2), 0 6px 26px 0 rgba(0, 0, 0, 0.19); \">";
            bodyBuilder += "		<img src=\"https://image.ibb.co/ekXJgy/Checkarr_logo_transparent.png\" style=\"max-width: 30%\" alt=\"Checkarr\" />";
            bodyBuilder += "		<p style = \"padding-left: 5%; padding-right: 5%; \">Thanks for letting us know you. Get ready to make everyone your fan!</p>";
            bodyBuilder += "		<h2>Just few steps from awesomeness</h2>";
            bodyBuilder += "		<p style = \"padding-left: 5%; padding-right: 5%; \">It's time to verify your account. Verified account are subjected to get best out of our services.<br/>Verify your account by logging into your account at www.checkarr.pk</p>";
            bodyBuilder += "		<p style = \"color: grey\"> If you don't know about doing this let us know <a href=\"http://localhost:4200/not-my-account/\">here</a></p>";
            bodyBuilder += "	<p>Stay awesome!</p><br/><br/>";
            bodyBuilder += "	<p style = \"color: grey; font-size: 10px\">This is auto generated mail. Please don't reply</p>";
            bodyBuilder += "	<p style = \"color: grey; font-size: 10px\">Checkarr © - Islamabad, Pakistan </br>www.checkarr.pk</p>";
            bodyBuilder += "    </div>";
            bodyBuilder += "</div>";

            this.send_mail(email_to, mailTitle, bodyBuilder);
        }
    }
}
