using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.ManagerServices.AppointmentReportService
{
    /// <summary>
    /// Request object for getting appointment report.
    /// </summary>
    public class AppointmentReportRequest
    {
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid FacilityId { get; set; }
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(10, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Type { get; set; } = "day";
        public DateTime? Date { get; set; }
    }
}