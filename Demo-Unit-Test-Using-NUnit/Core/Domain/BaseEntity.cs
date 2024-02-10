using System.ComponentModel.DataAnnotations;

namespace Demo_Unit_Test_Using_NUnit.Core.Domain
{
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
    }
}
