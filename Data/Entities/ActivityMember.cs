#nullable disable

namespace CompatriotsClub.Data
{
    public partial class ActivityMember
    {
        public int MemberId { get; set; }
        public int ActivityId { get; set; }

        public virtual Member Activity { get; set; }
        public virtual Activity Member { get; set; }
    }
}
