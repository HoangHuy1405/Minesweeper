// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


var gridItems = document.querySelectorAll(".grid-item");


// call the function generateBoard to get initial board stored into a var
var board = [
    [9,1,0,0,0,0,0,0],
    [1,1,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0],
]
let bombInt = 9;
// function that populate the grid container based on var board
console.log(board);
function print() {

}
// click a cell and reveal the closed cell along with it data?
    // click

    // check if grid item is a closed cell, which can only be clicked on
gridItems.forEach((gridItem, index) => {
    if (gridItem.classList.contains("closed")) {
        gridItem.addEventListener("click", function () {
            gridItem.classList.remove("closed");
            // clickable
            var x = Math.floor(index / board.length);
            var y = index % board.length; 

            if (board[x][y] == 9) {
                gridItem.classList.add("mine");
            } else if (board[x][y] == 0) {
                gridItem.classList.add("opened");
            } else {
                gridItem.classList.add("opened");
                gridItem.innerHTML = board[x][y];
            }
            
            
        })
    }
}) 


