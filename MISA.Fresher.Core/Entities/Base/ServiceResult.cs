using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Base
{
    /// <summary>
    /// Service Result
    /// createcBY NHHAi 25/2/2022
    /// </summary>
    public class ServiceResult
    {
        // Mã trả về
        public int Code { get; set; } = (int)System.Net.HttpStatusCode.OK;
        // Dữ liệu trả về
        public object Data { get; set; }
        // Thông tin lỗi
        public string ErrorMessage { get; set; }
        // Dữ liệu trả về thành công hay không (true- thành công,false- không thành công) 
        public bool Success { get; set; } = true;
        // Danh sách lỗi
        public List<ValidateField> ValidateInfo { get; set; }
        // ServiceTime
        public DateTime ServerTime { get; set; } = DateTime.Now;
        // SetError
        public void SetError(Exception ex)
        {
            Code = (int)System.Net.HttpStatusCode.InternalServerError;
            ErrorMessage = ex.Message;
            Success = false;
        }
    }
}
