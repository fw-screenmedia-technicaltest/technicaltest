using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScreenMediaTT.Core.Interfaces;
using ScreenMediaTT.Core.Services;
using ScreenMediaTT.Data;
using ScreenMediaTT.Data.Interfaces;
using ScreenMediaTT.Data.Repositories;

namespace ScreenMediaTT.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ScreenMediaTtContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<IRoomAvailabilityRepository, RoomAvailabilityRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();

            services.AddTransient<IAvailabilityService, AvailabilityService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IUtilityService, UtilityService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ScreenMediaTtContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
