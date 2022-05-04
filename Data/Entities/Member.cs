#nullable disable


namespace CompatriotsClub.Data
{
    public partial class Member
    {
        public Member()
        {
            AddressMembers = new HashSet<AddressMember>();
            ContactMembers = new HashSet<ContactMembers>();
            MemberUsers = new HashSet<MemberUser>();
            RoleMembers = new HashSet<PositionMember>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public int Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public string Idcard { get; set; }
        public string Addres { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Word { get; set; }
        public bool IsDelete { get; set; }
        public int? IdAccount { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<AddressMember> AddressMembers { get; set; }
        public virtual ICollection<ContactMembers> ContactMembers { get; set; }
        public virtual ICollection<MemberUser> MemberUsers { get; set; }
        public virtual ICollection<PositionMember> RoleMembers { get; set; }

    }
}