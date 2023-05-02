using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using BusinessModel.Common;
using AutoMapper.Configuration;

//using Microsoft.Extensions.Hosting;

namespace NeoBank.API.Utilities
{
    // private readonly IConfiguration Configuration;

    public static class MailServices
    {
       


        public static string USERREGISTERMAILSUBJECT = "New User Registration";
        public static string USERRESETPASSWORDMAILSUBJECT = "Reset Password";
        public static string OTPFORMAILMAILSUBJECT = "Mail OTP";
        public static string CONTENTFORMAILMAILSUBJECT = "Reference Id";
        public static string USERAPPROVAL = "Approval";
        public static string WELCOMEMAILSUBJECT = "Welcome Mail";
        public static string SENDINGLINKFORKYC = "Link for KYC";

        public static void AdminApproval(string EmailAddress, string Password, string TPin, string imgUrl, string VAccountId, string VIFSCCode, string VBankName, string fromAddr, string smtpAddr, string smtpPort, string mailSecret)
        {
            string htmlBody = string.Empty;
            string password = Crypto.AES_DECRYPT(Password, COMMON.EMAILKEY);
            string fromEmail = Crypto.AES_DECRYPT(EmailAddress, COMMON.EMAILKEY);
            string tPin = Crypto.AES_DECRYPT(TPin, COMMON.EMAILKEY);
            //string lstrImageURL = ConfigurationManager.AppSettings[""];
            // string lstrImageURL = Configuration.GetSection("AppSettings:").Value;
            string lstrImageURL = imgUrl;
            //string lstrImageURL = Configuration.GetSection("appSettings")["DMTURL"];
            using (StreamReader reader = new StreamReader("Template/AdminPage.html"))
            {
                htmlBody = reader.ReadToEnd();
            }

            htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
            htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
            htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
            htmlBody = htmlBody.Replace("{username}", fromEmail);
            htmlBody = htmlBody.Replace("{password}", password);
            htmlBody = htmlBody.Replace("{tpin}", tPin);
            htmlBody = htmlBody.Replace("{virtualaccountno}", VAccountId);
            htmlBody = htmlBody.Replace("{virtualifsccode}", VIFSCCode);
            htmlBody = htmlBody.Replace("{virtualbankname}", VBankName);


            SendMail(fromEmail, USERAPPROVAL, htmlBody, fromAddr, smtpAddr, smtpPort, mailSecret);
        }

        //public static void UserRegistration(string EmailAddress, string Password, string TPin, string VAccountId, string VIFSCCode, string VBankName)
        //{
        //    string htmlBody = string.Empty;
        //    string password = Crypto.AES_DECRYPT(Password, COMMON.EMAILKEY);
        //    string fromEmail = Crypto.AES_DECRYPT(EmailAddress, COMMON.EMAILKEY);
        //    string tPin = Crypto.AES_DECRYPT(TPin, COMMON.EMAILKEY);
        //    string lstrImageURL = ConfigurationManager.AppSettings["IMAGEURLPATH"];

        //    using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Template/UserRegistration.html")))
        //    {
        //        htmlBody = reader.ReadToEnd();
        //    }

        //    htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
        //    htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
        //    htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
        //    htmlBody = htmlBody.Replace("{username}", fromEmail);
        //    htmlBody = htmlBody.Replace("{password}", password);
        //    htmlBody = htmlBody.Replace("{tpin}", tPin);
        //    htmlBody = htmlBody.Replace("{virtualaccountno}", VAccountId);
        //    htmlBody = htmlBody.Replace("{virtualifsccode}", VIFSCCode);
        //    htmlBody = htmlBody.Replace("{virtualbankname}", VBankName);

        //    SendMail(fromEmail, USERREGISTERMAILSUBJECT, htmlBody);
        //}

        //public static void ResetPassword(string EmailAddress, string Password)
        //{
        //    string htmlBody = string.Empty;
        //    string password = Crypto.AES_DECRYPT(Password, COMMON.EMAILKEY);
        //    string fromEmail = Crypto.AES_DECRYPT(EmailAddress, COMMON.EMAILKEY);
        //    string lstrImageURL = ConfigurationManager.AppSettings["IMAGEURLPATH"];
        //    using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Template/ResetPassword.html")))
        //    {
        //        htmlBody = reader.ReadToEnd();
        //    }

        //    htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
        //    htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
        //    htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
        //    htmlBody = htmlBody.Replace("{username}", fromEmail);
        //    htmlBody = htmlBody.Replace("{password}", password);


        //    SendMail(fromEmail, USERRESETPASSWORDMAILSUBJECT, htmlBody);
        //}

        public static void UserApproval(string EmailID, string Password, string fromAddr, string smtpAddr, string smtpPort, string mailSecret,string imgUrl)
        {
            string htmlBody = string.Empty;
            string password = Crypto.AES_DECRYPT(Password, COMMON.EMAILKEY);
            string fromEmail = Crypto.AES_DECRYPT(EmailID, COMMON.EMAILKEY);
            string lstrImageURL = imgUrl;
            using (StreamReader reader = new StreamReader("Template/WelcomeTemplate.html"))
            {
                htmlBody = reader.ReadToEnd();
            }

            htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
            htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
            htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
            htmlBody = htmlBody.Replace("{username}", fromEmail);
            htmlBody = htmlBody.Replace("{password}", password);


            SendMail(fromEmail, WELCOMEMAILSUBJECT, htmlBody, fromAddr, smtpAddr, smtpPort, mailSecret);
        }
        public static void ComplaintMail(string CustomerId, string ToAddress, string MailSubject, string MailBody, string fromAddr, string smtpAddr, string smtpPort, string mailSecret,string lstrImageURL)
         {
            string htmlBody = string.Empty;  
            using (StreamReader reader = new StreamReader("Template/ComplaintTemplate.html"))
            {
                htmlBody = reader.ReadToEnd();
            }
            htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
            htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
            htmlBody = htmlBody.Replace("{id}", CustomerId);
            htmlBody = htmlBody.Replace("{Subject}", MailSubject);
            htmlBody = htmlBody.Replace("{Body}", MailBody);


            SendMail(ToAddress, MailSubject, htmlBody, fromAddr, smtpAddr, smtpPort, mailSecret);
        }
        public static void LoginOTP(string OTP, string ToAddress, string fromAddr, string smtpAddr, string smtpPort, string mailSecret, string lstrImageURL)
        {
            string htmlBody = string.Empty;
            using (StreamReader reader = new StreamReader("Template/LoginemailOTP.html"))
            {
                htmlBody = reader.ReadToEnd();
            }
            htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
            htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");  
            htmlBody = htmlBody.Replace("{OTP}", OTP);


            SendMail(ToAddress, "Login OTP", htmlBody, fromAddr, smtpAddr, smtpPort, mailSecret);
        }
        public static void UserEntryOTP(string EmailID, string OTP, string imgUrl, string fromAddr, string smtpAddr, string smtpPort, string mailSecret)
        {
            string htmlBody = string.Empty;
            string otp = Crypto.AES_DECRYPT(OTP, COMMON.EMAILKEY);
            string fromEmail = Crypto.AES_DECRYPT(EmailID, COMMON.EMAILKEY);
           // string otp = "12345";
            //string fromEmail = "cynthia@accupaydtech.com";
            string lstrImageURL = imgUrl;
            using (StreamReader reader = new StreamReader("Template/LoginOTPTemplate.html"))
            {
                htmlBody = reader.ReadToEnd();
            }

            htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
            htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
            htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
            htmlBody = htmlBody.Replace("{userid}", fromEmail);
            htmlBody = htmlBody.Replace("{OTP}", otp);


            SendMail(fromEmail, OTPFORMAILMAILSUBJECT, htmlBody, fromAddr, smtpAddr, smtpPort, mailSecret);
        }


        public static void SendMail(string ToAddress, string MailSubject, string MailBody, string fromAddr, string smtpAddr, string smtpPort, string mailSecret)
        {
            try
            {
                var emailMessage = new MailMessage();

                emailMessage.To.Add(new MailAddress(ToAddress));
                emailMessage.From = new MailAddress(fromAddr);
                emailMessage.Subject = MailSubject;
                emailMessage.Body = MailBody;
                emailMessage.IsBodyHtml = true;
                using (var smtp = new SmtpClient(smtpAddr, Convert.ToInt32(smtpPort)))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromAddr, mailSecret);
                    smtp.EnableSsl = true;
                    smtp.Send(emailMessage);
                }
            
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static void SendRefId(string EmailID, string RefId, string imgUrl, string fromAddr, string smtpAddr, string smtpPort, string mailSecret)
        {
            string htmlBody = string.Empty;
            string refid = Crypto.AES_DECRYPT(RefId, COMMON.EMAILKEY);
            string fromEmail = Crypto.AES_DECRYPT(EmailID, COMMON.EMAILKEY);
            string lstrImageURL = imgUrl;
            using (StreamReader reader = new StreamReader("Template/RefId.html"))
            {
                htmlBody = reader.ReadToEnd();
            }

            htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
            htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
            htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
            htmlBody = htmlBody.Replace("{Email ID}", fromEmail);
            htmlBody = htmlBody.Replace("{REF}", refid);


            SendMail(fromEmail, CONTENTFORMAILMAILSUBJECT, htmlBody, fromAddr, smtpAddr, smtpPort, mailSecret);
        }
        public static void SendOTP(string EmailID, string OTP, string imgUrl, string fromAddr, string smtpAddr, string smtpPort, string mailSecret, string path)
        {
            string htmlBody = string.Empty;
            string otp = Crypto.AES_DECRYPT(OTP, COMMON.EMAILKEY);
            string fromEmail = Crypto.AES_DECRYPT(EmailID, COMMON.EMAILKEY);
            string lstrImageURL = imgUrl;
            using (StreamReader reader = new StreamReader(path))
            {
                htmlBody = reader.ReadToEnd();
            }

            htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
            htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
            htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
            htmlBody = htmlBody.Replace("{Email ID}", fromEmail);
            htmlBody = htmlBody.Replace("{OTP}", otp);


            SendMail(fromEmail, OTPFORMAILMAILSUBJECT, htmlBody, fromAddr, smtpAddr, smtpPort, mailSecret);
        }
        public static void SendLink(string EmailID, string Link, string uniqueId, string imgUrl, string fromAddr, string smtpAddr, string smtpPort, string mailSecret)
        {
            string htmlBody = string.Empty;
            string link = Crypto.AES_DECRYPT(Link, COMMON.EMAILKEY);
            string fromEmail = Crypto.AES_DECRYPT(EmailID, COMMON.EMAILKEY);
            string uniId = Crypto.AES_DECRYPT(uniqueId, COMMON.EMAILKEY);
            string lstrImageURL = "";
            // using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Template/SendLink.html")))
            using (StreamReader reader = new StreamReader("Template/SendLink.html"))
            {
                htmlBody = reader.ReadToEnd();
            }
        
            htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
            htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
            htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
            htmlBody = htmlBody.Replace("{Email ID}", fromEmail);
            htmlBody = htmlBody.Replace("{Link}", link);
            htmlBody = htmlBody.Replace("{uniqueId}", uniId);


            SendMail(fromEmail, OTPFORMAILMAILSUBJECT, htmlBody, fromAddr, smtpAddr, smtpPort, mailSecret);
        }


        //public static void Sendapproval(string EmailID, string RefId)
        //{
        //    string htmlBody = string.Empty;
        //    string refid = Crypto.AES_DECRYPT(RefId, COMMON.EMAILKEY);
        //    string fromEmail = Crypto.AES_DECRYPT(EmailID, COMMON.EMAILKEY);
        //    string lstrImageURL = ConfigurationManager.AppSettings["IMAGEURLPATH"];
        //    var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/SomePath");
        //    using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Template/RefId.html")))
        //    {
        //        htmlBody = reader.ReadToEnd();
        //    }

        //    htmlBody = htmlBody.Replace("{imglogopath}", lstrImageURL + "maillogo.png");
        //    htmlBody = htmlBody.Replace("{imglogocolorpath}", lstrImageURL + "maillogo_color.png");
        //    htmlBody = htmlBody.Replace("{imgbannerpath}", lstrImageURL + "mailbanner.png");
        //    htmlBody = htmlBody.Replace("{Email ID}", fromEmail);
        //    htmlBody = htmlBody.Replace("{REF}", refid);


        //    SendMail(fromEmail, USERAPPROVAL, htmlBody);
        //}

 


    }



}

