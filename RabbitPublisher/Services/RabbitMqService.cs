﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitSender.Interfaces;
using RabbitSender.Model;

namespace RabbitSender.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly AppSettings _settings;
    private readonly ILogger<RabbitMqService> _logger;

    [SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
    public RabbitMqService(IOptions<AppSettings> options, ILogger<RabbitMqService> logger)
    {
        _logger = logger;
        _settings = options.Value;
    }

    public IConnection? CreateChannel()
    {
        ConnectionFactory factory = new()
        {
            Uri = new Uri(_settings.RabbitMq!.RabbitUri),
            ClientProvidedName = "Rabbit Publisher App"
        };

        var channel = factory.CreateConnection();
        
        if(channel != null)
            _logger.LogInformation("[RabbitMQ]: Channel successfully created");
            
        return channel;
    }
}