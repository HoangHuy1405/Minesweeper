using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MinesweeperWeb.Models {
    public class Board {
        [DefaultValue(8)]
        [Range(8,30, ErrorMessage = "Rows must be between 8 and 30")]
        public int Height { get; set; }

        [DefaultValue(8)]
        [Range(8, 30, ErrorMessage = "Columns must be between 8 and 30")]
        public int Width { get; set; }


        /*
         * 0 present for empty
         * landminesInt present for the number of the landmines
         */
        public int[,] BoardArr { get; }
        public int Landmines { get; }
        private int LandminesInt = -8;
        public int getLandminesInt() { return LandminesInt; }

        float difficulty = 0.3f;
        public Board() {
            this.Height = 8;
            this.Width = 8;
            this.Landmines = (int)Math.Round(difficulty * 8 * 8);
            BoardArr = new int[8, 8];

            Clear();
        }
        public Board(int height, int width) {
            this.Height = height;
            this.Width = width;
            this.Landmines = (int)Math.Round(difficulty * height * width);
            BoardArr = new int[height, width];

            Clear();
        }
        public void Clear() {
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    BoardArr[i, j] = 0;
                }
            }
        }
        public void Generate() {
            Clear();
            PlantMines();
        }
        private void PlantMines() {
            int range = Height * Width;

            List<int> coordinate = Enumerable.Range(0, range).ToList();
            Random rand = new Random();

            for (int i = 0; i < Landmines; i++) {
                int index = rand.Next(range--);

                int row = coordinate[index] / Height;
                int col = coordinate[index] % Width;

                BoardArr[row, col] = LandminesInt;
                FillNum(row, col);

                coordinate.RemoveAt(index);
            }
        }
        private void FillNum(int row, int col) {
            for (int i = row - 1; i <= row + 1; i++) {
                // if row go over up and low boundary then skip
                if (i < 0 || i >= Height) continue;

                for (int j = col - 1; j <= col + 1; j++) {
                    //if col go over left and right boundary then skip
                    if (j < 0 || j >= Width) continue;
                    // if landmines then skip
                    if (BoardArr[i, j] == LandminesInt) continue;

                    BoardArr[i, j] += 1;
                }
            }
        }
        //public int countLandmines()
        //{
        //    int count = 0;
        //    for (int i = 0;i < Height; i++)
        //    {
        //        for ( int j = 0; j < Width; j++)
        //        {
        //            if (Board[i, j] == landminesInt)
        //            {
        //                count++;
        //            }
        //        }
        //    }
        //    return count;
        //}

    }
}
