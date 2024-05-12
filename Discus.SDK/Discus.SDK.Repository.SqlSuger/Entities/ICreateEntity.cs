namespace Discus.SDK.Repository.SqlSugar.Entities
{
    public interface ICreateEntity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public long CreateById { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateByTime { get; set; }

    }
}
