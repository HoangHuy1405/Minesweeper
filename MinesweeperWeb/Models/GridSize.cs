using System.ComponentModel.DataAnnotations;

namespace MinesweeperWeb.Models
{
    public class GridSize
    {
        [Required(ErrorMessage = "Width is required")]
        [Range(4, 30, ErrorMessage = "Rows must be between 8 and 30")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Range(4, 30, ErrorMessage = "Columns must be between 8 and 30")]
        public int Width { get; set; }
    }
}
