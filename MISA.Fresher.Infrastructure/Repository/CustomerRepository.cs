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
    public class CustomerRepository : ICustomerRepository
    {
        #region feilds
        // Lấy thông tin database
        protected string _connectionString = string.Empty;
        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("connectDB");
        }
        #endregion

        #region methods
        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            // 1.Khởi tạo kết nối database
            using (MySqlConnection sqlConnector = new MySqlConnection(_connectionString))
            {

                // 2.Thực hiện lấy dữ liệu trong database
                string sql = $"SELECT * FROM Customer";
                var entities = sqlConnector.Query<Customer>(sql);
                // 3. Trả về dữ liệu cho client
                return entities;
            }
        }

        public IEnumerable<Customer> GetById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public int Insert(Customer entity)
        {
            // 1. Khởi tạo kết nối database
            using (MySqlConnection sqlConnection = new MySqlConnection(_connectionString))
            {

                // 2. Thực hiện lưu dữ liệu trong database
                var columns = "";
                var columnParams = "";

                // map các thuộc tính property và value 
                var props = typeof(Customer).GetProperties();
                DynamicParameters dynamicParameters = new DynamicParameters();
                foreach (var prop in props)
                {
                    // TH gán giá trị cho customerID 
                    if (prop.Name == $"CustomerId" && prop.PropertyType == typeof(Guid))
                    {
                        entity.CustomerId = Guid.NewGuid();
                    }
                    var propName = prop.Name;
                    // lấy tên column
                    columns += $"{propName},";
                    // Lấy tên columnParam
                    columnParams += $"@{propName},";
                    // Map giá trị của column param với giá trị mình đem vào tương ứng
                    dynamicParameters.Add($"@{propName}", prop.GetValue(entity));
                }

                // xóa dấu "," ở cuối mỗi chuỗi
                columns = columns.Substring(0, columns.Length - 1);
                columnParams = columnParams.Substring(0, columnParams.Length - 1);
                var sql = $"INSERT INTO Customer({columns}) VALUES ({columnParams})";
                // thực hiện execute dữ liệu
                var rowEffects = sqlConnection.Execute(sql, param: dynamicParameters);
                // 4. trả kết quả
                if (rowEffects > 0)
                {
                    // Trường hợp thêm vào dữ liệu
                    return rowEffects;
                }
                else
                {
                    // trường hợp không thêm được dữ liệu
                    return rowEffects;
                }
            }
        }
        public bool CheckCustomerCodeDuplicate(string customerCode)
        {
            // Khởi tạo kết nối với database:
            MySqlConnection sqlConnection = new MySqlConnection(_connectionString);

            var sqlCheck = "SELECT CustomerCode FROM Customer WHERE CustomerCode = @CustomerCode";
            DynamicParameters paramCheck = new DynamicParameters();
            paramCheck.Add("@CustomerCode", customerCode);
            var customerCodeDuplicate = sqlConnection.QueryFirstOrDefault<object>(sqlCheck, paramCheck);

            if (customerCodeDuplicate != null)
            {
                return true;
            }
            return false;
        }
        public int Update(Customer entity, Guid entityId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}

