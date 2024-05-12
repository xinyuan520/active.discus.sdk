namespace Discus.CommonService.Authority.Repository.Entities
{
    [SugarTable("user_info")]
    public class UserInfo : FullEnity, IDeleted
    {
        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }

        [SugarColumn(ColumnName = "password")]
        public string Password { get; set; }

        [SugarColumn(ColumnName = "nick_name")]
        public string Nickname { get; set; }

        [SugarColumn(ColumnName = "mobile")]
        public string Mobile { get; set; }

        [SugarColumn(ColumnName = "email")]
        public string Email { get; set; }

        [SugarColumn(ColumnName = "age")]
        public int Age { get; set; }

        [SugarColumn(ColumnName = "sex")]
        public int sex { get; set; }

        [SugarColumn(ColumnName = "description")]
        public string Description { get; set; }

        [SugarColumn(ColumnName = "state")]
        public bool State { get; set; }

        [SugarColumn(ColumnName = "is_admin")]
        public bool IsAdmin { get; set; }

        [SugarColumn(ColumnName = "is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
