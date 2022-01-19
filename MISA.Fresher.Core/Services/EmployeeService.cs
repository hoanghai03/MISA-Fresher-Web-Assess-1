﻿using Microsoft.AspNetCore.Http;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Fresher.Core.Enum;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using MISA.Fresher.Core.MISAAttribute;

namespace MISA.Fresher.Core.Services
{
    /// <summary>
    /// employee service
    /// createdBy NHHAi 28/12/2021
    /// </summary>
    public class EmployeeService : BaseService<Employee>, IEmployeeService

    {

        IEmployeeRepository _employee;
        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeRepository employee) : base(employeeRepository)
        {
            _employee = employee;
        }

        public int DeleteAllService(List<string> employeeIds)
        {
            try
            {
                if (employeeIds.Count > 0)
                {
                    // thực hiện gọi đến repositoty
                    var res = _employee.DeleteAllRepository(employeeIds);
                    return res;
                }
                // nếu như dữ liệu trống thì sẽ hiện ra thông báo
                throw new HttpResponseException(Properties.Resources.NullId);
            }
            catch (HttpResponseException ex)
            {

                throw new HttpResponseException(ex.Value);
            }
        }

        public Stream ExportListUsingEPPlus(int pageSize, int pageNumber, string employeeFilter)
        {
            try
            {
                // Lấy danh sách nhân viên
                DataFilter employees = FilterService(pageSize, pageNumber, employeeFilter);
                if(employees.TotalRecord != 0)
                {
                    var stream = new MemoryStream();

                    using (ExcelPackage excel = new ExcelPackage(stream))
                    {

                        var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                        workSheet.TabColor = System.Drawing.Color.Black;
                        workSheet.DefaultRowHeight = (int)Enum.Excel.DefaultRowHeight;
                        //Header of table  
                        workSheet.Row(1).Height = (int)Enum.Excel.Height;
                        workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Row(1).Style.Font.Bold = true;
                        var properties = typeof(Employee).GetProperties();
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
                                workSheet.Cells[1, count].Value = propertyDisplay;
                                workSheet.Cells[1, count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[1, count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[1, count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[1, count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                count++;
                            }

                        }
                        int length = count;
                        //Body of table  
                        //  
                        int recordIndex = (int)Enum.Excel.Record_Index;
                        foreach (var employee in employees.data)
                        {
                            count = (int)Enum.Excel.Count;
                            foreach (var property in properties)
                            {
                                var propertyName = property.Name;
                                var exportExcel = property.GetCustomAttributes(typeof(ExportExcel), true);
                                var propertyDisplay = "";
                                if (exportExcel.Length > 0)
                                {
                                    workSheet.Cells[recordIndex, count].Value = property.GetValue(employee);
                                    if(property.PropertyType == typeof(DateTime?))
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
                return null;
            }
            catch (HttpResponseException ex)
            {

                throw new HttpResponseException(ex.Value);
            }
        }

        public DataFilter FilterService(int pageSize, int pageNumber, string employeeFilter)
        {
            try
            {
                // Xét trường hợp không nhập gì
                if (employeeFilter == null)
                {
                    employeeFilter = "";
                }
                // Xét TH pageSize và pageNumber <= 0
                if (pageSize <= 0 || pageNumber <= 0)
                {
                    throw new HttpResponseException(MISA.Fresher.Core.Properties.Resources.ParameterFilter);
                }
                return _employee.FilterRepository(pageSize, pageNumber, employeeFilter);
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

        public string GetNewEmployeeCode()
        {
            try
            {
                // gọi đến làm lấy mã code mới ở repository
                var employeeCode = _employee.GetMaxCodeRepository();
                employeeCode = employeeCode.Substring(3, employeeCode.Length - 3);
                //int number = Int32.Parse(employeeCode);
                Int32.TryParse(employeeCode, out int number);

                number += 1;
                var code = "NV-" + number.ToString().Trim();
                // trả về mã code mới
                return code;
            }
            catch (Exception ex)
            {

                throw new HttpResponseException(ex.Data);
            }
        }

        // TODO : đang làm
        public async Task<object> Import(IFormFile formFile)
        {
            try
            {
                // kiểm tra tệp nhập khẩu
                // 1. có đúng định dạng hay không
                // 2. kiểm tra dung lượng tệp có hợp lệ (VD : < 10MB)
                var length = formFile.ContentType.Length;
                if (length > 5000)
                {
                    throw new HttpResponseException("file có dung lượng quá lớn");
                }
                var employees = new List<Employee>();
                var employeesError = new List<Employee>();
                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var employeeCode = worksheet.Cells[row, 2].Value;
                            var fullName = worksheet.Cells[row, 3].Value;
                            var dateOfBirth = worksheet.Cells[row, 6].Value;
                            var email = worksheet.Cells[row, 7].Value;
                            var departmenId = worksheet.Cells[row, 8].Value;
                            Gender? gender = null;
                            var genderName = worksheet.Cells[row, 5].Value;
                            if (genderName != null)
                            {
                                switch (genderName)
                                {
                                    case "Nam":
                                        gender = Gender.Male;
                                        break;
                                    case "Nữ":
                                        gender = Gender.Female;
                                        break;
                                    case "Khác":
                                        gender = Gender.Other;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            var employee = new Employee
                            {
                                EmployeeCode = employeeCode == null ? null : employeeCode.ToString().Trim(),
                                FullName = fullName == null ? null : fullName.ToString().Trim(),
                                /*                                Gender = gender,*/
                                DateOfBirth = dateOfBirth == null ? null : DateTime.Parse(dateOfBirth.ToString().Trim()),
                                Email = email == null ? null : email.ToString().Trim(),
                                DepartmentId = departmenId == null ? Guid.Empty : Guid.Parse(departmenId.ToString().Trim()),
                            };
                            // Thực hiện validate dữ liệu
                            var isValid = DoValidate(employee, null);
                            if (isValid)
                                employees.Add(employee);
                            else
                                employeesError.Add(employee);
                        }
                    }
                }
                /*                if(employees.Count > 0)
                                {*/
                var res = _employee.Import(employees);
                return new
                {
                    DataImported = employees,
                    DataNotImported = employeesError,
                };
                /*                }
                                else return false;*/
            }
            catch (HttpResponseException ex)
            {

                throw new HttpResponseException(ex.Value);
            }
        }
    }
}
