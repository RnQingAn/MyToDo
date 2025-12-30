// 分页结果类
public class PageResult<T>
{
    /// <summary>
    /// 1. Items - 当前页的数据列表
    /// </summary>
    public List<T>? Data { get; set; }=new List<T>();
    // ✅ 作用：存储用户当前看到的数据
    // ✅ 示例：[任务1, 任务2, 任务3, ..., 任务20]

    /// <summary>
    /// 2. TotalCount - 符合条件的总记录数
    /// </summary>
    public int TotalCount { get; set; } = 0;
    // ✅ 作用：告诉用户总共有多少条数据
    // ✅ 示例：105（数据库中有105条符合条件的记录）

    /// <summary>
    /// 3. Page - 当前页码
    /// </summary>
    public int Page { get; set; } = 0;
    // ✅ 作用：标识用户当前在第几页
    // ✅ 示例：3（用户正在看第3页）

    /// <summary>
    /// 4. PageSize - 每页显示数量
    /// </summary>
    public int PageSize { get; set; } = 0;
    // ✅ 作用：定义每页显示多少条数据
    // ✅ 示例：20（每页显示20条数据）

    /// <summary>
    /// 5. TotalPages - 总页数（计算属性）
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    // ✅ 作用：自动计算总共需要多少页
    // ✅ 示例：6（105条数据，每页20条，需要6页显示完）
}