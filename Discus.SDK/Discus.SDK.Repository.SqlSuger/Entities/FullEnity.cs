using SqlSugar;

namespace Discus.SDK.Repository.SqlSugar.Entities
{
    public class FullEnity : Entity, ICreateEntity, IEditEntity
    {
        [SugarColumn(ColumnName = "create_by_id")]
        public long CreateById { get; set; }
        [SugarColumn(ColumnName = "create_by_time")]
        public DateTime CreateByTime { get; set; }
        [SugarColumn(ColumnName = "edit_by_id")]
        public long EditById { get; set; }
        [SugarColumn(ColumnName = "edit_by_time")]
        public DateTime EditByTime { get; set; }
    }
}
