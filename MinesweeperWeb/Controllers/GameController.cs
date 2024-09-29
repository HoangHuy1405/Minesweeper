using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using MinesweeperWeb.Models;

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
            if(ModelState.IsValid) {
                
                //Board boardArr = new Board(board.Width, board.Height);
                board.Generate();

                return View(board);
            }
            return View(board);
        }
    }
}
