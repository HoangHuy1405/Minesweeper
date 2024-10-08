namespace MinesweeperWeb.Services {
    public static class Utilities {
        public static int[] convertToOneD(int[,] boardArr) {
            int[] arr = new int[boardArr.GetLength(0) * boardArr.GetLength(1)];
            for (int i = 0; i < boardArr.GetLength(0); i++) {
                for (int j = 0; j < boardArr.GetLength(1); j++) {
                    arr[i * boardArr.GetLength(1) + j] = boardArr[i, j];
                }
            }
            return arr;
        }

        public static int[,] convertToTwoD(int[] boardArr, int width, int height) {
            int[,] board = new int[height, width];
            for (int i = 0; i < boardArr.Length; i++) {
                board[i / width, i % width] = boardArr[i];
            }
            return board;
        }

    }
}
