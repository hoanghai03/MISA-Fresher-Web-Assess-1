using MISA.Fresher.Core.Enum;
using MISA.Fresher.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// createdBy NHHai 28/12/2021
    /// </summary>
    public class Employee : BaseEntity
    {
        /// <summary>
        /// mã id
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        [Id]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// mã nhân viên
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        [NotEmpty]
        [PropertyName("Mã nhân viên")]
        [NotDuplicate]
        [CheckInsertCode]
        [ExportExcel]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// tên
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// tên cuối
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// họ và tên
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        [ExportExcel]
        [NotEmpty]
        [PropertyName("Họ và tên")]
        public string FullName { get; set; }

        /// <summary>
        /// địa chỉ
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// email
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// số cmnd
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        [ExportExcel]
        [PropertyName("Số CMND")]
        public string IdentifyNumber { get; set; }


        /// <summary>
        /// Ngày sinh
        /// createdBy NHHai 28/12/2021
        /// </summary>
        [CheckDate]
        [PropertyName("Ngày sinh")]
        [ExportExcel]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// ngày tạo
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        [CheckDate]
        [PropertyName("Ngày cấp cmnd")]
        public DateTime? IdentifyDate { get; set; }

        /// <summary>
        /// nơi cấp cmnd
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string IdentifyPlace { get; set; }

        /// <summary>
        /// ngày tham gia
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        [CheckDate]
        [PropertyName("Ngày tham gia")]
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// tình trạng học vấn
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public int? MartialStatus { get; set; }

        /// <summary>
        /// tình trạng học vấn
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public int? EducationalBackground { get; set; }

        /// <summary>
        /// QualificationId
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        /*public Guid? QualificationId { get; set; }*/

        /// <summary>
        /// Mã phòng ban
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        [NotEmpty]
        [PropertyName("Phòng ban")]
        public Guid DepartmentId { get; set; }

        [Ignore]
        [ExportExcel]
        [PropertyName("Tên đơn vị")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// mã vị trí / chức vụ
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        /*public Guid? PositionId { get; set; }*/

        /// <summary>
        /// trạng thái
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public int? WorkStatus { get; set; }

        /// <summary>
        /// thuế cá nhân
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string PersonalTaxCode { get; set; }

        /// <summary>
        /// mức lương
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public double? Salary { get; set; }

        /// <summary>
        /// giới tính
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public Gender? Gender { get; set; }

        [Ignore]
        [ExportExcel]
        [PropertyName("Giới tính")]
        public string GenderName{ 
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.Female:
                        return Properties.Resources.Enum_Gender_Female;
                    case Enum.Gender.Male:
                        return Properties.Resources.Enum_Gender_Male;
                    case Enum.Gender.Other:
                        return Properties.Resources.Enum_Gender_Other;
                    default:
                        return null;
                }
            }
        }
        public string PhoneNumber { get; set; }
        public string TelephoneNumber { get; set; }

        [ExportExcel]
        [PropertyName("Số tài khoản")]
        public string BankAccountNumber { get; set; }

        [ExportExcel]
        [PropertyName("Tên ngân hàng")]
        public string BankName { get; set; }

        [ExportExcel]
        [PropertyName("Chi nhánh ngân hàng")]
        public string BankBranchName { get; set; }

        [ExportExcel]
        [PropertyName("Chức danh")]
        public string PositionName { get; set; }

        [Ignore]
        public List<string> ErrorMsgs { get; set; } = new List<string>();
    }
}
