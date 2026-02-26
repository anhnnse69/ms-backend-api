namespace MS.Domain.Enums.Roles
{
    /// <summary>
    /// System role enum for user authorization
    /// Simplified to 3 roles as per requirements:
    /// - ITAdmin: Full system rights
    /// - Manager: System management (e.g., for facility managers or doctors)
    /// - Patient: Users who create accounts and book appointments (e.g., patients)
    /// </summary>
    public enum SystemRole
    {
        ITAdmin = 1,
        Manager = 2,
        Patient = 3
    }
}