using System.Runtime.CompilerServices;

namespace Minesweeper.Models
{
    public class MinesweeperBoard
    {
        /*
         * 0 present for empty
         * landminesInt present for the number of the landmines
         */
        public int[,] Board { get; }
        public int Height {  get; }
        public int Width { get; }
        public int Landmines { get; }
        private int landminesInt = -8;
        public MinesweeperBoard(int height, int width, int landmines)
        {
            this.Height = height;
            this.Width = width;
            this.Landmines = landmines;
            Board = new int[height, width];

            Clear();
        }
        public void Clear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Board[i, j] = 0;
                }
            }
        }
        public void Generate()
        {   
            Clear();
            PlantMines();
        }
        private void PlantMines()
        {
            int range = Height * Width;

            List<int> coordinate = Enumerable.Range(0, range).ToList();
            Random rand = new Random();

            for (int i = 0; i < Landmines; i++)
            {
                int index = rand.Next(range--);

                int row = coordinate[index] / Height;
                int col = coordinate[index] % Width;

                Board[row, col] = landminesInt;
                FillNum(row, col);
                
                coordinate.RemoveAt(index);
            }
        }
        private void FillNum(int row, int col)
        {
            for(int i = row - 1; i <= row + 1; i++)
            {
                // if row go over up and low boundary then skip
                if (i < 0 || i >= Height) continue;

                for( int j = col - 1;  j <= col + 1; j++)
                {
                    //if col go over left and right boundary then skip
                    if (j < 0 || j >= Width) continue;
                    // if landmines then skip
                    if (Board[i, j] == landminesInt) continue;
                    
                    Board[i, j] += 1;
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


