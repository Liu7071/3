using Core;
using EFWork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UserService : AsyncCrudAppService<Users>, IUserService
    {      
        public UserService(IRepository<Users> _repository) : base(_repository)
        {
           
        }

    }
}
