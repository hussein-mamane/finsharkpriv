namespace FinShark.Controllers;
//helpers can and must be in their own class lib
public static class Helper


/*
https://stack247.wordpress.com/2017/09/20/why-must-extension-methods-be-declared-in-a-static-class/
Why Must Extension Methods Be Declared in a Static Class?
    Posted on September 20, 2017 by stack247
Some possible answers:

The purpose of Extension Methods is to make LINQ work and LINQ only needs extension methods to be in a static, non-generic, non-nested class.
Easier for compiler to work with. Compiler and VS’s Intellisense can easily locate the methods.
Grouping the helper methods together.*/
{
    //extension method on IEnumerable 
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        // source.ThrowIfNull("source"); //extension method on source
        // action.ThrowIfNull("action"); //extension method on action
        if (source == null || action == null) throw(new NullReferenceException
            ("source or action is null in linq foreach"));
        foreach (T element in source)
        {
            action(element);
        }
    }
}