using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.DoctorServices.UpdateDoctorMedicalRecordService
{
    /// <summary>
    /// Request model used to update medical record
    /// </summary>
    public class UpdateDoctorMedicalRecordRequest
    {
        /// <summary> Patient symptoms </summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(2000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Symptoms { get; set; } = string.Empty;

        /// <summary> Doctor diagnosis </summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(2000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string Diagnosis { get; set; } = string.Empty;

        /// <summary> Additional notes </summary>
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(3000, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string? Notes { get; set; }
    }
}
