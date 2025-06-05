
using Microsoft.Extensions.Options;

namespace GestaoDeConcessionaria.Application.Services
{
    public class NotificacaoService(IOptions<TwilioConfig> twilioOptions, IOptions<SmtpConfig> smtpOptions)
    {
        private readonly TwilioConfig _twilio = twilioOptions.Value;
        private readonly SmtpConfig _smtp = smtpOptions.Value;

        public void EnviarSms()
        {
            Console.WriteLine($"Enviando SMS de {_twilio.FromPhone}");
        }

        public void EnviarEmail()
        {
            Console.WriteLine($"Enviando e-mail usando SMTP: {_smtp.Host}:{_smtp.Port}");
        }
    }

}
