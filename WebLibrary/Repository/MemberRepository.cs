using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.DataAccess;

namespace WebLibrary.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(int id) => MemberDAO.Instance.Remove(id);

        public Member GetMemberByID(int id) => MemberDAO.Instance.GetMemberByID(id);

        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMembers();

        public void InsertMember(Member mem) => MemberDAO.Instance.AddNew(mem);

        public void UpdateMember(Member mem) => MemberDAO.Instance.Update(mem);
    }
}
