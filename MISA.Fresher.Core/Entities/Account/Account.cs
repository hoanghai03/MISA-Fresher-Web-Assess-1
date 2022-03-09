using MISA.Fresher.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Account
{
    public class Account : BaseEntity
    {
        // khóa chính
        [Id]
        public Guid AccountId { get; set; }
        // số tài khoản
        [NotEmpty]
        [NotDuplicate]
        [PropertyName("Số tài khoản")]
        public string AccountNumber { get; set; }
        // tên tài khoản
        [NotEmpty]
        [PropertyName("Tên tài khoản")]
        public string AccountName { get; set; }
        // tên tiếng anh
        public string AccountNameEnglish { get; set; }
        // Tính chất (1-Dư nợ, 2-Dư có ,3- Lưỡng tính,4- không có số dư)
        [NotEmpty]
        [PropertyName("Tính chất")]
        public int? AccountCategoryKind { get; set; }
        [Ignore]
        public string AccountCategorykindName
        {
            get
            {
                switch (AccountCategoryKind)
                {
                    case (int)Enum.AccountCategoryKind.Debt:
                        return Properties.Resources.Debt;
                    case (int)Enum.AccountCategoryKind.Credit:
                        return Properties.Resources.Credit;
                    case (int)Enum.AccountCategoryKind.Hermaphrodite:
                        return Properties.Resources.Hermaphrodite;
                    case (int)Enum.AccountCategoryKind.NoBalance:
                        return Properties.Resources.NoBalance;
                    default:
                        return null;
                }
            }
        }

        // diễn giải
        public string Description { get; set; }
        // Có hoạch toán ngoại tệ (0-sai,1-đúng)
        public bool? ForeignCurrencyAccount { get; set; }
        //Đối tượng (1- nhà cung cấp,2-khách hàng,3-nhân viên)
        public int? DetailByObjectKind { get; set; }
        // tên đối tượng
        [Ignore]
        public string DetailByObjectKindName
        {
            get
            {
                switch (DetailByObjectKind)
                {
                    case (int)Enum.DetailByObjectKind.Supplier:
                        return Properties.Resources.Supplier;
                    case (int)Enum.DetailByObjectKind.Customer:
                        return Properties.Resources.Customer;
                    case (int)Enum.DetailByObjectKind.Employee:
                        return Properties.Resources.Employee;
                    default:
                        return null;
                }


            }
        }
        public bool? DetailByAccount { get; set; }

        //Theo dõi chi tiết theo Đối tượng THCP( 0- Chỉ cảnh báo; 1- Bắt buộc nhập)
        public byte? DetailByCostAggregationObjKind { get; set; }
        [Ignore]
        public string DetailByCostAggregationObjKindName
        {
            get
            {
                switch (DetailByCostAggregationObjKind)
                {
                    case (int)Enum.Follow.Alert:
                        return Properties.Resources.Alert;
                    case (int)Enum.Follow.Obligate:
                        return Properties.Resources.Obligate;
                    default:
                        return null;
                }
            }
        }
        //Theo dõi chi tiết theo Đơn đặt hàng (0 - Chỉ cảnh báo; 1- Bắt buộc nhấp)
        public byte? DetailByOrderKind { get; set; }
        [Ignore]
        public string DetailByOrderKindName
        {
            get
            {
                switch (DetailByOrderKind)
                {
                    case (int)Enum.Follow.Alert:
                        return Properties.Resources.Alert;
                    case (int)Enum.Follow.Obligate:
                        return Properties.Resources.Obligate;
                    default:
                        return null;
                }
            }
        }
        //Theo dõi chi tiết theo Hợp đồng mua (0 - Chỉ cảnh báo; 1- Bắt buộc nhấp)
        public byte? DetailByPurchaseContractKind { get; set; }
        [Ignore]
        public string DetailByPurchaseContractKindName
        {
            get
            {
                switch (DetailByPurchaseContractKind)
                {
                    case (int)Enum.Follow.Alert:
                        return Properties.Resources.Alert;
                    case (int)Enum.Follow.Obligate:
                        return Properties.Resources.Obligate;
                    default:
                        return null;
                }
            }
        }
        //Theo dõi chi tiết theo Đơn vị (0 - Chỉ cảnh báo; 1- Bắt buộc nhấp)
        public byte? DetailByUnitKind { get; set; }
        [Ignore]
        public string DetailByUnitKindName
        {
            get
            {
                switch (DetailByUnitKind)
                {
                    case (int)Enum.Follow.Alert:
                        return Properties.Resources.Alert;
                    case (int)Enum.Follow.Obligate:
                        return Properties.Resources.Obligate;
                    default:
                        return null;
                }
            }
        }
        //Theo dõi chi tiết theo Công trình (0 - Chỉ cảnh báo; 1- Bắt buộc nhấp)
        public byte? DetailByContructionKind { get; set; }
        [Ignore]
        public string DetailByContructionKindName
        {
            get
            {
                switch (DetailByContructionKind)
                {
                    case (int)Enum.Follow.Alert:
                        return Properties.Resources.Alert;
                    case (int)Enum.Follow.Obligate:
                        return Properties.Resources.Obligate;
                    default:
                        return null;
                }
            }
        }
        //Theo dõi chi tiết theo Hợp đồng bán (0 - Chỉ cảnh báo; 1- Bắt buộc nhấp)
        public byte? DetailByContractSaleKind { get; set; }
        [Ignore]
        public string DetailByContractSaleKindName
        {
            get
            {
                switch (DetailByContractSaleKind)
                {
                    case (int)Enum.Follow.Alert:
                        return Properties.Resources.Alert;
                    case (int)Enum.Follow.Obligate:
                        return Properties.Resources.Obligate;
                    default:
                        return null;
                }
            }
        }
        //Theo dõi chi tiết theo Khoản mục chi phí (0 - Chỉ cảnh báo; 1- Bắt buộc nhấp)
        public byte? DetailByExpenseItemKind { get; set; }
        [Ignore]
        public string DetailByExpenseItemKindName
        {
            get
            {
                switch (DetailByExpenseItemKind)
                {
                    case (int)Enum.Follow.Alert:
                        return Properties.Resources.Alert;
                    case (int)Enum.Follow.Obligate:
                        return Properties.Resources.Obligate;
                    default:
                        return null;
                }
            }
        }
        //Theo dõi chi tiết theo Mã thống kê(0 - Chỉ cảnh báo; 1- Bắt buộc nhấp)
        public byte? DetailByStatisticalCodeKind { get; set; }

        [Ignore]
        public string DetailByStatisticalCodeKindName
        {
            get
            {
                switch (DetailByStatisticalCodeKind)
                {
                    case (int)Enum.Follow.Alert:
                        return Properties.Resources.Alert;
                    case (int)Enum.Follow.Obligate:
                        return Properties.Resources.Obligate;
                    default:
                        return null;
                }
            }
        }
        //Theo dõi chi tiết theo Đối tượng(0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByObject { get; set; }
        //Theo dõi chi tiết theo Đối tượng THCP (0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByCostAggregationObj { get; set; }
        //Theo dõi chi tiết theo Đơn đặt hàng (0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByOrder { get; set; }
        //Theo dõi chi tiết theo Hợp đồng mua(0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByPurchaseContract { get; set; }
        //Theo dõi chi tiết theo Đơn vị(0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByUnit { get; set; }
        //Theo dõi chi tiết theo Công trình(0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByContruction { get; set; }
        //Theo dõi chi tiết theo Hợp đồng bán(0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByContractSale { get; set; }
        //Theo dõi chi tiết theo Khoản mục chi phí(0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByExpenseItem { get; set; }
        //Theo dõi chi tiết theo Mã thống kê(0 - Không theo dõi; 1- Theo dõi)
        public bool? DetailByStatisticalCode { get; set; }

        public Guid? ParentId { get; set; }

        // trạng thái (1- đang sử dụng,2-ngưng sử dụng)
        public byte? Status { get; set; }
        [Ignore]
        public string StatusName {
            get
            {
                switch (Status)
                {
                    case (int) Enum.Status.Status_1:
                        return Properties.Resources.Status_Name_1;
                    case (int) Enum.Status.Status_0:
                        return Properties.Resources.Status_Name_0;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Ignore]
        public List<Account> Children { get; set; } = new List<Account>();

        [Ignore]
        public int? ChildIndex { get; set; }

        [Ignore]
        public string ParentName { get; set; }
    }
}
