using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    /// <summary>
    /// 分页数据模型
    /// </summary>
    public class PaginationData
    {
        /// <summary>
        /// 2. TotalCount - 符合条件的总记录数
        /// </summary>
        public int TotalCount { get; set; }
        // ✅ 作用：告诉用户总共有多少条数据
        // ✅ 示例：105（数据库中有105条符合条件的记录）

        /// <summary>
        /// 3. Page - 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        // ✅ 作用：标识用户当前在第几页
        // ✅ 示例：3（用户正在看第3页）

        /// <summary>
        /// 4. PageSize - 每页显示数量
        /// </summary>
        public int PageSize { get; set; }
        // ✅ 作用：定义每页显示多少条数据
        // ✅ 示例：20（每页显示20条数据）

        /// <summary>
        /// 5. TotalPages - 总页数（计算属性）
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        // ✅ 作用：自动计算总共需要多少页
        // ✅ 示例：6（105条数据，每页20条，需要6页显示完）

        public PaginationData() { }

        public PaginationData(int pageIndex, int pageSize, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }


    }
}
