using MS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Infrastructure.Repositories.PatientRepositories.CreateReview
{
    /// <summary>
    /// Repository interface for persisting a new review to the database.
    /// </summary>
    public interface ICreateReview
    {
        /// <summary>
        /// Persists the specified review entity to the database.
        /// </summary>
        /// <param name="review">The review entity to persist.</param>
        /// <returns>The persisted review entity.</returns>
        Task<Review> Execute(Review review);
    }
}
