using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinesweeperWeb.Models;
using MinesweeperWeb.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MinesweeperWeb.Controllers
{
    public class GameController : Controller {

        public IActionResult Index() {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Play(GridSize size) {
            if (ModelState.IsValid) {
                Board newBoard = new(size);

                //HttpContext.Session.SetInt32("width", board.Width);
                //HttpContext.Session.SetInt32("height", board.Height);
                //HttpContext.Session.SetInt32("TotalBomb", newBoard.getLandmines());

                string boardJson = newBoard.MSBoardToJson();
                HttpContext.Session.SetString("board", boardJson);

                return View(newBoard);
            }
            return RedirectToAction("Index","Home");
        }

        // first clicks, it will generate the board and store in session (cookie) for the other click
        [HttpPost]
        public IActionResult FirstClick([FromBody] Coordinate coordinate)
        {
            //int x = coordinate.X;
            //int y = coordinate.Y;

            //int width = (int)HttpContext.Session.GetInt32("width");
            //int height = (int)HttpContext.Session.GetInt32("height");
            //Board board = new Board(width, height);


            //HashSet<KeyValuePair<int[], int>> openList = new HashSet<KeyValuePair<int[], int>>();
            string boardJson = HttpContext.Session.GetString("board");
            Board board = Board.MSBoardFromJson(boardJson);
            board.Generate(coordinate);
            HttpContext.Session.SetString("board", board.MSBoardToJson());

            //int[] boardArr = Utilities.convertToOneD(board.BoardArr);
            //HttpContext.Session.Set("boardArr", boardArr);
            //openList = board.Open(x, y);

            Dictionary<Coordinate, int> openListWithValue = board.ListOfCoordinateToOpenWithValue(coordinate);
            var openListWithValueJson = Board.GetVarOfDictionary(openListWithValue);

            return Json(openListWithValueJson);
        }

        [HttpPost]
        public IActionResult Click([FromBody] Coordinate coordinate)
        {
            //int x = coordinate.X;
            //int y = coordinate.Y;

            //int width = (int)HttpContext.Session.GetInt32("width");
            //int height = (int)HttpContext.Session.GetInt32("height");
            //Board board = new Board(width, height);

            //HashSet<KeyValuePair<int[], int>> openList = new HashSet<KeyValuePair<int[], int>>();

            //int[] boardArr1D = HttpContext.Session.Get<int[]>("boardArr");

            //int[,] boardArr = Utilities.convertToTwoD(boardArr1D, width, height);
            //board.BoardArr = boardArr;
            //openList = board.Open(x, y);

            Board board = Board.MSBoardFromJson(HttpContext.Session.GetString("board"));

            Dictionary<Coordinate, int> openListWithValue = board.ListOfCoordinateToOpenWithValue(coordinate);
            var openListWithValueJson = Board.GetVarOfDictionary(openListWithValue);

            return Json(openListWithValueJson);
        }

        [HttpGet]
        public IActionResult GetTotalMines() {
            //int landmines = (int)HttpContext.Session.GetInt32("TotalBomb");
            Board board = Board.MSBoardFromJson(HttpContext.Session.GetString("board"));
            int landmines = board.getLandmines();

            return Json(landmines);
        }

        
    }
}
