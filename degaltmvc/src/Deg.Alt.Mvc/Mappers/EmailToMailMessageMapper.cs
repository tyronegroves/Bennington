using System;
using System.Collections.Generic;
using System.Net.Mail;
using AutoMapper;
using AutoMapperAssist;
using Deg.Alt.ContentProvider.RelatedItemReaders;

namespace Deg.Alt.Mvc.Mappers
{
    public interface IEmailToMailMessageMapper
    {
        MailMessage CreateInstance(Email source);
        IEnumerable<MailMessage> CreateSet(IEnumerable<Email> source);
    }

    public class EmailToMailMessageMapper : Mapper<Email, MailMessage>, IEmailToMailMessageMapper
    {
        public override void DefineMap(IConfiguration configuration)
        {
            configuration.CreateMap<Email, MailMessage>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(orig => string.IsNullOrEmpty(orig.HtmlBody) ? orig.TextBody : orig.HtmlBody))
                .ForMember(dest => dest.IsBodyHtml, opt => opt.MapFrom(orig => string.IsNullOrEmpty(orig.HtmlBody) == false))
                .ForMember(dest => dest.Bcc, opt => opt.Ignore())
                .ForMember(dest => dest.CC, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.Ignore())
                .ForMember(dest => dest.From, opt => opt.Ignore())
                .ForMember(dest => dest.To, opt => opt.Ignore())
                .ForMember(dest => dest.ReplyTo, opt => opt.Ignore())
                .ForMember(dest => dest.BodyEncoding, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.AlternateViews, opt => opt.Ignore())
                .ForMember(dest => dest.Priority, opt => opt.Ignore())
                .ForMember(dest => dest.DeliveryNotificationOptions, opt => opt.Ignore())
                .ForMember(dest => dest.SubjectEncoding, opt => opt.Ignore());
        }

        public override MailMessage CreateInstance(Email source)
        {
            var mailMessage = base.CreateInstance(source);

            SetTheFromAddress(source, mailMessage);
            SetTheToAddresses(source, mailMessage);
            SetTheBccAddresses(source, mailMessage);
            SetTheCcAddresses(source, mailMessage);

            return mailMessage;
        }

        private static void SetTheCcAddresses(Email source, MailMessage mailMessage)
        {
            if (source.CcEmail != null)
            {
                var emails = source.CcEmail.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var email in emails)
                    mailMessage.CC.Add(email);
            }
        }

        private static void SetTheBccAddresses(Email source, MailMessage mailMessage)
        {
            if (source.BccEmail != null)
            {
                var emails = source.BccEmail.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var email in emails)
                    mailMessage.Bcc.Add(email);
            }
        }

        private static void SetTheToAddresses(Email source, MailMessage mailMessage)
        {
            if (string.IsNullOrEmpty(source.ToEmail) == false)
                if (source.ToName != null)
                    mailMessage.To.Add(new MailAddress(source.ToEmail, source.ToName));
                else
                    mailMessage.To.Add(new MailAddress(source.ToEmail));
        }

        private static void SetTheFromAddress(Email source, MailMessage mailMessage)
        {
            if (string.IsNullOrEmpty(source.FromEmail) == false)
                if (source.FromName != null)
                    mailMessage.From = new MailAddress(source.FromEmail, source.FromName);
                else
                    mailMessage.From = new MailAddress(source.FromEmail);
        }
    }
}