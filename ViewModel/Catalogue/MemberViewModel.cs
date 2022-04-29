using ViewModel.common;

namespace ViewModel
{
#nullable disable
    public class MemberViewModel
    {
        public int GroupId { get; set; } = 1;
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public int Gender { get; set; }
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
        public int Gender { get; set; }
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

    public class MemberFilter : PagingFilter
    {
        public string Keyword { get; set; }
    }
}
