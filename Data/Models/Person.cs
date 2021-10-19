namespace Data.Models
{
    using System;
    using System.Collections.Generic;
    using Data.Enumerations;

    /// <summary>
    /// Person.
    /// </summary>
    /// <seealso cref="Data.Models.BaseModel" />
    public class Person : BaseModel
    {
        /// <summary>
        /// Gets or sets the vorname.
        /// </summary>
        /// <value>
        /// The vorname.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the nachname.
        /// </summary>
        /// <value>
        /// The nachname.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the year of birth.
        /// </summary>
        /// <value>
        /// The year of birth.
        /// </value>
        public DateTime Birth { get; set; }

        /// <summary>
        /// Gets or sets the sex.
        /// </summary>
        /// <value>
        /// The sex.
        /// </value>
        public Sex Sex { get; set; }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        public ICollection<Group> Groups { get; set; }
    }
}
