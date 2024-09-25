using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MinesweeperWeb.Models {
    public class Board {
        [DefaultValue(8)]
        [Range(8,30, ErrorMessage = "Rows must be between 8 and 30")]
        public int Rows { get; set; }

        [DefaultValue(8)]
        [Range(8, 30, ErrorMessage = "Columns must be between 8 and 30")]
        public int Columns { get; set; }

        public Board() {
            Rows = 8; 
            Columns = 8; 
        }

        public Board(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
        }
    }
}
