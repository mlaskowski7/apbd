using tutorial2.Models;

Container liquidContainer = new LiquidContainer(
    height: 200, 
    tareWeight: 1000, 
    cargoWeight: 0, 
    depth: 500, 
    maxPayload: 5000);
var ship = new Ship(null, 30, 5, 100000);
ship.LoadContainer(liquidContainer);
    