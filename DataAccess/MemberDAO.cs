using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Member> GetMembers()
        {
            List<Member> list = new List<Member>();
            try
            {
                using (var context = new FStoreContext())
                {
                    list = context.Members.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Member GetMemberByID(int id)
        {
            Member mem = null;
            try
            {
                using var context = new FStoreContext();
                mem = context.Members.SingleOrDefault(mem => mem.MemberId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }

        public void AddNew(Member mem)
        {
            try
            {
                Member tmp = GetMemberByID(mem.MemberId);
                if (tmp == null)
                {
                    using var context = new FStoreContext();
                    context.Members.Add(mem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This member is already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Member mem)
        {
            try
            {
                Member tmp = GetMemberByID(mem.MemberId);
                if (tmp != null)
                {
                    using var context = new FStoreContext();
                    context.Members.Update(mem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This member does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                Member tmp = GetMemberByID(id);
                if (tmp != null)
                {
                    using var context = new FStoreContext();
                    context.Members.Remove(tmp);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This member does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
