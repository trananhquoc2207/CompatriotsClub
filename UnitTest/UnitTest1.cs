using NUnit.Framework;
using Service.Catalogue;

namespace UnitTest
{
    public class Tests
    {
        private IMemberService _memberService;
        [SetUp]
        public void Setup()
        {
            _memberService = new MemberService();
        }

        [Test]
        public void Test1()
        {
            var s = _memberService.GetAll();
            Assert.AreEqual(0, s.Count);
        }
    }
}