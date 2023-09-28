Console.WriteLine("***** Generic Delegates *****\n");

// Register targets.
//MyGenericDelegate<string> strTarget = new MyGenericDelegate<string>(StringTarget);
MyGenericDelegate<string> strTarget = StringTarget;
strTarget("Some string data");

//Using the method group conversion syntax
MyGenericDelegate<int> intTarget = IntTarget;
intTarget(9);

MyGenericDelegate<double> doubleTarget = new MyGenericDelegate<double>(DoubleTarget);
doubleTarget(100.0002);


static void StringTarget(string arg)
{
    Console.WriteLine("arg in uppercase is: {0}", arg.ToUpper());
}

static void IntTarget(int arg)
{
    Console.WriteLine("++arg is: {0}", ++arg);
}

static void DoubleTarget(double arg)
{
    Console.WriteLine("arg is: {0}", arg);
}

// This generic delegate can represent any method
// returning void and taking a single parameter of type T.
public delegate void MyGenericDelegate<T>(T arg);