using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities.PaymentDetail;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    public class PaymentDetailRepository : BaseRepository<PaymentDetail>,IPaymentDetailRepository
    {
        public PaymentDetailRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<PaymentDetail> GetWithPaymentId(Guid paymentId)
        {
            using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@PaymentId", paymentId);
                    // Gọi đến store để lấy dữ liệu
                    var entities = mySqlConnector.Query<PaymentDetail>($"Proc_GetPaymentDetailByPaymentId",param:parameters, commandType: CommandType.StoredProcedure);

                    // trả về dữ liệu cho client
                    return entities;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
