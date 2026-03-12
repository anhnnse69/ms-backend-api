using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetFacilityById
{
    /// <summary>
    /// Provides an implementation for retrieving facility by ID.
    /// </summary>
    public class GetFacilityByIdImpl
        : RepositoryBase<Facility, Guid, AppDbContext>, IGetFacilityById
    {
        /// <summary>
        /// Initializes a new instance of the GetFacilityByIdImpl class.
        /// </summary>
        public GetFacilityByIdImpl(
            AppDbContext context,
            IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Retrieves facility information by ID.
        /// </summary>
        public async Task<Facility?> Execute(Guid id)
        {
            return await FindByCondition(x => x.Id == id && !x.IsDeleted, false)
                .FirstOrDefaultAsync();
        }
    }
}