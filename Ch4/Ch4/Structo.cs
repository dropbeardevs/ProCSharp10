namespace Ch4;

struct Structo
{
    private int Number;

    public Structo() => this.Number = 100;

    public void SetNumber(int num) => this.Number = num;

    public int GetNumber() => this.Number;
}

public class Yo
{
    public static void Test()
    {
        Structo testStruct = new();
        //testStruct.Number = 100;
        testStruct.SetNumber(3);
        int num = testStruct.GetNumber();
        Console.WriteLine($"TEST: {num}");
    }
}