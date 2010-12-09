using System.Linq;
using System.Net.Mail;
using AutoMapper;
using AutoMapperAssist;
using AutoMoq;
using Deg.Alt.ContentProvider.RelatedItemReaders;
using Deg.Alt.Mvc.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deg.Alt.Mvc.Tests.Mappers
{
    [TestClass]
    public class EmailToMailMessageMapperTests
    {
        private AutoMoqer mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [TestMethod]
        public void Assert_configuration_is_valid()
        {
            var mapper = mocker.Resolve<EmailToMailMessageMapper>();
            try
            {
                mapper.AssertConfigurationIsValid();
            }
            catch (AutoMapperConfigurationException autoMapperConfigurationException)
            {
                if (TheConfigurationErrorIsExpectedBecauseAutoMapperReturnsTwoFieldsThatAreNotAccessible(autoMapperConfigurationException))
                {
                }
                else
                {
                    throw autoMapperConfigurationException;
                }
            }
        }

        private bool TheConfigurationErrorIsExpectedBecauseAutoMapperReturnsTwoFieldsThatAreNotAccessible(AutoMapperConfigurationException autoMapperConfigurationException)
        {
            return autoMapperConfigurationException.Message.StartsWith("The following 2 properties on System.Net.Mail.MailMessage are not mapped:");
        }

        [TestMethod]
        public void Returns_a_mail_message_when_passed_an_email()
        {
            var mapper = mocker.Resolve<EmailToMailMessageMapper>();

            var mailMessage = mapper.CreateInstance(new Email());

            Assert.IsNotNull(mailMessage);
        }

        [TestMethod]
        public void Sets_From_Address_to_the_from_email()
        {
            var email = new Email{FromEmail = "test@test.com"};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual("test@test.com", mailMessage.From.Address);
        }

        [TestMethod]
        public void Sets_the_From_DisplayName_from_the_from_name()
        {
            var email = new Email{FromEmail = "test@test.com", FromName = "Name"};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual("Name", mailMessage.From.DisplayName);
        }

        [TestMethod]
        public void Sets_From_to_null_if_the_from_email_is_null()
        {
            var email = new Email{FromEmail = null};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(null, mailMessage.From);
        }

        [TestMethod]
        public void Sets_From_to_null_if_the_from_email_is_empty()
        {
            var email = new Email{FromEmail = string.Empty};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(null, mailMessage.From);
        }

        [TestMethod]
        public void Sets_To_Address_to_the_to_email()
        {
            var email = new Email{ToEmail = "to@email.com"};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(1, mailMessage.To.Count());
            Assert.AreEqual("to@email.com", mailMessage.To.First().Address);
        }

        [TestMethod]
        public void Sets_To_DisplayName_from_the_to_name()
        {
            var email = new Email{ToEmail = "to@email.com", ToName = "Name"};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(1, mailMessage.To.Count());
            Assert.AreEqual("Name", mailMessage.To.First().DisplayName);
        }

        [TestMethod]
        public void Sets_To_to_null_if_the_to_email_is_null()
        {
            var email = new Email{ToEmail = null};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(0, mailMessage.To.Count());
        }

        [TestMethod]
        public void Does_not_set_ToEmail_Address_if_the_to_email_is_empty()
        {
            var email = new Email{ToEmail = string.Empty};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(0, mailMessage.To.Count());
        }

        [TestMethod]
        public void Sets_IsBodyHtml_to_true_when_the_html_body_is_not_empty()
        {
            var email = new Email{HtmlBody = "test"};

            var mailMessage = GetMailMessage(email);
            Assert.IsTrue(mailMessage.IsBodyHtml);
        }

        [TestMethod]
        public void Sets_IsBodyHtml_to_false_when_the_html_body_is_empty()
        {
            var email = new Email{HtmlBody = string.Empty};

            var mailMessage = GetMailMessage(email);
            Assert.IsFalse(mailMessage.IsBodyHtml);
        }

        [TestMethod]
        public void Sets_IsBodyHtml_to_false_when_the_html_body_is_null()
        {
            var email = new Email{HtmlBody = null};

            var mailMessage = GetMailMessage(email);
            Assert.IsFalse(mailMessage.IsBodyHtml);
        }

        [TestMethod]
        public void Sets_Body_to_html_body_when_the_html_body_is_not_empty()
        {
            var email = new Email{HtmlBody = "TEST"};

            var mailMessage = GetMailMessage(email);
            Assert.AreEqual("TEST", mailMessage.Body);
        }

        [TestMethod]
        public void Sets_body_to_text_body_when_the_html_body_is_empty()
        {
            var email = new Email{HtmlBody = "", TextBody = "TEST"};

            var mailMessage = GetMailMessage(email);
            Assert.AreEqual("TEST", mailMessage.Body);
        }

        [TestMethod]
        public void Sets_body_to_text_body_when_the_html_body_is_null()
        {
            var email = new Email{HtmlBody = null, TextBody = "TEST"};

            var mailMessage = GetMailMessage(email);
            Assert.AreEqual("TEST", mailMessage.Body);
        }

        [TestMethod]
        public void Sets_body_to_empty_when_both_html_body_and_text_body_are_null()
        {
            var email = new Email{HtmlBody = null, TextBody = null};

            var mailMessage = GetMailMessage(email);
            Assert.AreEqual(string.Empty, mailMessage.Body);
        }

        [TestMethod]
        public void Adds_one_address_when_Bcc_has_one_email_address()
        {
            var email = new Email{BccEmail = "test@test.com"};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(1, mailMessage.Bcc.Count());
            Assert.AreEqual("test@test.com", mailMessage.Bcc.First().Address);
        }

        [TestMethod]
        public void Adds_two_addresses_when_Bcc_has_two_email_addresses()
        {
            var email = new Email{BccEmail = "test@test.com;test2@test.com"};

            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(2, mailMessage.Bcc.Count());
            Assert.AreEqual("test@test.com", mailMessage.Bcc.ToList()[0].Address);
            Assert.AreEqual("test2@test.com", mailMessage.Bcc.ToList()[1].Address);
        }

        [TestMethod]
        public void Adds_one_address_when_extra_semicolons_are_at_the_end_of_bcc()
        {
            var email = new Email{BccEmail = "test@test.com;;;;"};

            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(1, mailMessage.Bcc.Count());
            Assert.AreEqual("test@test.com", mailMessage.Bcc.ToList()[0].Address);
        }

        [TestMethod]
        public void Does_not_add_bcc_when_Bcc_is_null()
        {
            var email = new Email{BccEmail = null};

            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(0, mailMessage.Bcc.Count());
        }

        [TestMethod]
        public void Does_not_add_bcc_when_Bcc_is_empty()
        {
            var email = new Email{BccEmail = string.Empty};

            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(0, mailMessage.Bcc.Count());
        }

        [TestMethod]
        public void Adds_one_address_when_cc_has_one_email_address()
        {
            var email = new Email{CcEmail = "test@test.com"};
            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(1, mailMessage.CC.Count());
            Assert.AreEqual("test@test.com", mailMessage.CC.First().Address);
        }

        [TestMethod]
        public void Adds_two_addresses_when_Cc_has_two_email_addresses()
        {
            var email = new Email{CcEmail = "test@test.com;test2@test.com"};

            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(2, mailMessage.CC.Count());
            Assert.AreEqual("test@test.com", mailMessage.CC.ToList()[0].Address);
            Assert.AreEqual("test2@test.com", mailMessage.CC.ToList()[1].Address);
        }

        [TestMethod]
        public void Adds_one_address_when_extra_semicolons_are_at_the_end_of_cc()
        {
            var email = new Email{CcEmail = "test@test.com;;;;"};

            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(1, mailMessage.CC.Count());
            Assert.AreEqual("test@test.com", mailMessage.CC.ToList()[0].Address);
        }

        [TestMethod]
        public void Does_not_add_cc_when_cc_is_null()
        {
            var email = new Email{CcEmail = null};

            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(0, mailMessage.CC.Count());
        }

        [TestMethod]
        public void Does_not_add_cc_when_Cc_is_empty()
        {
            var email = new Email{CcEmail = string.Empty};

            var mailMessage = GetMailMessage(email);

            Assert.AreEqual(0, mailMessage.CC.Count());
        }

        private MailMessage GetMailMessage(Email email)
        {
            var mapper = mocker.Resolve<EmailToMailMessageMapper>();

            return mapper.CreateInstance(email);
        }
    }
}