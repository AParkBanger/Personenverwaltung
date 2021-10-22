namespace Data
{
    using Data.Models.DAO;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// PersonManagementContext.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class PersonManagementContext : DbContext
    {
        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>The groups.</value>
        public DbSet<GroupDAO> Groups { get; set; }

        /// <summary>
        /// Gets or sets the persons.
        /// </summary>
        /// <value>The persons.</value>
        public DbSet<PersonDAO> Persons { get; set; }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used
        /// for this context. This method is called for each instance of the context that
        /// is created. The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see
        /// cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have
        /// been passed to the constructor, you can use <see
        /// cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" />
        /// to determine if the options have already been set, and skip some or all of the
        /// logic in <see
        /// cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)"
        /// />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">
        /// A builder used to create or modify options for this context. Databases (and
        /// other extensions) typically define extension methods on this object that allow
        /// you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-9ODII40; database=PersonManagementDatabase; Integrated Security = true");
        }
    }
}