using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.ValueObject.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace IAmBacon.Core.Application.Email.Commands
{
    public class EmailCommandHandler : ICommandHandler<SendEmailCommand>
    {
        private readonly EmailConfiguration _configuration;

        private const string SystemEmailAddress = "email@iambacon.co.uk";

        private const string SystemDisplayName = "iambacon.co.uk";

        public EmailCommandHandler(EmailConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task HandleAsync(SendEmailCommand command)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(SystemDisplayName, SystemEmailAddress));
            message.To.Add(new MailboxAddress(command.Name, command.Email));
            message.Subject = command.Subject;
            message.Body = new TextPart(TextFormat.Html) { Text = command.HtmlMessage };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                // Not sure if I need this or not?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_configuration.Host, _configuration.Port, false);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
