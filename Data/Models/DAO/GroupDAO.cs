using System.Collections.Generic;

namespace Data.Models.DAO
{
    /// <summary>
    /// Group.
    /// </summary>
    public class GroupDAO : BaseModelDAO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the persons.
        /// </summary>
        /// <value>The persons.</value>
        public ICollection<PersonDAO> Persons { get; set; }
    }
}