using Infrastructure.Adapters.MediatR;
using Infrastructure.Adapters.MediatR.Impl;
using Infrastructure.Repositories.Transactions;
using Infrastructure.Repositories.Transactions.Impl;
using Infrastructure.Repositories.Users;
using Infrastructure.Repositories.Users.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UmHelpFinanceiro.Services.IdentityTokens;
using UmHelpFinanceiro.Services.IdentityTokens.Impl;
using UmHelpFinanceiro.Services.Transactions;
using UmHelpFinanceiro.Services.Transactions.Impl;
using UmHelpFinanceiro.Services.Users;
using UmHelpFinanceiro.Services.Users.Impl;

namespace UmHelpFinanceiro.Extensions
{
    public static class ConfigureServicesDependencyInjection
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration, byte[] key)
        {
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IIdentityTokenService, IdentityTokenService>(provider => new IdentityTokenService(key));
            services.AddScoped<IMediatRClient, MediatRClient>();
        }
    }
}