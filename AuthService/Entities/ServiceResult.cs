using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class ServiceResult
    {
        public int Code { get; set; } = 200;
        public bool Success
        {
            get
            {
                switch (Code)
                {
                    case 200:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public object Data { get; set; }

        // Thông tin lỗi
        public string ErrorMessage { get; set; }

        // Danh sách lỗi
        public List<ValidateField> ValidateInfo { get; set; }

        public DateTime ServerTime { get; set; } = DateTime.Now;

        public void SetError(Exception ex)
        {
            Code = (int)System.Net.HttpStatusCode.InternalServerError;
            ErrorMessage = ex.Message;
        }



    }
}
