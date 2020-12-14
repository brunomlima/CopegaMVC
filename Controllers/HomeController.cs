using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotDetect.Web.Mvc;
using CopegeMVC.Models;
using CopegeMVC.Libraries.Email;

namespace CopegeMVC.Models
{
    public class HomeController : Controller
    {
        private GerenciarEmail _gerenciarEmail;
        public HomeController(GerenciarEmail gerenciarEmail)
        {
            _gerenciarEmail = gerenciarEmail;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SobreNos()
        {
            return View();
        }
        public IActionResult Servicos()
        {
            return View();
        }
        public IActionResult Cotacao()
        {
            return View();
        }
        public IActionResult Contato()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "ContactCaptcha", "Captcha errado!")]
        public IActionResult ContatoAcao([FromForm] Contato contato)
        {
            try
            {
                string userInput = HttpContext.Request.Form["CaptchaCode"];
                MvcCaptcha mvcCaptcha = new MvcCaptcha("ExampleCaptcha");
                if (!mvcCaptcha.Validate(userInput))
                {
                    ModelState.AddModelError("CaptchaCode", "Captcha errado!");
                    ViewData["CONTATO"] = contato;
                    return View("Contato", contato);
                }
                
                ModelState.Remove("captchacode");

                if (ModelState.IsValid)
                {
                    _gerenciarEmail.EnviarContatoPorEmail(contato);
                    ViewData["MSG_S"] = "Mensagem de contato enviada com sucesso!";
                    ViewData["MSG_E"] = "";
                    MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                    return View("Contato");
                }
                else
                {
                    ViewData["CONTATO"] = contato;
                    return View("Contato", contato);
                }

            }
            catch (Exception)
            {
                ViewData["MSG_E"] = "Opsss! Tivemos um erro, tem novamente mais tarde!";
            }
            return View("Contato");

        }

    }
}
