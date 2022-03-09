using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.MISAAttribute;
using System.Data;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Entities.Base;

namespace MISA.Fresher.Infrastructure.Repository
{
    /// <summary>
    /// base repository
    /// createdBy NHHAi 1/1/2021
    /// </summary>
    /// <typeparam name="T">entity</typeparam>
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region fields
        // Lấy thông tin database
        protected string _connectionString = string.Empty;
        protected string _className = typeof(T).Name;
        public IDbConnection dbConnection;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("connectDB");
        }
        #endregion

        #region Methods
        public virtual int Delete(Guid entityId)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                mySqlConnection.Open();
                //transaction
                var transaction = mySqlConnection.BeginTransaction();
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    // transaction
                    // thực hiện thêm dữ liệu vào param
                    parameters.Add($"@m_{_className}Id", entityId);
                    // thưc thi câu lệnh sql xóa dữ liệu
                    var rowEffects = mySqlConnection.Execute($"Proc_Del{_className}", param: parameters, transaction, commandType: CommandType.StoredProcedure);
                    if (rowEffects > 0)
                    {
                        // commit
                        transaction.Commit();
                    }
                    return rowEffects;


                }
                catch (HttpResponseException ex)
                {
                    transaction.Rollback();
                    throw new HttpResponseException(ex.Value);
                }
            }
        }

        public IEnumerable<T> Get()
        {
            using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
            {
                try
                {
                    // Gọi đến store để lấy dữ liệu
                    var entities = mySqlConnector.Query<T>($"Proc_Get{_className}s", commandType: CommandType.StoredProcedure);

                    // trả về dữ liệu cho client
                    return entities;

                }
                catch (HttpResponseException ex)
                {
                    throw new HttpResponseException(ex.Value);
                }
            }
        }

        public virtual T GetById(Guid entityId)
        {
            using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
            {

                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    // thêm giá trị entityId cho param
                    parameter.Add($"@{_className}Id", entityId);
                    // thực hiện query lấy dữ liệu
                    var entities = mySqlConnector.QueryFirstOrDefault<T>($"Proc_Get{_className}ById", param: parameter, commandType: CommandType.StoredProcedure);
                    // trả dữ liệu về cho client
                    return entities;

                }
                catch (HttpResponseException ex)
                {
                    throw new HttpResponseException(ex.Value);
                }
            }
        }

        public virtual int Insert(T entity)
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
                        // lấy toàn bộ proprety tương ứng với entity
                        var properties = typeof(T).GetProperties();
                        DynamicParameters parameters = new DynamicParameters();
                        // lặp các thuộc tính cần thêm dữ liệu trong T
                        foreach (var property in properties)
                        {
                            var propIgnore = property.GetCustomAttributes(typeof(Ignore), true);
                            if (propIgnore.Length > 0)
                            {
                                continue;
                            }
                            // lấy tên và value
                            var propName = property.Name;
                            var propValue = property.GetValue(entity);
                            // TH là id thì thêm mới id
                            if (propName == $"{_className}Id" && property.PropertyType == typeof(Guid))
                            {
                                parameters.Add($"@{propName}", Guid.NewGuid());
                                continue;
                            }
                            parameters.Add($"@{propName}", propValue);
                        }
                        // thực hiện thêm dữ liệu vào csdl
                        var rowEffects = mySqlConnector.Execute($"Proc_Insert{_className}", param: parameters, transaction, commandType: CommandType.StoredProcedure);
                        if (rowEffects > 0)
                        {
                            //commit
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                        // trả về kết quả khi thêm mơi thành công
                        return rowEffects;


                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                }

            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

        public virtual int Update(T entity, Guid entityId)
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
                        // lấy toàn bộ proprety tương ứng với entity
                        var properties = typeof(T).GetProperties();
                        DynamicParameters parameters = new DynamicParameters();

                        foreach (var property in properties)
                        {
                            var propIgnore = property.GetCustomAttributes(typeof(Ignore), true);
                            var propId = property.GetCustomAttributes(typeof(Id), true);
                            if (propIgnore.Length > 0 || propId.Length > 0)
                            {
                                continue;
                            }
                            // lấy tên và value
                            var propName = property.Name;
                            var propValue = property.GetValue(entity);
                            parameters.Add($"@{propName}", propValue);
                        }
                        // gán giá trị cho id
                        parameters.Add($"@{_className}Id", entityId);

                        // thực hiện thêm dữ liệu vào csdl
                        var rowEffects = mySqlConnector.Execute($"Proc_Update{_className}", param: parameters, transaction, commandType: CommandType.StoredProcedure);

                        if (rowEffects > 0)
                        {
                            //commit
                            transaction.Commit();
                        }
                        // trả về kết quả khi thêm mơi thành công
                        return rowEffects;
                    }
                    catch (HttpResponseException ex)
                    {
                        transaction.Rollback();
                        throw new HttpResponseException(ex.Value);
                    }
                }

            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

        public virtual int CheckCodeDuplicate(string entityCode)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_connectionString))
                {
                    DynamicParameters paramCheck = new DynamicParameters();
                    paramCheck.Add($"@{_className}Code", entityCode);
                    // thực hiện kiểm tra trùng mã
                    var customerCodeDuplicate = sqlConnection.QueryFirstOrDefault<int>($"Proc_CheckDuplicate{_className}Code", paramCheck, commandType: CommandType.StoredProcedure);
                    // trả về kết quả
                    return customerCodeDuplicate;

                }

            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

        public string GetCode(Guid? entityId)
        {
            try
            {
                using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
                {
                    DynamicParameters parameter = new DynamicParameters();
                    // thêm giá trị entityId cho param
                    parameter.Add($"@{_className}Id", entityId);
                    // thực hiện query lấy dữ liệu
                    var entities = mySqlConnector.QueryFirstOrDefault<string>($"Proc_Get{_className}CodeById", param: parameter, commandType: CommandType.StoredProcedure);
                    // trả dữ liệu về cho client
                    return entities;
                }
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }
        public DataFilter<T> FilterRepository(PaginationRequest paginationRequest)
        {
            try
            {
                using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
                {
                    // khai báo result và gán giá trị
                    DataFilter<T> result = new DataFilter<T>();
                    result.TotalPage = 0;
                    result.TotalRecord = 0;
                    result.data = null;
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add($"{_className}Filter", paginationRequest.FilterText, DbType.String);
                    // tính tổng số bản ghi
                    int totalPage = 0;
                    int totalRecord = mySqlConnector.QueryFirstOrDefault<int>($"Proc_TotalRecord{_className}", param: parameter, commandType: CommandType.StoredProcedure);
                    if (totalRecord > 0)
                    {
                        var offset = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
                        // lấy data từ trang thứ first page 
                        parameter.Add("@firstPage", offset, DbType.Int32);
                        parameter.Add("@pageSize", paginationRequest.PageSize, DbType.Int32);
                        // thực hiện lấy dữ liệu
                        var filterText = mySqlConnector.Query<T>($"Proc_Get{_className}Paging", param: parameter, commandType: CommandType.StoredProcedure);
                        //Phân trang
                        if (totalRecord % paginationRequest.PageSize > 0)
                        {
                            totalPage = (totalRecord / paginationRequest.PageSize) + 1;
                        }
                        else
                        {
                            totalPage = (totalRecord / paginationRequest.PageSize);
                        }
                        // gán giá trị cho result
                        result.TotalPage = totalPage;
                        result.TotalRecord = totalRecord;
                        result.data = filterText;
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
        public int DeleteAllRepository(List<string> entityIds)
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
                        //var ids = string.Join(",", employeeIds.Select(item => "'" + item + "'"));
                        //// add param
                        //parameters.Add("ids", ids);
                        //// thực hiện xóa
                        //var res = mySqlConnector.Execute($"Proc_DeleteEmployeeChecked", param: parameters, transaction, commandType: CommandType.StoredProcedure);
                        var count = 1;
                        var ids = "";
                        foreach (var id in entityIds)
                        {
                            //add pẩm
                            parameters.Add($"@{count}", id);
                            ids += $"@{count},";
                            count++;
                        }
                        //xóa dấu "," ở cuối chuỗi
                        ids = ids.Substring(0, ids.Length - 1);
                        // thực hiện xóa 
                        var res = mySqlConnector.Execute($"DELETE FROM {_className} WHERE {_className}ID IN ({ids}) ", param: parameters, transaction);

                        if (res == count - 1)
                        {
                            //commit
                            transaction.Commit();
                        }
                        else transaction.Rollback();
                        // trả về kết quả khi thêm mơi thành công
                        return res;
                    }
                    catch (HttpResponseException ex)
                    {
                        throw new HttpResponseException(ex.Value);
                    }
                }
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

        public string GetMaxCodeRepository()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
                {
                    // Lấy mã code lớn nhất trong hệ thống
                    var code = mySqlConnection.QueryFirst<string>($"Proc_GetMaxCode{_className}", commandType: CommandType.StoredProcedure);
                    // trả về mã code
                    return code;
                }

            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }
    }


    #endregion

}
