using Azure.Communication.Email;

namespace OnatrixWebApi.Services
{
    public class QuestionEmail
    {
        public async Task<EmailContent> QuestionEmailResponseAsync(string message, string name)
        {

            var emailContent = new EmailContent("Onatrix - comfirmation email")
            {
                Html = $@"
                <html>
                <body style='font-family: Arial, sans-serif; margin: 0; padding: 0;'>
                    <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                        <tr>
                            <td align='center' style='background-color: #f8f9fa; padding: 20px;'>
                                <table width='600' cellpadding='0' cellspacing='0' border='0' style='background-color: #ffffff; padding: 20px; border-radius: 6px;'>
                                    <tr>
                                        <td align='center' style='padding-bottom: 20px;'>
                                            <h1 style='font-size: 24px; color: #535656;'>Thanks for your question, {name}!</h1>
                                            <p style='font-size: 16px; color: #535656;'>We will answer it in a short while!</p>
                                            <a href='https://example.com' style='display: inline-block; padding: 10px 20px; background-color: #4F5955; color: #F2EDDC; text-decoration: none; border-radius: 4px; font-size: 16px;'>To Our website</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='padding: 10px 0; border-top: 1px solid #dee2e6; text-align: center; font-size: 12px; color: #adb5bd;'>
                                            <p>Your Question: {message}</p>
                                            <p>This is an auto-generated message, please do not reply.</p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>",
                PlainText = "Onatrix - Thanks for you question. This is an auto-generated message, please do not reply"
            };

            return await Task.FromResult(emailContent);
        }
    }
}
