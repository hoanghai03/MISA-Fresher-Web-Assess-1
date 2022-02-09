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
}
