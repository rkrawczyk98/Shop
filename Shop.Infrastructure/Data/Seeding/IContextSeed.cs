using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data.Seeding
{
    public interface IContextSeed
    {
        Task SeedAsync();
    }
}
