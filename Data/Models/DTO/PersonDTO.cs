using Data.Enumerations;
using Data.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.DTO
{
    /// <summary>
    /// PersonDTO.
    /// </summary>
    public class PersonDTO : BaseModelDTO
    {
        /// <summary>
        /// Gets or sets the year of birth.
        /// </summary>
        /// <value>The year of birth.</value>
        public DateTime Birth { get; set; }

        /// <summary>
        /// Gets or sets the vorname.
        /// </summary>
        /// <value>The vorname.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>The groups.</value>
        public ICollection<GroupDTO> Groups { get; set; }

        /// <summary>
        /// Gets or sets the nachname.
        /// </summary>
        /// <value>The nachname.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the sex.
        /// </summary>
        /// <value>The sex.</value>
        public Sex Sex { get; set; }
    }
}