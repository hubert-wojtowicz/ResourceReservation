namespace ReservationApi.ApiContract
{
    /// <summary>
    /// Define pagination interface.
    /// </summary>
    public interface IPaginable
    {
        /// <summary>
        /// Gets or sets the take.
        /// </summary>
        /// <value>
        /// The take.
        /// </value>
        int Take { get; set; }

        /// <summary>
        /// Gets or sets the s kip.
        /// </summary>
        /// <value>
        /// The skip.
        /// </value>
        int Skip { get; set; }
    }
}