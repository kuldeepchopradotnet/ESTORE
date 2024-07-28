using System.ComponentModel.DataAnnotations;

namespace ESTORE.Models.Product
{
    public class AddProduct
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public IFormFile file { get; set; }

        [StringLength(500)]
        public string Description { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }


      /*  learing*/

        /*private decimal _price;

        public decimal Price
        {
            get
            {
                return _price;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price must be a non-negative value.");
                }
                _price = value;
            }
        }*/

        public int Quantity { get; set; }



    }
}
