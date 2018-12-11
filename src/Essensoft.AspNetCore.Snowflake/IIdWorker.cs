namespace Essensoft.AspNetCore.Snowflake
{
    /// <summary>
    /// Id Worker
    /// </summary>
    public interface IIdWorker
    {
        /// <summary>
        /// 获取下一个Id
        /// </summary>
        /// <returns>Id</returns>
        long NextId();
    }
}