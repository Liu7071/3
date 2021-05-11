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
            
           
            //在 Startup.ConfigureServices 方法中，创建具有和方法的身份验证中间件服务 AddAuthentication AddCookie 
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                //没有登陆时，将会将请求重定向到这个相对路径
                options.LoginPath = new PathString("/Account/Login");
                //登录后cookie的名称
                options.Cookie.Name = "My_Cookie";
                //没有访问权限时，将会将请求重定向到这个相对路径
                options.AccessDeniedPath = new PathString("/Home/Error");
            });
            //全局加入身份验证
            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter())).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddRazorRuntimeCompilation();

            //AddRazorRuntimeCompilation  在修改前端时，刷新页面就可以看到效果
            //services.AddDbContext<WebDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            var connectionString = Configuration.GetConnectionString("Default");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<WebDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            //添加数据库异常筛选器 需要下载Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore，并且引用
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
            //批量依赖注入
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
            //身份验证注册
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
