using Core;

namespace Application
{
    public interface IUserService : IAsyncCrudAppService<Users>, IDependency
    {
    }
}
