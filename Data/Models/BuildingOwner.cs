namespace Data.Models
{
    using System.Collections.Generic;

    public class BuildingOwner
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        #region Navigation properties

        public virtual ICollection<Building> Buildings { get; } = new List<Building>();

        #endregion
    }
}
