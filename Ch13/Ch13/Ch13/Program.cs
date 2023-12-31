﻿Console.WriteLine("***** Fun with LINQ to Objects *****\n");
QueryOverStrings();
QueryOverStringsWithExtensionMethods();
QueryOverInts();

static void QueryOverStrings()
{
    // Assume we have an array of strings.
    string[] currentVideoGames = {"Morrowind", "Uncharted 2", "Fallout 3", "Daxter", "System Shock 2"};
    
    // Build a query expression to find the items in the array
    // that have an embedded space.
    IEnumerable<string> subset =
    //var subset=
        from g in currentVideoGames
        where g.Contains(" ")
        orderby g
        select g;
    
    ReflectOverQueryResults(subset);
    
    // Print out the results.
    foreach (string s in subset)
    {
        Console.WriteLine("Item: {0}", s);
    }

}


static void QueryOverStringsWithExtensionMethods()
{
    // Assume we have an array of strings.
    string[] currentVideoGames = {"Morrowind", "Uncharted 2", "Fallout 3", "Daxter", "System Shock 2"};

    // Build a query expression to find the items in the array
    // that have an embedded space.
    IEnumerable<string> subset =
        currentVideoGames.Where(
            (g) => g.Contains(' '))
            .OrderBy(g => g)
            .Select(g => g);

    ReflectOverQueryResults(subset, "Extension Methods");
    
    // Print out the results.
    foreach (string s in subset)
    {
        Console.WriteLine("Item: {0}", s);
    }
}

static void ReflectOverQueryResults(object resultSet, string queryType = "Query Expressions")
{
    Console.WriteLine($"***** Info about your query using {queryType} *****");
    Console.WriteLine("resultSet is of type: {0}", resultSet.GetType().Name);
    Console.WriteLine("resultSet location: {0}", resultSet.GetType().Assembly.GetName().Name);
}

static void QueryOverInts()
{
    int[] numbers = {10, 20, 30, 40, 1, 2, 3, 8};
    // Use implicit typing here...
    var subset = from i in numbers where i < 10 select i;
    // ...and here.
    foreach (var i in subset)
    {
        Console.WriteLine("Item: {0} ", i);
    }
    ReflectOverQueryResults(subset);
}