using Application;
using Core;
using EFWork.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFramework
{
    /// <summary>
    /// 先创建一个抽象类
    /// </summary>
    public abstract class IocManager
    {
        public static void Register(IServiceCollection services)
        {
            Type type = typeof(IDependency);//获取继承IDependency的类型
            //从程序集中找到包含IDependency所有相关
            var Types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(type) || t.GetInterfaces().Contains(type)));
            //得到类的集合
            var classList = Types.Where(o => o.IsClass).ToList();
            //等到接口的集合
            var InterfaceList = Types.Where(o => o.IsInterface).ToList();
            foreach (var item in classList)
            {
                var firstInterface = InterfaceList.FirstOrDefault(o => o.IsAssignableFrom(item));//class有接口，注入接口和类
                if (firstInterface != null)
                {
                    services.AddScoped(firstInterface, item);
                }
                else
                {
                    //注入class
                    services.AddScoped(item);
                }
            }
            //派生类注入
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAsyncCrudAppService<>), typeof(AsyncCrudAppService<>));

        }
    }
}
