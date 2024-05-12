using SqlSugar;

namespace Discus.SDK.Repository.SqlSugar.Entities
{
    public class Entity
    {
        [SugarColumn(IsPrimaryKey =true, ColumnName = "id")]
        public long Id { get; set; }
    }
}
