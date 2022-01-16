using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using MISA.Fresher.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        // danh sách lỗi
        protected List<string> errorMsgs = new List<string>();
        IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public int DeleteService(Guid entityId)
        {
            try
            {
                if (entityId != null)
                {
                    // goi đến hàm delete của repository
                    return _baseRepository.Delete(entityId);
                }
                throw new HttpResponseException(MISA.Fresher.Core.Properties.Resources.ValidateId);
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }

        }

        public T GetByIdService(Guid entityId)
        {
            try
            {
                if (entityId != null)
                {
                    // goi đến hàm lấy nhân viên theo id của repository
                    return _baseRepository.GetById(entityId);
                }
                throw new HttpResponseException(MISA.Fresher.Core.Properties.Resources.ValidateId);
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

        public IEnumerable<T> GetService()
        {
            try
            {
                // gửi yêu cầu đên repository
                var res = _baseRepository.Get();
                return res;

            }
            catch (Exception ex)
            {

                throw new HttpResponseException(ex.Data);
            }
        }

        public int? InsertService(T entity)
        {
            try
            {
                // validate dữ liệu chung
                bool isValid = ValidateObject(entity, null);
                // gọi đến repository
                if (isValid)
                {
                    return _baseRepository.Insert(entity);
                }
                return null;

            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

        public int? UpdateService(T entity, Guid entityId)
        {
            try
            {
                // validate dữ liệu chung
                bool isValid = ValidateObject(entity, entityId);
                // gọi đến repository
                if (isValid)
                {
                    return _baseRepository.Update(entity, entityId);
                }
                return null;
            }
            catch (HttpResponseException ex)
            {

                throw new HttpResponseException(ex.Value);
            }
        }

        public bool ValidateObject(T entity, Guid? entityId)
        {
            try
            {
                var isValid = DoValidate(entity, entityId);
                if (errorMsgs.Count > 0)
                {
                    isValid = false;
                    throw new HttpResponseException(errorMsgs);
                }
                return isValid;
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

        protected bool DoValidate(T entity, Guid? entityId)
        {
            List<string> ErrorMsgs = new List<string>();
            var isValid = true;
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                // Lấy các thông tin thuộc tính
                var propertyName = property.Name;
                var propertyDisplay = propertyName;
                var propertyValue = property.GetValue(entity);
                var notEmpty = property.GetCustomAttributes(typeof(NotEmpty), true);
                var propName = property.GetCustomAttributes(typeof(PropertyName), true);
                var propDuplicate = property.GetCustomAttributes(typeof(NotDuplicate), true);
                var propCheckDate = property.GetCustomAttributes(typeof(CheckDate), true);
                var propCheckCode = property.GetCustomAttributes(typeof(CheckInsertCode), true);

                // lấy tên PropertyName
                if (propName.Length > 0)
                {
                    propertyDisplay = (propName[0] as PropertyName).name;
                }
                // nếu như thuộc tính hiện tại có [NotEmpty]
                if (notEmpty.Length > 0)
                {
                    // Nếu value là null hoặc empty
                    if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString().Trim()))
                    {
                        errorMsgs.Add(string.Format(Properties.Resources.NullValue, propertyDisplay));
                    }
                    // Nếu không gửi mã id phòng ban thì nó sẽ thực hiện validate ở đây
                    var checkValueGuid = new Guid();
                    if (property.PropertyType == typeof(Guid) && propertyValue.Equals(checkValueGuid))
                    {
                        errorMsgs.Add(string.Format(Properties.Resources.NullValue, propertyDisplay));
                    }
                }
                // TH có [NotDuplicate]
                if (propDuplicate.Length > 0 && propertyValue != null)
                {
                    int checkDuplicate = _baseRepository.CheckCodeDuplicate(propertyValue.ToString().Trim());
                    // xét trường hợp id not null
                    if (entityId != null)
                    {
                        // lấy mã nhân viên tương ứng với id
                        string checkEntity = _baseRepository.GetCode(entityId);
                        // so sánh
                        if (String.Compare(propertyValue.ToString().Trim(), checkEntity, true) != 0)
                        {
                            // nếu khác nhau thì gán giá trị cho checkDuplicate
                            checkDuplicate = Int32.Parse(Properties.Resources.checkDuplicate);
                        }
                    }
                    if (((checkDuplicate != 0 && entityId == null) || (checkDuplicate > 1 && entityId != null)))
                    {
                        errorMsgs.Add(string.Format(Properties.Resources.DuplicateCode, propertyDisplay));
                    }
                }

                // TH có checkdate
                if (propCheckDate.Length > 0 && propertyValue != null)
                {
                    var date = DateTime.Parse(propertyValue.ToString());
                    var today = DateTime.Now;
                    if (DateTime.Compare(date, today) > 0)
                    {
                        errorMsgs.Add(string.Format(Properties.Resources.DateMessage, propertyDisplay));
                    }

                }

                // Th nhập mã code sai định dạng
                if (propCheckCode.Length > 0 && propertyValue != null)
                {
                    Regex regex = new Regex(@"^NV-[0-9]+$");
                    if (!regex.IsMatch(propertyValue.ToString()))
                    {
                        errorMsgs.Add(string.Format(Properties.Resources.FormatCode, propertyDisplay));
                    }

                }
            }
            isValid = ValidateCustom(entity);
            if (errorMsgs.Count() > 0)
            {
                var propError = entity.GetType().GetProperty("ErrorMsgs");
                if(propError != null)
                {
                    propError.SetValue(entity, errorMsgs);
                }
                /*errorMsgs = ErrorMsgs;*/
                isValid = false;
            }
            return isValid;
        }
         
        protected virtual bool ValidateCustom(T entity)
        {
            return true;
        }
    }
}
