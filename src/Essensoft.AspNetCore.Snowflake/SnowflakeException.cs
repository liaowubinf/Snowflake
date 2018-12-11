using System;

namespace Essensoft.AspNetCore.Snowflake
{
    /// <summary>
    /// Snowflake 异常。
    /// </summary>
    public class SnowflakeException : Exception
    {
        public SnowflakeException(string messages) : base(messages)
        {
        }
    }
}
