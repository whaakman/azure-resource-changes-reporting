using System;

public class SubscriptionModel
{
    public string Id { get; set; } = Environment.GetEnvironmentVariable("subscriptionId");
}