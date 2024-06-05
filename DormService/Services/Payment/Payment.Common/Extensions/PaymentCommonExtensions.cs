using Microsoft.Extensions.DependencyInjection;
using Payment.Common.Data;
using Payment.Common.DTOs;
using Payment.Common.Entities;
using Payment.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Common.Extensions
{
    public static class PaymentCommonExtensions
    {
        public static void AddPaymentCommonServices(this IServiceCollection services)
        {
            // Dependency injections
            services.AddScoped<IDebtsContext, DebtsContext>();
            services.AddScoped<IDebtsRepository, DebtsRepository>();

            // DTO Mapping
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<ReduceCreditDTO, StudentDebts>().ReverseMap();
            });
        }
    }
}
