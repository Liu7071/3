using EFWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyFramework
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
            
           
            //�� Startup.ConfigureServices �����У��������кͷ����������֤�м������ AddAuthentication AddCookie 
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                //û�е�½ʱ�����Ὣ�����ض���������·��
                options.LoginPath = new PathString("/Account/Login");
                //��¼��cookie������
                options.Cookie.Name = "My_Cookie";
                //û�з���Ȩ��ʱ�����Ὣ�����ض���������·��
                options.AccessDeniedPath = new PathString("/Home/Error");
            });
            //ȫ�ּ��������֤
            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter())).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddRazorRuntimeCompilation();

            //AddRazorRuntimeCompilation  ���޸�ǰ��ʱ��ˢ��ҳ��Ϳ��Կ���Ч��
            //services.AddDbContext<WebDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            var connectionString = Configuration.GetConnectionString("Default");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<WebDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            //������ݿ��쳣ɸѡ�� ��Ҫ����Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore����������
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
            //��������ע��
            IocManager.Register(services);
            //services.AddScoped<IRepository<Users>, Repository<Users>>();
            //services.AddScoped<IAsyncCrudAppService<Users>, AsyncCrudAppService<Users>>();
            //services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //�����֤ע��
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
