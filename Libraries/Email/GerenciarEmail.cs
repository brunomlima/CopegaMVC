using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;
using CopegeMVC.Models;

namespace CopegeMVC.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _config;
        public GerenciarEmail(SmtpClient smtp, IConfiguration config)
        {
            _config = config;
            _smtp = smtp;
        }

        public string LayoutEmailTBViagens(string Titulo, string Conteudo)
        {
            string sHtmlEmail = string.Format("<html>" +
                                              "<head>" +
                                              "    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />" +
                                              "    <title>E-mail TBViagens</title>" +
                                              "    <meta name='viewport' content='width=device-width, initial-scale=1.0' />" +
                                              "</head>" +
                                              "<body style='margin: 0; padding: 0;'>" +
                                              "    <table border='0' cellpadding='0' cellspacing='0' width='100%'>" +
                                              "        <tr>" +
                                              "            <td>" +
                                              "                <table align='center' border='0' cellpadding='0' cellspacing='0' width='600' style='border-collapse: collapse;'>" +
                                              "                    <tr>" +
                                              "                        <td align='center' bgcolor='#DDDDDD' style='padding: 40px 0 30px 0;'>" +
                                              "                            <img src='https://" + _config.GetValue<string>("Empresa:Dominio") + "/images/LogoCOPEGE.png' alt='Copege de E-mail' style='display: block;' />" +
                                              "                        </td>" +
                                              "                    </tr>" +
                                              "                    <tr>" +
                                              "                        <td bgcolor='#FFCF09' style='padding: 40px 30px 40px 30px;'>" +
                                              "                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>" +
                                              "                                <tr>" +
                                              "                                    <td style='color: #153643; font-family: Arial, sans-serif; font-size: 24px;'>" +
                                              "                                        {0}" +
                                              "                                    </td>" +
                                              "                                </tr>" +
                                              "                                <tr>" +
                                              "                                    <td style='padding: 10px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>" +
                                              "                                        {1}" +
                                              "                                    </td>" +
                                              "                                </tr>" +
                                              "                            </table>" +
                                              "                        </td>" +
                                              "                    </tr>" +
                                              "                    <tr>" +
                                              "                        <td bgcolor='#2D305B' style='padding: 30px 30px 30px 30px;'>" +
                                              "                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>" +
                                              "                                <tr>" +
                                              "                                    <td width='75%' style='color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;'>" +
                                              "                                        <font color='#ffffff'>Fique tranquilo.</font>" +
                                              "                                    </td>" +
                                              "                                    <td width='75%' align='right'>" +
                                              "                                        <table border='0' cellpadding='0' cellspacing='0'>" +
                                              "                                            <tr>" +
                                              "                                                <td style='font-size: 0; line-height: 0;' width='20'> </td>" +
                                              "                                                <td>" +
                                              "                                                    <a href='https://wa.me/"+ _config.GetValue<string>("Empresa:Whatsapp") + "?text='>" +
                                              "                                                    <img src='https://www.copege.com.br/images/WhatsApp.png' alt='Facebook' width='32' height='32' style='display: block;' border='0' />" +
                                              "                                                    </a>" +
                                              "                                                </td>" +
                                              "                                            </tr>" +
                                              "                                        </table>" +
                                              "                                    </td>" +
                                              "                                </tr>" +
                                              "                            </table>" +
                                              "                        </td>" +
                                              "                    </tr>" +
                                              "                </table>" +
                                              "            </td>" +
                                              "        </tr>" +
                                              "    </table>" +
                                              "</body>" +
                                              "</html>", Titulo, Conteudo);
            return sHtmlEmail;
        }
        public void EnviarContatoPorEmail(Contato contato)
        {
            string tituloMsg = "<h2> Contato - " + _config.GetValue<string>("Empresa:NomeFantasia") + " </h2>";

            string corpoMsg = string.Format("<b>Nome:</b> {0} <br />" +
                                            "<b>Email:</b> {1} <br />" +
                                            "<b>Mensagem:</b> {2} <br />" +
                                            "E-mail enviado automaticamente pelo site.", contato.nome, contato.email, contato.mensagem);

            string Msg = LayoutEmailTBViagens(tituloMsg, corpoMsg);

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_config.GetValue<string>("Email:Usuario"));
            mensagem.To.Add(_config.GetValue<string>("Email:Remetente"));
            mensagem.Subject = string.Format(_config.GetValue<string>("Empresa:NomeFantasia") + " - Email enviado pelo cliente {0} do e-mail {1} ", contato.nome, contato.email);
            mensagem.Body = Msg;
            mensagem.IsBodyHtml = true;
            _smtp.Send(mensagem);
        }

    }
}
