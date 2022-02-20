using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Base
{
    public class ServiceResult
    {
        public int Code { get; set; } = (int)System.Net.HttpStatusCode.OK;

        public object Data { get; set; }

        public string ErrorMessage { get; set; }

        public bool Success { get; set; } = true;

        public List<ValidateField> ValidateInfo { get; set; }

        public DateTime ServerTime { get; set; } = DateTime.Now;

        public void SetError(Exception ex)
        {
            Code = (int)System.Net.HttpStatusCode.InternalServerError;
            ErrorMessage = ex.Message;
            Success = false;
        }
    }
}
