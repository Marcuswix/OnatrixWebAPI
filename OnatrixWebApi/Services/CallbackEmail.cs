using Azure.Communication.Email;

namespace OnatrixWebApi.Services
{
    public class CallbackEmail
    {
        public async Task<EmailContent> CallbackEmailResponseAsync(string message)
        {
            var emailContent = new EmailContent("Onatrix - Confirmation email")
            {
                Html = "<html>\r\n<body style=\"font-family: Arial, sans-serif; margin: 0; padding: 0;\">\r\n<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n<tr>\r\n<td align=\"center\" style=\"background-color: #f8f9fa; padding: 20px;\">\r\n <table width=\"600\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"background-color: #ffffff; padding: 20px; border-radius: 6px;\">\r\n <tr>\r\n                        <td align=\"center\" style=\"padding-bottom: 20px;\">\r\n                            <h1 style=\"font-size: 24px; color: #535656;\">We Have Recived Your Callback Request!</h1>\r\n                            <p style=\"font-size: 16px; color: #535656;\">You will recive a callback in a short while!.</p>\r\n <a href=\"https://example.com\" style=\"display: inline-block; padding: 10px 20px; background-color: #4F5955; color: #F2EDDC; text-decoration: none; border-radius: 4px; font-size: 16px;\">To Our website</a>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"padding: 10px 0; border-top: 1px solid #dee2e6; text-align: center; font-size: 12px; color: #adb5bd;\">\r\n                            This is an auto generated message, please do not answer.\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n</html>\r\n",
                PlainText = "Thanks for your callback request. This is a confirmation email."
            };

            return await Task.FromResult(emailContent);
        }
    }
}
