using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;
using System.Net.Mime;

public class Email
{

    private IConfiguration _config;

    public Email(IConfiguration configuration)
	{
        _config = configuration;
	}
    public string EnviarAnexo(string emailDestinatario,string nomeDestinatario,string mensagem, string titulo, string caminho_anexo)
    {
        //cria objeto com dados do e-mail 
        MailMessage objEmail = new MailMessage();

        //remetente do e-mail 
        objEmail.From = new MailAddress(_config.GetValue<string>("EmailRemetente"), _config.GetValue<string>("EmailNomeRemetente"));
        
        //destinatários do e-mail 
        MailAddress oTo = new MailAddress(emailDestinatario, nomeDestinatario);

        objEmail.To.Add(oTo);
        
        //prioridade do e-mail 
        objEmail.Priority = MailPriority.Normal;

        //formato do e-mail HTML (caso não queira HTML alocar valor false) 
        objEmail.IsBodyHtml = true;

        //título do e-mail 
        objEmail.Subject = titulo.ToString();

        //corpo do e-mail 
        objEmail.Body = mensagem.ToString();
        //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1" 
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

        // Cria o anexo para o e-mail
        Attachment anexo = new Attachment(caminho_anexo, MediaTypeNames.Application.Pdf);

        // Anexa o arquivo a mensagem
        objEmail.Attachments.Add(anexo);
        
        //cria objeto com os dados do SMTP 
        SmtpClient objSmtp = new SmtpClient();

        System.Net.NetworkCredential autenticacao = new System.Net.NetworkCredential();
        autenticacao.UserName = _config.GetValue<string>("EmailUserName");
        autenticacao.Password = _config.GetValue<string>("EmailPassword");
        //alocamos o endereço do host para enviar os e-mails, smtp2.locaweb.com.br 
        objSmtp.Host = _config.GetValue<string>("EmailHost");
        objSmtp.UseDefaultCredentials = false;
        objSmtp.Port = _config.GetValue<int>("EmailPorta");
        objSmtp.Credentials = autenticacao;
        
        //enviamos o e-mail através do método .send() 
        try
        {
            objSmtp.Send(objEmail);
            return "E-mail enviado com sucesso! Para o e-mail " + emailDestinatario.ToString();
        }
        catch (Exception ex)
        {
            return "Ocorreram problemas no envio do e-mail. Error = " + ex.ToString();            
        }
    }

    public string Enviar(string emailDestinatario, string nomeDestinatario, string mensagem, string titulo)
    {
        //cria objeto com dados do e-mail 
        MailMessage objEmail = new MailMessage();

        //remetente do e-mail 
        objEmail.From = new MailAddress(_config.GetValue<string>("EmailRemetente"), _config.GetValue<string>("EmailNomeRemetente"));

        //destinatários do e-mail 
        MailAddress oTo = new MailAddress(emailDestinatario, nomeDestinatario);

        objEmail.To.Add(oTo);

        //prioridade do e-mail 
        objEmail.Priority = MailPriority.Normal;

        //formato do e-mail HTML (caso não queira HTML alocar valor false) 
        objEmail.IsBodyHtml = true;

        //título do e-mail 
        objEmail.Subject = titulo.ToString();

        //corpo do e-mail 
        objEmail.Body = mensagem.ToString();
        //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1" 
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

        //cria objeto com os dados do SMTP 
        SmtpClient objSmtp = new SmtpClient();

        System.Net.NetworkCredential autenticacao = new System.Net.NetworkCredential();
        autenticacao.UserName = _config.GetValue<string>("EmailUserName");
        autenticacao.Password = _config.GetValue<string>("EmailPassword");
        //alocamos o endereço do host para enviar os e-mails, smtp2.locaweb.com.br 
        objSmtp.Host = _config.GetValue<string>("EmailHost");
        objSmtp.UseDefaultCredentials = false;
        objSmtp.Port = _config.GetValue<int>("EmailPorta");
        objSmtp.Credentials = autenticacao;

        //enviamos o e-mail através do método .send() 
        try
        {
            objSmtp.Send(objEmail);
            return "E-mail enviado com sucesso! Para o e-mail " + emailDestinatario.ToString();
        }
        catch (Exception ex)
        {
            return "Ocorreram problemas no envio do e-mail. Error = " + ex.ToString();
        }
    }
}

