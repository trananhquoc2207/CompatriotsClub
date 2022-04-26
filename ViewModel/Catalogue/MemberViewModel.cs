namespace ViewModel
{
#nullable disable
    public class MemberViewModel
    {
        public int FamilyId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public string Idcard { get; set; }
        public string Notes { get; set; }
        public string Addres { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Word { get; set; }
        public bool IsDelete { get; set; }
        public int? IdAccount { get; set; }
    }
    public class MemberResponseViewModel
    {
        public int FamilyId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public string Idcard { get; set; }
        public string Notes { get; set; }
        public string Addres { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Word { get; set; }
        public bool IsDelete { get; set; }
        public int? IdAccount { get; set; }

    }
}
