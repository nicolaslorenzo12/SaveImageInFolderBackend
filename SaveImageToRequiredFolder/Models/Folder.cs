using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveImageToRequiredFolder.Models
{
    public class Folder
    {
        public Folder(string name)
        {
            this.name = name;
        }

        public Folder(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string name { get; set; }
    }
}
