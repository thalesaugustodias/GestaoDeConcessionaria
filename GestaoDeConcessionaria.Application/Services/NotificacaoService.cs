using GestaoDeConcessionaria.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace GestaoDeConcessionaria.Application.Services
{
    public class NotificacaoService
    {
        private readonly TwilioConfig _twilio;
        private readonly SmtpConfig _smtp;

        public NotificacaoService(IOptions<TwilioConfig> twilioOptions, IOptions<SmtpConfig> smtpOptions)
        {
            _twilio = twilioOptions.Value;
            _smtp = smtpOptions.Value;
        }

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
