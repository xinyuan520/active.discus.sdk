namespace Discus.SDK.Repository.SqlSugar.Entities
{
    public interface IEditEntity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public long EditById { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime EditByTime { get; set; }
    }
}
