using Newtonsoft.Json;

namespace MinesweeperWeb.Models {
    public class Board {
        /*
         * 0 present for empty
         * landminesInt present for the number of the landmines
         */
        #region properties
        public GridSize Size { get; set; }
        public List<Coordinate> BombList { get; set; }
        public int[,] BoardArr { get; set; }
        public int Landmines { get; set; }
        private int LandminesInt = -8;
        public int getLandminesInt() { return LandminesInt; }
        public float Difficulty { get; set; }
        #endregion

        #region constructor
        public Board(GridSize size, float difficulty = 0.15f) {
            Size = size;
            Difficulty = difficulty;
            this.Landmines = (int)Math.Round(difficulty * Size.Height * Size.Width);
            BoardArr = new int[Size.Height, Size.Width];
            BombList = new List<Coordinate>();
        }
        #endregion

        #region functions

        #region get values
        public int getLandmines() {
            return (int)Math.Round(Difficulty * Size.Height * Size.Width);
        }
        public int GetValueFromCoordinate(Coordinate coordinate) => BoardArr[coordinate.X, coordinate.Y];
        #endregion

        #region generate landmines
        public void Clear() {
            for (int i = 0; i < Size.Height; i++) {
                for (int j = 0; j < Size.Width; j++) {
                    BoardArr[i, j] = 0;
                }
            }
        }
        public void Generate(Coordinate coordinate) {
            Clear();
            PlantMines(coordinate);
        }
        private void PlantMines(Coordinate coordinate)
        {
            int range = Size.Height * Size.Width;
            List<int> validCoordinates = Enumerable.Range(0, range).ToList();
            validCoordinates.RemoveAt(coordinate.X * Size.Width + coordinate.Y);
            range--;

            Random rand = new Random();

            for (int i = 0; i < Landmines; i++)
            {
                int index = rand.Next(range--);

                int row = validCoordinates[index] / Size.Width;
                int col = validCoordinates[index] % Size.Width;
                BoardArr[row, col] = LandminesInt;

                Coordinate coor = new Coordinate
                {
                    X = row,
                    Y = col
                };

                BombList.Add(coor);  // Add bomb position to BombList
                Console.WriteLine($"Bomb placed at: ({row}, {col})");  // Log bomb placement
                FillNum(coor);

                validCoordinates.RemoveAt(index);
            }

            // Debug output to check BombList after all bombs are placed
            Console.WriteLine("Final BombList:");
            foreach (var bomb in BombList)
            {
                Console.WriteLine($"Bomb at position ({bomb.X}, {bomb.Y})");
            }
        }

        private void FillNum(Coordinate coordinate) {
            for (int i = coordinate.X - 1; i <= coordinate.X + 1; i++) {
                // if row go over up and low boundary then skip
                if (i < 0 || i >= Size.Height) continue;

                for (int j = coordinate.Y - 1; j <= coordinate.Y + 1; j++) {
                    //if col go over left and right boundary then skip
                    if (j < 0 || j >= Size.Width) continue;
                    // if landmines then skip
                    if (BoardArr[i, j] == LandminesInt) continue;

                    BoardArr[i, j] += 1;
                }
            }
        }
        #endregion

        #region get list of coordinate

        #region to open
        public Dictionary<Coordinate, int> ListOfCoordinateToOpenWithValue(Coordinate coordinate)
        {
            Dictionary<Coordinate, int> keyValuePairs = [];
            List<Coordinate> coordinates = ListOfCoordinateToOpen(coordinate);
            foreach (Coordinate c in coordinates)
            {
                keyValuePairs.Add(c, this.GetValueFromCoordinate(c));
            }

            return keyValuePairs;
        }
        private List<Coordinate> ListOfCoordinateToOpen(Coordinate coordinate)
        {
            List<Coordinate> listToOpen = [];
            listToOpen.Add(coordinate);

            if (GetValueFromCoordinate(coordinate) != 0) return listToOpen; //if that coordinate contain N*, dont need to open their neighbor

            int index = 0;
            do
            {
                Coordinate current = listToOpen[index];

                if (GetValueFromCoordinate(current) != 0) continue; //if the value is N*, dont need to check the neighbor 

                //get neighbors and check. If not exist, add into the list
                List<Coordinate> neigbors = GetNeighbors(current);
                foreach (Coordinate c in neigbors)
                {
                    if (!listToOpen.Contains(c)) listToOpen.Add(c);
                }
            } while (++index < listToOpen.Count);
            return listToOpen;
        }
        private List<Coordinate> GetNeighbors(Coordinate coordinate)
        {
            List<Coordinate> neighbors = [];

            int boardHeight = Size.Height;
            int boardWidth = Size.Width;

            int x = coordinate.X;
            int y = coordinate.Y;

            for (int row = x - 1; row <= x + 1; row++)
            {
                if (row >= boardHeight || row < 0) continue; // out of bound then continue
                for (int col = y - 1; col <= y + 1; col++)
                {
                    if (col >= boardWidth || col < 0) continue; // out of bound then continue

                    Coordinate currentCoordinate = new Coordinate
                    {
                        X = row, 
                        Y = col
                    };

                    if (currentCoordinate.Equals(coordinate)) continue; // not add the current coordinate

                    neighbors.Add(currentCoordinate);// add into neighbors
                }
            }

            return neighbors;
        }
        public HashSet<KeyValuePair<int[], int>> Open(int x, int y)
        {
            HashSet<KeyValuePair<int, int>> cellsToBeChecked = new HashSet<KeyValuePair<int, int>>();
            HashSet<KeyValuePair<int, int>> cellsChecked = new HashSet<KeyValuePair<int, int>>();
            HashSet<KeyValuePair<int[], int>> openList = new HashSet<KeyValuePair<int[], int>>();

            if (BoardArr[x, y] != 0)
            {
                openList.Add(new KeyValuePair<int[], int>(new int[] { x, y }, BoardArr[x, y]));
                return openList;
            }
            else
            {
                //rows
                cellsToBeChecked.Add(new KeyValuePair<int, int>(x, y));
                while (cellsToBeChecked.Count > 0)
                {
                    var cell = cellsToBeChecked.First();


                    int _x = cell.Key;
                    int _y = cell.Value;

                    openList.Add(new KeyValuePair<int[], int>(new int[] { _x, _y }, BoardArr[_x, _y]));
                    if (BoardArr[_x, _y] != 0)
                    {
                        cellsToBeChecked.Remove(cell);
                        continue;
                    }
                    for (int i = _x - 1; i <= _x + 1; i++)
                    {
                        for (int j = _y - 1; j <= _y + 1; j++)
                        {
                            if (i >= Size.Height || i < 0 || j >= Size.Width || j < 0 || (i == _x && j == _y)) continue;
                            if (!cellsChecked.Contains(cell)) cellsToBeChecked.Add(new KeyValuePair<int, int>(i, j));

                        }
                    }
                    cellsChecked.Add(cell);
                    cellsToBeChecked.Remove(cell);
                }
            }

            return openList;
        }
        #endregion

        #region already open
        #endregion

        #endregion

        #region convert
        public string MSBoardToJson()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
        public static Board MSBoardFromJson(string json)
        {
            Board msboard = null;
            if (!string.IsNullOrEmpty(json))
            {
                msboard = JsonConvert.DeserializeObject<Board>(json);
            }
            return msboard;
        }
        public static object GetVarOfDictionary(Dictionary<Coordinate, int> dictionary)
        {
            var jsonArray = dictionary.Select(item => new
            {
                x = item.Key.X,
                y = item.Key.Y,
                value = item.Value
            });

            return jsonArray;
        }
        #endregion

        #endregion
    }
}
