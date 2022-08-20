using Coravel.Invocable;
using FluentEmail.Core;

namespace BackgroundWorker;

//We implement the IInvocable interface from the Coravel.Invocable library
public class ProcessOrder : IInvocable
{
    private readonly ILogger<ProcessOrder> _logger;
    private readonly IFluentEmail _email;

    public ProcessOrder(ILogger<ProcessOrder> logger, IFluentEmail email)
    {
        _logger = logger;
        _email = email;
    }

    public async Task Invoke()
    {
        var order = new OrderInfo()
        {
            OrderId = Guid.NewGuid(),
            ItemName = "Cheese Pizza",
            QuantityOrderded = 2,
            CustomerName = "Bob",
            CustomerEmail = "test@test.com"
        };

        _logger.LogWarning("Warning");
        //This '@' sign is important to say to serilog that it needs to deal with serialization
        _logger.LogInformation("Processing order {@order}", order);

        //We create an email template for Razor
        var emailTemplate =
        @"<p>Dear @Model.CustomerName,</p> 
                    <p>Your order of @Model.QuantityOrdered @Model.ItemName has been received!</p>
                    <p>Sincerely,<br>Some Person</p>";

        var newEmail = _email
            .To(order.CustomerEmail)
            .Subject($"Thanks for your order {order.CustomerName}")
            .UsingTemplate(emailTemplate, order);

        await newEmail.SendAsync();
    }
}
