using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Services
{
    /// <summary>
    /// SupplierService
    /// createdBy NHHai 20/2/2022
    /// </summary>
    public class SupplierService : BaseService<Supplier>,ISupplierService
    {
        IBaseRepository<Supplier> _base;
        public SupplierService(ISupplierRepository supplierRepository) : base(supplierRepository)
        {
            _base = supplierRepository;
        }

        public string GetNewSupplierCode()
        {
            try
            {
                // gọi đến làm lấy mã code mới ở repository
                var code = _base.GetMaxCodeRepository();
                code = code.Substring(3, code.Length - 3);
                //int number = Int32.Parse(employeeCode);
                Int32.TryParse(code, out int number);

                number += 1;
                var supplierCode = "NCC" + number.ToString().Trim();
                // trả về mã code mới
                return supplierCode;
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }
    }
}
