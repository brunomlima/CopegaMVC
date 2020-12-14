using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;
using CopegeMVC.Libraries.Email;

namespace CopegeMVC.Configurations
{
    public static class SmtpConfig
    {
        public static IServiceCollection AddSmtpConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<SmtpClient>(options =>
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = configuration.GetValue<string>("Email:ServidorSMTP"),
                    Port = configuration.GetValue<int>("Email:ServidorPorta"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(configuration.GetValue<string>("Email:Usuario"), configuration.GetValue<string>("Email:Senha")),
                    EnableSsl = false
                };
                return smtp;
            });
            services.AddScoped<GerenciarEmail>();
            return services;
        }
    }
}
