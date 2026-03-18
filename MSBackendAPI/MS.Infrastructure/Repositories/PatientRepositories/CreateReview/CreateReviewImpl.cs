using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.CreateReview
{
    /// <summary>
    /// Repository implementation for persisting a new review to the database.
    /// </summary>
    public class CreateReviewImpl : RepositoryBase<Review, Guid, AppDbContext>, ICreateReview
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReviewImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public CreateReviewImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Persists the specified review entity to the database.
        /// </summary>
        /// <param name="review">The review entity to persist.</param>
        /// <returns>The persisted review entity.</returns>
        public async Task<Review> Execute(Review review)
        {
            await CreateAsync(review);
            await SaveChangesAsync();
            return review;
        }
    }
}
