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
        // GET
        public IActionResult Play() {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Play(Board board) {
            Console.WriteLine($"Width: {board.Width}, Height: {board.Height}");
            if (ModelState.IsValid) {
                Board newBoard = new Board(board.Width, board.Height);
                HttpContext.Session.SetInt32("width", board.Width);
                HttpContext.Session.SetInt32("height", board.Height);
                HttpContext.Session.SetInt32("TotalBomb", board.getLandmines());

                newBoard.Clear();

                return View(newBoard);
            }
            return RedirectToAction("Index","Home");
        }

        // first clicks, it will generate the board and store in session (cookie) for the other click
        [HttpPost]
        public IActionResult FirstClick([FromBody] Coordinate coordinate) {
            int x = coordinate.X;
            int y = coordinate.Y;

            int width = (int)HttpContext.Session.GetInt32("width");
            int height = (int)HttpContext.Session.GetInt32("height");
            Board board = new Board(width, height);

            HashSet<KeyValuePair<int[], int>> openList = new HashSet<KeyValuePair<int[], int>>();

            board.Generate(x, y);
            
            int[] boardArr = Utilities.convertToOneD(board.BoardArr);
            HttpContext.Session.Set("boardArr", boardArr);
            openList = board.Open(x, y);

            return Json(openList);
        }

        [HttpPost]
        public IActionResult Click([FromBody] Coordinate coordinate) {
            int x = coordinate.X;
            int y = coordinate.Y;

            int width = (int)HttpContext.Session.GetInt32("width");
            int height = (int)HttpContext.Session.GetInt32("height");
            Board board = new Board(width, height);

            HashSet<KeyValuePair<int[], int>> openList = new HashSet<KeyValuePair<int[], int>>();

            int[] boardArr1D = HttpContext.Session.Get<int[]>("boardArr");

            int[,] boardArr = Utilities.convertToTwoD(boardArr1D, width, height);
            board.BoardArr = boardArr;
            openList = board.Open(x, y);

            return Json(openList);
        }

        [HttpGet]
        public IActionResult GetTotalMines() {
            int landmines = (int)HttpContext.Session.GetInt32("TotalBomb");
            return Json(landmines);
        }
    }
}
