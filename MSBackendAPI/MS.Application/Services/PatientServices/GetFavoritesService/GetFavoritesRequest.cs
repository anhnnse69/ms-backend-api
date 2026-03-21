using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.PatientServices.GetFavoritesService
{
    /// <summary>
    /// Represents the query parameters for retrieving the patient's favorites list.
    /// </summary>
    public class GetFavoritesRequest
    {
        /// <summary>Current page index (default = 1).</summary>
        [Range(1, int.MaxValue, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public int Page { get; set; } = 1;
        /// <summary>Number of records per page (default = 10).</summary>
        [Range(1, 100, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public int Size { get; set; } = 10;
        /// <summary>Filter type: "Doctor", "Facility", or "All" (default = "All").</summary>
        public string Type { get; set; } = "All";
    }
}
