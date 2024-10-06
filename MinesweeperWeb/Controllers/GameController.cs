using Microsoft.AspNetCore.Mvc;
using MinesweeperWeb.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MinesweeperWeb.Controllers {
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
                newBoard.Generate(3,4);

                // Store the board in temp
                TempData["Board"] = JsonConvert.SerializeObject(newBoard);

                return View(newBoard);
            }
            return RedirectToAction("Index","Home");
        }

        private int clicks = 0;
        [HttpPost]
        public IActionResult Click([FromBody] Coordinate coordinate) {
            clicks++;
            int x = coordinate.X;
            int y = coordinate.Y;

            HashSet <KeyValuePair<int, int>> openList = new HashSet<KeyValuePair<int, int>>();
            Board tempBoard = JsonConvert.DeserializeObject<Board>(TempData["Board"].ToString());
            Board board = new Board(tempBoard.Width, tempBoard.Height);
            if (clicks <= 1) {
                board.Generate(x, y);
                openList = board.Open(x, y);
            } else {
                openList = board.Open(x, y);
            }

            return Json(openList);
        }
    }
}
