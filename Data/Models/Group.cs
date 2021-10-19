using System.Collections.Generic;

namespace Data.Models
{
    /// <summary>
    /// Group.
    /// </summary>
    /// <seealso cref="Data.Models.BaseModel" />
    public class Group : BaseModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the persons.
        /// </summary>
        /// <value>
        /// The persons.
        /// </value>
        public ICollection<Person> Persons { get; set; }
    }
}
