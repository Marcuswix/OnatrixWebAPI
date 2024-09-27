using Azure;
using Azure.Communication.Email;
using Microsoft.AspNetCore.Mvc;
using OnatrixWebApi.Models;
using OnatrixWebApi.Services;
using OnatrixWebAPI.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnatrixWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly EmailClient _emailClient;
        private readonly IConfiguration _configuration;
        private readonly CallbackEmail _callbackEmail;
        private readonly QuestionEmail _questionEmail;

        public MessageController(EmailClient emailClient, IConfiguration configuration, QuestionEmail questionEmail, CallbackEmail callbackEmail)
        {
            _emailClient = emailClient;
            _configuration = configuration;
            _questionEmail = questionEmail;
            _callbackEmail = callbackEmail;
        }

        [HttpPost("callback-question")]
        public async Task <IActionResult> PostCallbackAndQuestion([FromBody] ContactFormToSendModel model)
        {
            if(model.Email != null)
            {
                var recipientEmailAddress = model.Email.ToString();

                EmailContent emailContent = new EmailContent("Onatrix Confirmation Email");
                
                if(model.FormName == "callback")
                {
                    emailContent = await _callbackEmail.CallbackEmailResponseAsync(model.Message);
                }
                else if(model.FormName == "question")
                {
                    emailContent = await _questionEmail.QuestionEmailResponseAsync(model.Message, model.Name);
                }
                else
                {
                    emailContent = new EmailContent("Onatrix Confirmation Email")
                    {
                        Html = "<html>\r\n<body style=\"font-family: Arial, sans-serif; margin: 0; padding: 0;\">\r\n<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n<tr>\r\n<td align=\"center\" style=\"background-color: #f8f9fa; padding: 20px;\">\r\n <table width=\"600\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"background-color: #ffffff; padding: 20px; border-radius: 6px;\">\r\n <tr>\r\n                        <td align=\"center\" style=\"padding-bottom: 20px;\">\r\n                            <h1 style=\"font-size: 24px; color: #535656;\">We Have Recived Your Help Request!</h1>\r\n                            <p style=\"font-size: 16px; color: #535656;\">We will help you in a short will!</p>\r\n <a href=\"https://example.com\" style=\"display: inline-block; padding: 10px 20px; background-color: #4F5955; color: #F2EDDC; text-decoration: none; border-radius: 4px; font-size: 16px;\">To Our website</a>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"padding: 10px 0; border-top: 1px solid #dee2e6; text-align: center; font-size: 12px; color: #adb5bd;\">\r\n                            This is an automatic generated message, please do not answer.\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n</html>\r\n",
                        PlainText = "Onatrix confirmation email. This is an auto-generated message, please do not reply."
                    };
                }

                var emailSender = _configuration["Values:SenderDomain"];

                var emailToMessage = new EmailMessage(emailSender, recipientEmailAddress, emailContent);

                var response = await _emailClient.SendAsync(WaitUntil.Completed, emailToMessage);

                if(response.HasCompleted)
                {
                    return new OkObjectResult(new
                    {
                        Success = true,
                        Messages = "Email sent successfully"     
                    });
                }
                else
                {
                    return new NotFoundObjectResult(new
                    {
                        Success = false,
                        Message = "No recipient by that emailaddress"
                    });
                }                         
            }
			return new BadRequestObjectResult(new
			{
				Success = false,
				Message = "Email sending not completed"
			});
		}

        // POST api/<MessageController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HelpYouFormToSendModel value)
        {
            if(value != null)
            {
                var recipientAddress = value?.HelpEmail.ToString();

                var emailContent= new EmailContent("Onatrix Confirmation Email")
                {
                    Html = "<html>\r\n<body style=\"font-family: Arial, sans-serif; margin: 0; padding: 0;\">\r\n<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n<tr>\r\n<td align=\"center\" style=\"background-color: #f8f9fa; padding: 20px;\">\r\n <table width=\"600\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"background-color: #ffffff; padding: 20px; border-radius: 6px;\">\r\n <tr>\r\n                        <td align=\"center\" style=\"padding-bottom: 20px;\">\r\n                            <h1 style=\"font-size: 24px; color: #535656;\">We Have Recived Your Help Request!</h1>\r\n                            <p style=\"font-size: 16px; color: #535656;\">We will help you in a short will! How? No idea...</p>\r\n <a href=\"https://onatrix-marcuswix-f3ccasfxa3encgcp.westeurope-01.azurewebsites.net/home/\" style=\"display: inline-block; padding: 10px 20px; background-color: #4F5955; color: #F2EDDC; text-decoration: none; border-radius: 4px; font-size: 16px;\">To Our website</a>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"padding: 10px 0; border-top: 1px solid #dee2e6; text-align: center; font-size: 12px; color: #adb5bd;\">\r\n                            This is an automatic generated message, please do not answer.\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n</html>\r\n",
                    PlainText = "Tack för din förfrågan. Detta är ditt bekräftelsemail."
                };
                try
                {
                    string sender = _configuration["Values:SenderDomain"]!;
                    var emailToSend = new EmailMessage(sender, recipientAddress, emailContent);
                    var result = await _emailClient.SendAsync(WaitUntil.Completed, emailToSend);

                    if(result.HasCompleted)
                    {
                        return new OkResult();
                    }
                }
                catch (Exception ex) 
                {
                    Console.Write(ex.Message.ToString());
                }
            }
            return new BadRequestResult();
        }
    }
}
