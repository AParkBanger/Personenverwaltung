namespace Data.Models
{
    /// <summary>
    /// BaseModel.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual long Id { get; set; }
    }
}
