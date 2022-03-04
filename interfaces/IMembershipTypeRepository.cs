using LibApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.interfaces
{
    public interface IMembershipTypeRepository
    {
        IEnumerable<MembershipType> GetMembershipTypes();
        MembershipType GetMembershipTypeById(int membershipTypeId);
        void AddMembershipType(MembershipType membershipType);
        void UpdateMembershipType(MembershipType membershipType);
        void DeleteMembershipType(int membershipTypeId);

        void Save();
    }
}
