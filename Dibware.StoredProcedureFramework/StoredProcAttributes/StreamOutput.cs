using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Allows the setting of the user defined table type name for table valued parameters
    /// </summary>
    public class StreamOutput : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StreamOutput"/> is buffered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if buffered; otherwise, <c>false</c>.
        /// </value>
        public Boolean Buffered { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [leave stream open].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [leave stream open]; otherwise, <c>false</c>.
        /// </value>
        public Boolean LeaveStreamOpen { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamOutput"/> class.
        /// </summary>
        public StreamOutput() { }
    }
}
