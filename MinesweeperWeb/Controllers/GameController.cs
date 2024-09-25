using Microsoft.AspNetCore.Mvc;
using MinesweeperWeb.Models;

namespace MinesweeperWeb.Controllers {
    public class GameController : Controller {


        // GET
        public IActionResult Play() {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Play(Board board) {
            if(ModelState.IsValid) {
                ViewBag.ContainerWidth = board.Columns;
                ViewBag.ContainerHeight = board.Rows;
                return View(board);
            }
            return View(board);
        }
    }
}
