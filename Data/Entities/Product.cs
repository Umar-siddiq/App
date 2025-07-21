using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Data.Entities
{
    [Table("Product", Schema = "SaleLT")]
    public class Product
    {
        [Key]
        public int ProductId   { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public int ProductNumber { get; set; } 
        
        public string Color { get; set; }

        [Column(TypeName = "money")]
        public decimal? StandardCost { get; set; }
        
        [Column(TypeName = "money")]
        public decimal? ListPrice {  get; set; }
        
        public string Size { get; set; }

        public int? ProductCategoryId { get; set; }

        public int? ProductModelId { get; set; }

        public byte[] ThumbnailPhoto { get; set; }

        public string ThumbnailPhotoFileName    { get; set; }

        public DateTime SellStartDate {  get; set; }

        public DateTime SellEndDate { get; set; }

        public DateTime DiscontinuedDate { get; set; }

        public DateTime ModifiedData {  get; set; }
        public Guid rowguid { get; set; }


    }
}
