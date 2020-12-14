using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
//using TBViagensMVC.Libraries.Agendador.Scheduler.Invocable;
//using TBViagensMVC.Libraries.Login;
//using TBViagensMVC.Libraries.Sessoes;
//using TBViagensMVC.Repositories;
//using TBViagensMVC.Repositories.Contracts;

namespace CopegeMVC.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<INewsletterRepository, NewsletterRepository>();
            //services.AddScoped<IClienteRepository, ClienteRepository>();
            //services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            //services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            //services.AddScoped<IPacoteRepository, PacoteRepository>();
            //services.AddScoped<IImagemRepository, ImagemRepository>();
            //services.AddScoped<IMensagemMotivacionalRepository, MensagemMotivacionalRepository>();
            //services.AddScoped<IServicoRepository, ServicoRepository>();
            //services.AddScoped<IImagemServicoRepository, ImagemServicoRepository>();
            //services.AddScoped<IImagemTestemunhoRepository, ImagemTestemunhoRepository>();
            //services.AddScoped<ITestemunhoRepository, TestemunhoRepository>();
            //services.AddScoped<IServicoPacoteRepository, ServicoPacoteRepository>();
            //services.AddScoped<ILinkTreeRepository, LinkTreeRepository>();
            //services.AddScoped<IDocumentoRespository,DocumentoRepository>();
            //services.AddScoped<Sessao>();
            //services.AddScoped<LoginCliente>();
            //services.AddScoped<LoginColaborador>();
            //services.AddTransient<EnvioEmailAniversarioJob>();
            //services.AddTransient<TesteAgendadorJob>();
            return services;
        }
    }
}
