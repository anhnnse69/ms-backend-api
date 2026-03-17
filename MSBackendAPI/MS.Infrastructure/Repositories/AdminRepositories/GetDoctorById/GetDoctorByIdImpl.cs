using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Application.Services.AdminServices.GetDoctorById
{
    /// <summary>
    /// Provides an implementation for retrieving doctor information by ID.
    /// </summary>
    public class GetDoctorByIdImpl : RepositoryBase<Doctor, Guid, AppDbContext>, IGetDoctorById
    {
        /// <summary>
        /// Initializes a new instance of the GetDoctorByIdImpl class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access data.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work instance for managing transactions.
        /// </param>
        public GetDoctorByIdImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork) 
        { 
        }

        /// <summary>
        /// Retrieves a doctor by their unique identifier.
        /// </summary>
        /// <param name="id">
        /// The doctor ID.
        /// </param>
        /// <returns>
        /// A <see cref="Doctor"/> entity if found and not deleted; otherwise null.
        /// </returns>
        public async Task<Doctor> Execute(Guid id)
        {
            return await FindByCondition(x => x.Id == id && !x.IsDeleted,false).FirstOrDefaultAsync();
        }
    }
}