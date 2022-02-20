using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Enum
{
    /// <summary>
    /// Giới tính
    /// createdBY NHHAi 11/1/2022
    /// </summary>
    public enum Gender
    {
        // Nữ
        Female = 0,
        //Nam
        Male = 1,
        // Khác
        Other = 2
    }

    public enum AccountCategoryKind
    {
        // dư nợ
        Debt = 1,
        // dư có
        Credit = 2,
        // lưỡng tính
        Hermaphrodite = 3,
        // không có số dư
        NoBalance = 4
    }    
    public enum DetailByObjectKind
    {
        // nhà cung cấp
        Supplier = 1,
        // khách hàng
        Customer = 2,
        // nhân viên
        Employee = 3
    }
    public enum Number
    {
        Number_1 = 1, // dữ liệu trống,null,
        Number_2 = 2, // dữ liệu trùng
        Number_3 = 3 // dữ liệu sai
    }

    /// <summary>
    /// EXcel
    /// createdBY NHHAi 11/1/2022
    /// </summary>
    public enum Excel
    {
        //ĐẾm số cột excel cần export
        Count = 1,
        // chiều cao mặc định của hàng
        DefaultRowHeight = 12,
        // chiều cao
        Height = 20,
        // hàng bắt đầu của header
        /*        Row_Header = 1,*/
        //hàng bắt đầu của body
        Row_Body = 2,
        // hàng bắt đầu của bản ghi đầu tiên trong body
        Record_Index = 6,
        OK = 1,
        // hàng bắt đầu của header
        Header = 5,
        // font chữ
        Font_Size = 24
    }

    public enum Prefix
    {
        //Ông
        Grandfather = 1,
        //Bà
        Grandma = 2,
        //Bạn
        You = 3,
        //Chị
        Sister = 4,
        //Mr
        Mr = 5,
        //Mrs
        Mrs = 6,
        //Ms
        Ms = 7,
        //Anh
        Brother = 8,
        //Chú
        Uncle = 9,
    }

    public enum Code {
        BadRequest = 400,
        Created = 201,
        ServerError = 500
    }

    public enum Follow
    {
        // cảnh báo
        Alert = 1,
        // bắt buộc
        Obligate = 0
    }
}
