﻿namespace IAmBacon.Core.Domain.ValueObject.Configuration
{
    public class EmailConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
