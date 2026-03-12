using MS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetFacilityById
{
    public interface IGetFacilityById
    {
        Task<Facility?> Execute(Guid id);
    }
}
