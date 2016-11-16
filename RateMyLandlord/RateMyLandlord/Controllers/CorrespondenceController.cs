using RateMyLandlord.Models.ViewModels.Correspondence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Controllers
{
    public class CorrespondenceController : Controller
    {
        // GET: Correspondence
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactEmailViewModel contactMessage)
        {
            //Validate contact message input
            if (contactMessage == null)
            {
                ModelState.AddModelError("", "No Message Provided");
                return View();
            }

            if (string.IsNullOrWhiteSpace(contactMessage.Name) || 
                string.IsNullOrWhiteSpace(contactMessage.Email) ||
                string.IsNullOrWhiteSpace(contactMessage.Subject) ||
                string.IsNullOrWhiteSpace(contactMessage.Message))
            {
                ModelState.AddModelError("", "All Fields are required.");
                return View();
            }

            //Create email message object
            System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

            // populate message
            email.To.Add("ratemylandlord03@gmail.com");
            email.From = new System.Net.Mail.MailAddress(contactMessage.Email);
            email.Subject = contactMessage.Subject;
            email.Body = string.Format(
                "Name: {0}\r\nMessage: {1}",
                contactMessage.Name,
                contactMessage.Message
                );
            email.IsBodyHtml = false;

            //set up SMTP client
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
            smtpClient.Host = "mail.twc.com";

            //send message
            smtpClient.Send(email);

            //notify user that the message was sent. 
            return View("emailConfirmation");
        }
    }
}