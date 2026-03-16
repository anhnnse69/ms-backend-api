namespace MS.Application.Services.AdminServices.GetAllDoctorsService
{
        /// <summary>
        /// Response model for getting all doctors
        /// </summary>
        public class GetAllDoctorsResponse
        {
            public Guid Id { get; set; }
            public string DisplayName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string AvatarUrl { get; set; }
            public int YearsOfExperience { get; set; }
            public double AverageRating { get; set; }
            public int RatingCount { get; set; }
            public string Specialty { get; set; }
            public bool IsDeleted { get; set; }
        } 
}
