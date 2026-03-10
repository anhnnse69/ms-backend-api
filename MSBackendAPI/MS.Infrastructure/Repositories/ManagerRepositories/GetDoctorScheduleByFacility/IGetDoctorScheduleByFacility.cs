using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorScheduleByFacility
{
    /// <summary>
    /// Repository contract for retrieving doctor schedules by facility
    /// </summary>
    public interface IGetDoctorScheduleByFacility
    {
        /// <summary>
        /// Retrieve doctors and their schedules for a specific facility
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>
        /// Tuple containing the list of doctors with schedules and the total record count
        /// </returns>
        Task<(List<Doctor> Doctors, int Total)> Execute(Guid facilityId, int page, int size);
    }
}