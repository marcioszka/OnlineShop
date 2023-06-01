using System;
using System.IO;
using System.Net.Mail;
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Models;

public class Payment
{
    public void ProcessPayment(Order order)
    {
        try
        {
            // Code for making the payment
            bool paymentSuccessful = MakePayment(order);

            if (paymentSuccessful)
            {
                DisplayConfirmationPage(order);

                SaveOrderToJson(order);

                SendConfirmationEmail(order);
            }
            else
            {
                DisplayErrorMessage("Payment failed. Please try again.");
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage("An error occurred while processing your payment: " + ex.Message);
        }
    }

    private bool MakePayment(Order order)
    {
        return true;
    }

    private void DisplayConfirmationPage(Order order)
    {
        Console.WriteLine("Confirmation Page");
        Console.WriteLine("Order ID: " + order.OrderId);
        Console.WriteLine("Order Details: " + order.OrderDetails);
        Console.WriteLine("Total Amount: " + order.TotalAmount);
        Console.WriteLine();
    }

    private void SaveOrderToJson(Order order)
    {
        // Save order as JSON
        string json = JsonConvert.SerializeObject(order);
        string filePath = "order.json"; // Replace with your desired file path
        File.WriteAllText(filePath, json);
    }

    private void SendConfirmationEmail(Order order)
    {
        // Send email to user
        string emailContent = "Thank you for your order!\n\n" +
            "Order ID: " + order.OrderId + "\n" +
            "Order Details: " + order.OrderDetails + "\n" +
            "Total Amount: " + order.TotalAmount + "\n";

        string userEmail = order.UserEmail; // Replace with actual user's email

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("your-email@example.com"); // Replace with your email address
        mail.To.Add(userEmail);
        mail.Subject = "Order Confirmation";
        mail.Body = emailContent;

        SmtpClient smtpClient = new SmtpClient("smtp.example.com"); // Replace with your SMTP server details
        smtpClient.Send(mail);
    }

    private void DisplayErrorMessage(string errorMessage)
    {
        // Code to display error message to the user
        Console.WriteLine(errorMessage);
        Console.WriteLine();
    }
}

public class Order
{
    public string OrderId { get; set; }
    public string OrderDetails { get; set; }
    public decimal TotalAmount { get; set; }
    public string UserEmail { get; set; }
}
