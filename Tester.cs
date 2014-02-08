using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class Tester
{
    public static void Main()
    {
    	int count = 10000;
    	{
    		Stopwatch sw = Stopwatch.StartNew();
        	IgushArray<int> array = new IgushArray<int>(500);
        	for (int i = 0; i < count; i++)
        	{
        		array.Add(i);
        	}
        	for (int i = 0; i < count; i++)
        	{
        		array.Insert(i, i * 10);
        	}
        	for (int i = 0; i < count; i++)
        	{
        		array.RemoveAt(i);
        	}
        	Console.WriteLine("IgushArray: " + sw.ElapsedMilliseconds + "ms");
        	sw.Stop();
    	}
    	{
    		Stopwatch sw = Stopwatch.StartNew();
    		List<int> array = new List<int>(count);
        	for (int i = 0; i < count; i++)
        	{
        		array.Add(i);
        	}
        	for (int i = 0; i < count; i++)
        	{
        		array.Insert(i, i * 10);
        	}
        	for (int i = 0; i < count; i++)
        	{
        		array.RemoveAt(i);
        	}
        	Console.WriteLine("List: " + sw.ElapsedMilliseconds + "ms");
        	sw.Stop();
    	}
    }
}
