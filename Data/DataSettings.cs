using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class DataSettings
    {
        [Required]
        public string ConnectionString { get; set; } = string.Empty;
    }
}
