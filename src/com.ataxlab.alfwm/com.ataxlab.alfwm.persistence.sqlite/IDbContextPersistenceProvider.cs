using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.persistence.dbcontext
{
    /// <summary>
    /// provides a mechanism for injecting your own Entity Framework types
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TDbOptions"></typeparam>
    public interface IDbContextPersistenceProvider<TDbContext, TDbOptions> 
        where TDbContext : class  // DbContext new()
        where TDbOptions : class // new() // DbContextOptions<TDbContext>
    {
        TDbContext DbContext { get; set; }

        TDbOptions DbContextOptions { get; set; }
    }
}
