using LibApp.Data;
using LibApp.interfaces;
using LibApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Repositories
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly ApplicationDbContext context;

        public MembershipTypeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddMembershipType(MembershipType membershipType)
        {
            context.MembershipTypes.Add(membershipType);
        }

        public void DeleteMembershipType(int membershipTypeId)
        {
            context.MembershipTypes.Remove(GetMembershipTypeById(membershipTypeId));
        }

        public MembershipType GetMembershipTypeById(int membershipTypeId)
        {
            return context.MembershipTypes.Find(membershipTypeId);
        }

        public IEnumerable<MembershipType> GetMembershipTypes()
        {
            return context.MembershipTypes;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateMembershipType(MembershipType membershipType)
        {
            context.MembershipTypes.Update(membershipType);
        }
    }
}
