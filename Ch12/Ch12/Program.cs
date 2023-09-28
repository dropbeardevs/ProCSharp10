//Program.cs
using SimpleDelegate;

Console.WriteLine("***** Simple Delegate Example *****\n");

// Create a BinaryOp delegate object that
// "points to" SimpleMath.Add().
SimpleMath m = new SimpleMath();
BinaryOp b = new BinaryOp(m.Add);

DisplayDelegateInfo(b);

int test = b(10, 10);

BinaryOp c = new(m.Subtract);

int test2 = c(20, 10);
DisplayDelegateInfo(c);

// Invoke Add() method indirectly using delegate object.
Console.WriteLine("10 + 10 is {0}", test);
Console.WriteLine("20 - 10 is {0}", test2);
Console.ReadLine();

static void DisplayDelegateInfo(Delegate delObj)
{
    // Print the names of each member in the
    // delegate's invocation list.
    foreach (Delegate d in delObj.GetInvocationList())
    {
        Console.WriteLine("Method Name: {0}", d.Method);
        Console.WriteLine("Type Name: {0}", d.Target);
    }
}

public delegate int BinaryOp(int x, int y);