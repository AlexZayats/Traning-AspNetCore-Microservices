﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System;
using System.Threading.Tasks;

namespace Traning.AspNetCore.Microservices.Email.API.Managers
{
    public class MailManager : IMailManager
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailManager> _logger;
        private readonly ITracer _tracer;

        public MailManager(IConfiguration configuration, ILogger<MailManager> logger, ITracer tracer)
        {
            _configuration = configuration;
            _logger = logger;
            _tracer = tracer;
        }

        public Task Send(string email, string subject, string body)
        {
            using (var scope = _tracer.BuildSpan("Email").StartActive(finishSpanOnDispose: true))
            {
                _logger.LogInformation($"Email:{Environment.NewLine}{email}{Environment.NewLine}{subject}{Environment.NewLine}{body}");
                return Task.CompletedTask;
            }
        }
    }
}
