namespace Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Building
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        public int BuildingOwnerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int UnitCount { get; set; }

        #region Navigation properties

        public virtual BuildingOwner BuildingOwner { get; set; } = null!;

        #endregion
    }
}
