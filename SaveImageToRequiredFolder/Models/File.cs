using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveImageToRequiredFolder.Models
{
    public class File
    {
        public File()
        {
        }

        public File(string name, int folderId)
        {
            this.name = name;
            this.folderId = folderId;
        }

        public File(int id, string name, int folderId, Folder folder)
        {
            this.id = id;
            this.name = name;
            this.folderId = folderId;
            this.folder = folder;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int folderId { get; set; }

        [ForeignKey("folderId")]
        public Folder folder { get; set; }
    }
}
