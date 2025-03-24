using System.Collections.Generic;

namespace eCinema.Data.ViewModels
{
    public class ManageRolesVM
    {
        public string UserId { get; set; } // رقم المستخدم
        public List<string> UserRoles { get; set; } = new List<string>(); // الأدوار الحالية للمستخدم
        public List<string> AvailableRoles { get; set; } = new List<string>(); // جميع الأدوار المتاحة
    }
}
