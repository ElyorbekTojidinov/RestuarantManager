using Aplication.Interfaces.InterfacesProducts;
using Domain.ModelsJWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.InterfacesJWT
{
    public interface IUserRepository : IRepositoryJwt<Users>
    {
        public Task<string> ComputeHashAsync(string input);
    }
}
