using MS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Infrastructure.Repositories.PatientRepositories.CancelAppointment
{
    public interface ICancelAppointment
    {
        /// <summary>
        /// Update appointment status to Cancelled in the data store
        /// </summary>
        /// <param name="appointment">The appointment entity</param>
        /// <returns>Task</returns>
        Task Execute(Appointment appointment);
    }
}
