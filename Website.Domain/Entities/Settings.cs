using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Abstract;

namespace Website.Domain.Entities
{

    /// <summary>
    ///SettingID: Định danh duy nhất cho mỗi cài đặt.
    ///Key: Khóa định danh cho cài đặt (ví dụ: HomePageTitle, ContactEmail, PostsPerPage, etc.).
    ///Value: Giá trị của cài đặt.
    ///Type: Kiểu dữ liệu của giá trị(ví dụ: String, Integer, Boolean, etc.) để đảm bảo ứng dụng xử lý chính xác.
    ///Description: Mô tả về cài đặt, giúp người quản trị hiểu cài đặt này ảnh hưởng như thế nào đến website 
    /// </summary>
    
    public class Settings : Entity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
