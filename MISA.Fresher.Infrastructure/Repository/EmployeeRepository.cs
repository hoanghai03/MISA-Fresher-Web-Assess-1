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

        public int DeleteAllRepository(List<string> employeeIds)
        {
            try
            {
                using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
                {
                    mySqlConnector.Open();
                    // transaction
                    var transaction = mySqlConnector.BeginTransaction();
                    try
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        // điền dấu '' cho mỗi phần tử id
                        var ids = string.Join(",", employeeIds.Select(item => "'" + item + "'"));
                        // add param
                        parameters.Add("ids", ids);

                        // thực hiện xóa
                        var res = mySqlConnector.Execute($"Proc_DeleteEmployeeChecked",param: parameters,transaction,commandType: CommandType.StoredProcedure);
                        //commit
                        transaction.Commit();
                        return res;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataFilter FilterRepository(int pageSize, int pageNumber, string employeeFilter)
        {
            try
            {
                using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
                {
                    // khai báo result và gán giá trị
                    DataFilter result = new DataFilter();
                    result.TotalPage = 0;
                    result.TotalRecord = 0;
                    result.data = null;
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add($"EmployeeFilter", employeeFilter, DbType.String);
                    // tính tổng số bản ghi
                    int totalPage = 0;
                    int totalRecord = mySqlConnector.QueryFirstOrDefault<int>($"Proc_TotalRecord", param: parameter, commandType: CommandType.StoredProcedure);
                    if (totalRecord > 0)
                    {
                        var firstPage = (pageNumber - 1) * pageSize;
                        // lấy data từ trang thứ first page 
                        parameter.Add("@firstPage", firstPage, DbType.Int32);
                        parameter.Add("@pageSize", pageSize, DbType.Int32);
                        // thực hiện lấy dữ liệu
                        var filter = mySqlConnector.Query<Employee>($"Proc_GetEmployeePaging", param: parameter, commandType: CommandType.StoredProcedure);
                        //Phân trang
                        if (totalRecord % pageSize > 0)
                        {
                            totalPage = (totalRecord / pageSize) + 1;
                        }
                        else
                        {
                            totalPage = (totalRecord / pageSize);
                        }
                        // gán giá trị cho result
                        result.TotalPage = totalPage;
                        result.TotalRecord = totalRecord;
                        result.data = filter;
                    }
                    // trả về kết quả
                    return result;
                }

            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
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
            catch (Exception ex)
            {
                throw new HttpResponseException(ex.Data);
            }
        }

        public string GetMaxCodeRepository()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
                {
                    // Lấy mã code lớn nhất trong hệ thống
                    var employeeCode = mySqlConnection.QueryFirstOrDefault($"Proc_GetMaxCode", commandType: CommandType.StoredProcedure);
                    // trả về mã code
                    return employeeCode.EmployeeCode;
                }

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(ex.Data);
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
