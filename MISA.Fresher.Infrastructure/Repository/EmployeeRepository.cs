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
using System.Data;
using MISA.Fresher.Core.MISAAttribute;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Properties;

namespace MISA.Fresher.Infrastructure.Repository
{
    /// <summary>
    /// employee repository
    /// createdBy NHHai 1/1/2022
    /// </summary>
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override Employee GetById(Guid entityId)
        {
            try
            {
                using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
                {
                    DynamicParameters parameter = new DynamicParameters();
                    // thêm giá trị entityId cho param
                    parameter.Add($"@EmployeeId", entityId);
                    // thực hiện query lấy dữ liệu
                    var entity = mySqlConnector.QueryFirstOrDefault<Employee>($"Proc_GetEmployeeById", param: parameter, commandType: CommandType.StoredProcedure);

                    // trả dữ liệu về cho client
                    return entity;
                }

            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }



        public object Import(List<Employee> employees)
        {
            try
            {
                int count = 0;
                foreach (var employee in employees)
                {
                    count += Insert(employee);

                }
                return count;
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }
    }
}
