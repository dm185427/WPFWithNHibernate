using System;

namespace PlayersInfo
{
    public class Player
    {
        public virtual int Id { get; set; }
        public virtual string PlayerName { get; set; }
        public virtual int PlayerAge { get; set; }
        public virtual DateTime DOJ { get; set; }
        public virtual string BelongsTo { get; set; }
    }
}
