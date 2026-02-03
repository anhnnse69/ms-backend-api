namespace MS.Domain.Enums.GeneralCodes
{
    /// <summary>
    /// Authentication and Authorization specific message codes
    /// </summary>
    public enum AuthMessageCode
    {
        /// <summary>
        /// Access token is missing in the Authorization header.
        /// (Original Case: token missing)
        /// </summary>
        APP_MESSAGE_0001,

        /// <summary>
        /// Access token is malformed or invalid.
        /// (Original Case: token invalid)
        /// </summary>
        APP_MESSAGE_0002,

        /// <summary>
        /// User does not have permission to access this resource.
        /// (Original Case: permission denied)
        /// </summary>
        APP_MESSAGE_0003,

        /// <summary>
        /// Access token has expired.
        /// (Original Case: token expired)
        /// </summary>
        APP_MESSAGE_0004,

        /// <summary>
        /// Account is locked due to multiple failed login attempts.
        /// (Original Case: account locked)
        /// </summary>
        APP_MESSAGE_0005
    }
}