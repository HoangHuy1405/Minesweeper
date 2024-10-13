namespace MinesweeperWeb.Models {
    public class Coordinate {
        public int X {  get; set; }
        public int Y {  get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Coordinate)
                return false;

            Coordinate other = (Coordinate)obj;
            return X == other.X && Y == other.Y;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
