using System;

namespace IAmBacon.Core.Application.Email.Commands
{
    public class SendEmailCommand
    {
        public string Name { get; }
        public string Email { get; }
        public string Subject { get; }
        public string HtmlMessage { get; }

        public SendEmailCommand(string name, string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(subject));
            if (string.IsNullOrWhiteSpace(htmlMessage)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(htmlMessage));

            Name = name;
            Email = email;
            Subject = subject;
            HtmlMessage = htmlMessage;
        }
    }
}
