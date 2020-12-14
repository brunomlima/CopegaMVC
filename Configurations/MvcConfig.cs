using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CopegeMVC.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(options => {
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => $"O valor '{x}' não é válido para '{y}'.");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => $"Não foi fornecido um valor para o campo {x}.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Campo obrigatório.");
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => $"O valor '{x}' não é válido.");
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor fornecido é inválido.");
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser um número.");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => $"O valor fornecido é inválido para '{x}'.");
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => $"O valor fornecido é inválido para '{x}'.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => $"O campo '{x}' deve ser um número.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "O valor nulo é inválido.");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            return services;
        }
    }
}
