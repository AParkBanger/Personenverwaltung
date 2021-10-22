namespace Data.Models.DAO
{
    /// <summary>
    /// BaseModel.
    /// </summary>
    public abstract class BaseModelDAO
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual long Id { get; set; }
    }
}