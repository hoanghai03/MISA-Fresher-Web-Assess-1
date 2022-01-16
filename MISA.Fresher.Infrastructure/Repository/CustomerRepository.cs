using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;


namespace MISA.Fresher.Infrastructure.Repository
{
    /// <summary>
    /// CustomerRepository
    /// createdBy NHHAi 20/12/2021
    /// </summary>
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }

}

