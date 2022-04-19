using System.ComponentModel.DataAnnotations;

namespace assigntwo.Data.Entities
{
    public class Match
    {
        [Key]
        public string Key { get; set; } = null!;

        public int Value { get; set; }
    }
}
