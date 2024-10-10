using Microsoft.AspNetCore.Razor.Language.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MinesweeperWeb.Models {
    public class Board {
        [Required(ErrorMessage = "Width is required")]
        [Range(4, 30, ErrorMessage = "Rows must be between 8 and 30")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Range(4, 30, ErrorMessage = "Columns must be between 8 and 30")]
        public int Width { get; set; }

        public int intialX { get; set; }
        public int intialY {  get; set; }


        /*
         * 0 present for empty
         * landminesInt present for the number of the landmines
         */
        public int[,] BoardArr { get; set; }
        public int Landmines { get; }
        private int LandminesInt = -8;
        public int getLandminesInt() { return LandminesInt; }
        float difficulty = 0.15f;

        
        public Board() {
            this.Landmines = (int)Math.Round(difficulty * 8 * 8);

            BoardArr = new int[8, 8];
        }
        public Board(int width, int height) {
            this.Height = height;
            this.Width = width;
            this.Landmines = (int)Math.Round(difficulty * this.Height * this.Width);
            BoardArr = new int[height, width];
        }
        
        public int getLandmines() {
            return (int)Math.Round(difficulty * this.Height * this.Width);
        }

        public void Clear() {
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    BoardArr[i, j] = 0;
                }
            }
        }
        public void Generate(int x, int y) {
            Clear();
            PlantMines(x, y);
        }
        private void PlantMines(int x, int y) {
            int range = Height * Width;

            List<int> coordinate = Enumerable.Range(0, range).ToList();
            coordinate.RemoveAt(x*Width + y);
            range--;
            Random rand = new Random();

            for (int i = 0; i < Landmines; i++) {
                int index = rand.Next(range--);

                int row = coordinate[index] / Width;
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
        public HashSet<KeyValuePair<int[], int>> Open(int x, int y) {
            HashSet<KeyValuePair<int, int>> cellsToBeChecked = new HashSet<KeyValuePair<int, int>>();
            HashSet<KeyValuePair<int, int>> cellsChecked = new HashSet<KeyValuePair<int, int>>();
            HashSet<KeyValuePair<int[], int>> openList = new HashSet<KeyValuePair<int[], int>>();
            
            if (BoardArr[x,y] != 0) {
                openList.Add(new KeyValuePair<int[], int>(new int[] { x, y }, BoardArr[x, y]));
                return openList;
            } else {
                //rows
                cellsToBeChecked.Add(new KeyValuePair<int, int>(x, y));
                while (cellsToBeChecked.Count > 0) {
                    var cell = cellsToBeChecked.First();
                    

                    int _x = cell.Key;
                    int _y = cell.Value;
                    
                    openList.Add(new KeyValuePair<int[], int>(new int[] { _x, _y }, BoardArr[_x, _y]));
                    if (BoardArr[_x, _y] != 0) {
                        cellsToBeChecked.Remove(cell);
                        continue;
                    }
                    for (int i = _x - 1; i <= _x+1; i++) {
                        for (int j = _y - 1; j <= _y + 1; j++) {
                            if (i >= Height || i < 0 || j >= Width || j < 0 || (i == _x && j == _y)) continue;
                            if(!cellsChecked.Contains(cell)) cellsToBeChecked.Add(new KeyValuePair<int, int>(i, j));
                            
                        }
                    }
                    cellsChecked.Add(cell);
                    cellsToBeChecked.Remove(cell);
                }
            }

            return openList;
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
