using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Entities.Base;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using MISA.Fresher.Core.MISAAttribute;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public ServiceResult DeleteService(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (entityId != null)
            {
                // goi đến hàm delete của repository
                serviceResult.Data = _baseRepository.Delete(entityId);
            }
            else
            {
                serviceResult.ErrorMessage = "Thiếu entityId";
                serviceResult.Success = false;
            }
            return serviceResult;
        }

        public ServiceResult GetByIdService(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (entityId != null)
            {
                // goi đến hàm lấy nhân viên theo id của repository
                serviceResult.Data = _baseRepository.GetById(entityId);
            }
            else
            {
                serviceResult.ErrorMessage = "Thiếu entityId";
                serviceResult.Success = false;
            }
            return serviceResult;

        }

        public ServiceResult GetService()
        {
            return new ServiceResult() { Data = _baseRepository.Get() };

        }

        public ServiceResult InsertService(T entity)
        {
            // Sửa lại dovalidate

            ServiceResult serviceResult = new ServiceResult();
            // validate dữ liệu chung
            ValidateResult validateResult = DoValidate(entity, null);

            if (!validateResult.IsValid)
            {
                serviceResult.Code = (int)Enum.Code.BadRequest;
                serviceResult.Success = false;
                serviceResult.ValidateInfo = validateResult.ValidateInfo;
            }
            else
            {
                // validate dữ liệu riêng
                if (validateResult.IsValid)
                {
                    validateResult.IsValid = ValidateCustom(entity);
                }
                // gọi đến repository
                if (validateResult.IsValid)
                {
                    serviceResult.Data = _baseRepository.Insert(entity);
                }
            }
            return serviceResult;
        }

        public ServiceResult UpdateService(T entity, Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            ValidateResult validateResult = new ValidateResult();


            // validate dữ liệu chung
            validateResult = DoValidate(entity, entityId);
            if (!validateResult.IsValid)
            {
                serviceResult.Code = (int) Enum.Code.BadRequest;
                serviceResult.Success = false;
                serviceResult.ValidateInfo = validateResult.ValidateInfo;
            }
            else
            {
                // validate dữ liệu riêng
                if (validateResult.IsValid)
                {
                    validateResult.IsValid = ValidateCustom(entity);
                }
                // gọi đến repository
                if (validateResult.IsValid)
                {
                    serviceResult.Data = _baseRepository.Update(entity, entityId);
                }
            }

            return serviceResult;

        }

        protected ValidateResult DoValidate(T entity, Guid? entityId)
        {
            ValidateResult result = new ValidateResult();
            List<string> errorMsgs = new List<string>();
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
                        //errorMsgs.Add(string.Format(Properties.Resources.NullValue, propertyDisplay));
                        result.IsValid = false;
                        result.ValidateInfo.Add(new ValidateField() { FieldName = propertyDisplay, ErrorCode = (int)Enum.Number.Number_1, ErrorMessage = Properties.Resources.NullValue });
                    }
                    // Nếu không gửi mã id phòng ban thì nó sẽ thực hiện validate ở đây
                    var checkValueGuid = new Guid();
                    if (property.PropertyType == typeof(Guid) && propertyValue.Equals(checkValueGuid))
                    {
                        //errorMsgs.Add(string.Format(Properties.Resources.NullValue, propertyDisplay));
                        result.IsValid = false;
                        result.ValidateInfo.Add(new ValidateField() { FieldName = propertyDisplay, ErrorCode = (int)Enum.Number.Number_1, ErrorMessage = Properties.Resources.NullValue });
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
                        if (String.Compare(propertyValue.ToString().Trim(), checkEntity, true) == 0)
                        {
                            // nếu khác nhau thì gán giá trị cho checkDuplicate
                            checkDuplicate = Int32.Parse(Properties.Resources.checkDuplicate);
                        }
                    }
                    if (checkDuplicate != 0)
                    {
                        //errorMsgs.Add(string.Format(Properties.Resources.DuplicateCode, propertyDisplay));
                        result.IsValid = false;
                        result.ValidateInfo.Add(new ValidateField() { FieldName = propertyDisplay, ErrorCode = (int)Enum.Number.Number_2, ErrorMessage = Properties.Resources.DuplicateCode });
                    }
                }

                // TH có checkdate
                if (propCheckDate.Length > 0 && propertyValue != null)
                {
                    var date = DateTime.Parse(propertyValue.ToString());
                    var today = DateTime.Now;
                    if (DateTime.Compare(date, today) > 0)
                    {
                        //errorMsgs.Add(string.Format(Properties.Resources.DateMessage, propertyDisplay));
                        result.IsValid = false;
                        result.ValidateInfo.Add(new ValidateField() { FieldName = propertyDisplay, ErrorCode = (int)Enum.Number.Number_3, ErrorMessage = Properties.Resources.DateMessage });
                    }

                }

                // Th nhập mã code sai định dạng
                if (propCheckCode.Length > 0 && propertyValue != null)
                {
                    Regex regex = new Regex(@"^NV-[0-9]+$");
                    if (!regex.IsMatch(propertyValue.ToString()))
                    {
                        //errorMsgs.Add(string.Format(Properties.Resources.FormatCode, propertyDisplay));
                        result.IsValid = false;
                        result.ValidateInfo.Add(new ValidateField() { FieldName = propertyDisplay, ErrorCode = (int)Enum.Number.Number_3, ErrorMessage = Properties.Resources.FormatCode });
                    }

                }
            }
            return result;
        }

        protected virtual bool ValidateCustom(T entity)
        {
            return true;
        }

        public ServiceResult FilterService(PaginationRequest paginationRequest)
        {
            ServiceResult serviceResult = new ServiceResult();

            if (paginationRequest == null)
            {
                throw new ArgumentNullException("paginationRequest null");
            }

            // Xét trường hợp không nhập gì
            if (paginationRequest.FilterText == null)
            {
                paginationRequest.FilterText = "";
            }
            // Xét TH pageSize và pageNumber <= 0
            if (paginationRequest.PageSize <= 0 || paginationRequest.PageNumber <= 0)
            {
                throw new HttpResponseException(MISA.Fresher.Core.Properties.Resources.ParameterFilter);
            }

            serviceResult.Data = _baseRepository.FilterRepository(paginationRequest);
            return serviceResult;
        }

        public ServiceResult DeleteAllService(List<string> entityIds)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (entityIds.Count > 0)
            {
                // thực hiện gọi đến repositoty
                serviceResult.Data = _baseRepository.DeleteAllRepository(entityIds);
                if ((int)serviceResult.Data == 0)
                {
                    // nếu như dữ liệu trống thì sẽ hiện ra thông báo
                    /*throw new HttpResponseException(Properties.Resources.NullId);*/
                    serviceResult.Success = false;
                    serviceResult.ErrorMessage = Properties.Resources.NullId;
                }
            }

            return serviceResult;
        }

        public Stream ExportListUsingEPPlus()
        {
            try
            {
                // Lấy danh sách nhân viên
                var entities = _baseRepository.Get();

                var stream = new MemoryStream();

                using (ExcelPackage excel = new ExcelPackage(stream))
                {

                    var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                    workSheet.TabColor = System.Drawing.Color.Black;
                    workSheet.DefaultRowHeight = (int)Enum.Excel.DefaultRowHeight;
                    // title danh sách nhân viên
                    workSheet.Cells["E2"].Value = Properties.Resources.ListEmployee;
                    workSheet.Cells["E2"].Style.Font.Name = "B Zar";
                    workSheet.Cells["E2"].Style.Font.Size = (int)Enum.Excel.Font_Size;
                    workSheet.Cells["E2"].Style.Font.Bold = true;
                    workSheet.Cells["E2:H2"].Merge = true;
                    //Header of table  
                    workSheet.Row((int)Enum.Excel.Header).Height = (int)Enum.Excel.Height;
                    workSheet.Row((int)Enum.Excel.Header).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row((int)Enum.Excel.Header).Style.Font.Bold = true;
                    var properties = typeof(T).GetProperties();
                    int count = (int)Enum.Excel.Count;
                    foreach (var property in properties)
                    {
                        var propName = property.GetCustomAttributes(typeof(PropertyName), true);
                        var exportExcel = property.GetCustomAttributes(typeof(ExportExcel), true);
                        var propertyDisplay = "";
                        // lấy tên PropertyName
                        if (propName.Length > 0)
                        {
                            propertyDisplay = (propName[0] as PropertyName).name;
                        }
                        if (exportExcel.Length > 0)
                        {
                            workSheet.Cells[(int)Enum.Excel.Header, count].Value = propertyDisplay;
                            workSheet.Cells[(int)Enum.Excel.Header, count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[(int)Enum.Excel.Header, count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[(int)Enum.Excel.Header, count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[(int)Enum.Excel.Header, count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            count++;
                        }

                    }
                    int length = count;
                    //Body of table  
                    //  
                    int recordIndex = (int)Enum.Excel.Record_Index;
                    foreach (var entity in entities)
                    {
                        count = (int)Enum.Excel.Count;
                        foreach (var property in properties)
                        {
                            var propertyName = property.Name;
                            var exportExcel = property.GetCustomAttributes(typeof(ExportExcel), true);
                            var propertyDisplay = "";
                            if (exportExcel.Length > 0)
                            {
                                workSheet.Cells[recordIndex, count].Value = property.GetValue(entity);
                                if (property.PropertyType == typeof(DateTime?))
                                {
                                    workSheet.Cells[recordIndex, count].Style.Numberformat.Format = "dd/mm/yyyy";
                                    // căn giữa
                                    workSheet.Cells[recordIndex, count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                }
                                workSheet.Cells[recordIndex, count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[recordIndex, count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[recordIndex, count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[recordIndex, count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                count++;
                            }


                        }
                        recordIndex++;
                    }
                    for (int i = 1; i < length; i++)
                    {
                        // format chiều ngang của cột
                        workSheet.Column(i).AutoFit();
                    }
                    String folder = System.Guid.NewGuid().ToString();
                    // Lưu file excel
                    //excel.SaveAs(new FileInfo($@"D:\{folder}.xlsx"));
                    excel.Save();
                    stream.Position = 0;
                    return excel.Stream;
                }

            }
            catch (HttpResponseException ex)
            {

                throw new HttpResponseException(ex.Value);
            }
        }
    }
}
