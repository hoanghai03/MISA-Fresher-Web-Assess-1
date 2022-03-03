using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities.Payment;
using MISA.Fresher.Core.Entities.PaymentDetail;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IConfiguration configuration) : base(configuration) { }

        public int InserMasterDetailRepo(Payment payment, IEnumerable<PaymentDetail> paymentDetails)
        {

            using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
            {
                mySqlConnector.Open();
                // transaction
                var transaction = mySqlConnector.BeginTransaction();
                try
                {
                    // lấy toàn bộ proprety tương ứng với entity
                    var properties = typeof(Payment).GetProperties();
                    DynamicParameters parameters = new DynamicParameters();
                    DynamicParameters parametersDetail = new DynamicParameters();
                    Guid guid = Guid.NewGuid();
                    //============================================PAYMENT=========================================================
                    // lặp các thuộc tính cần thêm dữ liệu trong payment
                    foreach (var property in properties)
                    {
                        var propIgnore = property.GetCustomAttributes(typeof(Ignore), true);
                        if (propIgnore.Length > 0)
                        {
                            continue;
                        }
                        // lấy tên và value
                        var propName = property.Name;
                        var propValue = property.GetValue(payment);
                        // TH là id thì thêm mới id
                        if (propName == "PaymentId" && property.PropertyType == typeof(Guid))
                        {
                            parameters.Add("@PaymentId", guid);
                            continue;
                        }
                        parameters.Add($"@{propName}", propValue);
                    }

                    int rowEffects = mySqlConnector.Execute("Proc_InsertPayment", param: parameters, transaction, commandType: CommandType.StoredProcedure);
                    int countRowEffectsDetail = 0;
                    int count = 0;
                    if (rowEffects > 0)
                    {
                        
                        //============================================PAYMENT DETAIL=========================================================
                        foreach (PaymentDetail paymentDetail in paymentDetails)
                        {
                            // lấy toàn bộ proprety tương ứng với entity
                            var propertiesDetail = typeof(PaymentDetail).GetProperties();
                            // lặp các thuộc tính cần thêm dữ liệu trong payment
                            foreach (var property in propertiesDetail)
                            {
                                var propIgnore = property.GetCustomAttributes(typeof(Ignore), true);
                                if (propIgnore.Length > 0)
                                {
                                    continue;
                                }
                                // lấy tên và value
                                var propName = property.Name;
                                var propValue = property.GetValue(paymentDetail);
                                // TH là id thì thêm mới id
                                if (propName == "PaymentDetailId" && property.PropertyType == typeof(Guid))
                                {

                                    parametersDetail.Add($"@{propName}", Guid.NewGuid());
                                    continue;
                                }
                                // TH 
                                if (propName == "PaymentId" && property.PropertyType == typeof(Guid))
                                {

                                    parametersDetail.Add($"@{propName}", guid);
                                    continue;
                                }
                                parametersDetail.Add($"@{propName}", propValue);
                            }
                            int rowEffectsDetail = mySqlConnector.Execute("Proc_InsertPaymentDetail", param: parametersDetail, transaction, commandType: CommandType.StoredProcedure);
                            countRowEffectsDetail++;
                            count += rowEffects;

                        }

                    }

                    if (countRowEffectsDetail == count)
                    {
                        // thực hiện thêm dữ liệu vào csdl
                        //commit
                        transaction.Commit();
                    }


                    // trả về kết quả khi thêm mơi thành công
                    return rowEffects;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

        }
    }
}
