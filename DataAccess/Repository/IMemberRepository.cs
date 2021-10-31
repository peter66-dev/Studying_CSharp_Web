using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DataAccess
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMemberByID(int id);
        void InsertMember(Member mem);
        void DeleteMember(int id);
        void UpdateMember(Member mem);
    }
}
