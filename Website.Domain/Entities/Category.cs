using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Domain.Abstract;

namespace Website.Domain.Entities
{

    /// <summary>
    ///CategoryID: Định danh duy nhất cho mỗi chủ đề/thể loại.
    ///Name: Tên của chủ đề/thể loại.
    ///Description: Mô tả về chủ đề/thể loại.
    /// </summary>
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
