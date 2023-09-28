
// Register with delegate as a lambda expression.
var m = new SimpleMath.SimpleMath();



m.SetMathHandler((msg, result) =>
    {Console.WriteLine("Message: {0}, Result: {1}", msg, result);});

m.SetMathHandler(Handler2);

// This will execute the lambda expression.
m.Add(10, 10);


void Handler2(string msg, int result)
{
    Console.WriteLine("Message2: {0}, Result2: {1}", msg, result.ToString());
}


List<Rectangle> myListOfRects = new List<Rectangle>
{
    new Rectangle {TopLeft = new Point { X = 10, Y = 10 },
        BottomRight = new Point { X = 200, Y = 200}},
    new Rectangle {TopLeft = new Point { X = 2, Y = 2 },
        BottomRight = new Point { X = 100, Y = 100}},
    new Rectangle {TopLeft = new Point { X = 5, Y = 5 },
        BottomRight = new Point { X = 90, Y = 75}}
};