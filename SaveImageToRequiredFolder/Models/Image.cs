using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveImageToRequiredFolder.Models
{
    public class Image
    {
        public Image(string fileName, byte[] data, string folderName)
        {
            this.fileName = fileName;
            this.data = data;
            this.folderName = folderName;
        }

        public Image(int id, string fileName, byte[] data, string folderName)
        {
            this.id = id;
            this.fileName = fileName;
            this.data = data;
            this.folderName = folderName;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string fileName { get; set; }

        [Required]
        public byte[] data { get; set; }

        [Required]
        public string folderName { get; set; }

    }
}
