using MimeKit;
using MailKit.Net.Smtp;

namespace th_poker_api.DTO.Email
{

    public class EmailAddress
    {
        public string Address { get; set; }
        public string DisplayName { get; set; }
    }

    public class MailSenderDTO
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }

        public MailSenderDTO(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(string.Empty, x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
/*
    public string? EmailSender { get; set; }
    public string? EmailReciever { get; set; }
    public string? EmailBody { get; set; }
    public string? EmailSubject { get; set; }
    public string? EmailCc { get; set; }
    public string? EmailBcc { get; set; }*/

}
