using System;
using NUnit.Framework;

[TestFixture]
public class IgushArrayTest
{	
	private IgushArray<string> array;
	
	private string[] Strings(params string[] args)
	{
		return args;
	}
	
	private void Init(int blockSize)
	{
		array = new IgushArray<string>(blockSize);
	}
	
	[Test]
	public void Add()
	{
		Init(3);
		
		Assert.AreEqual(0, array.Count);
		array.Add("a");
		Assert.AreEqual(1, array.Count);
		Assert.AreEqual("a", array[0]);
		
		array.Add("b");
		array.Add("c");
		array.Add("d");
		Assert.AreEqual(4, array.Count);
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d"), Strings(array[0], array[1], array[2], array[3]));
		
		array.Add("e");
		array.Add("f");
		array.Add("g");
		Assert.AreEqual(7, array.Count);
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d", "e", "f", "g"), Strings(array[0], array[1], array[2], array[3], array[4], array[5], array[6]));
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d", "e", "f", "g"), array.ToArray());
	}
	
	[Test]
	public void Insert_InsideBlock()
	{
		Init(3);
		array.Add("a");
		array.Add("c");
		CollectionAssert.AreEqual(Strings("a", "c"), array.ToArray());
		array.Insert(1, "b");
		CollectionAssert.AreEqual(Strings("a", "b", "c"), array.ToArray());
		
		Init(3);
		array.Add("b");
		array.Add("c");
		CollectionAssert.AreEqual(Strings("b", "c"), array.ToArray());		
		array.Insert(0, "a");
		CollectionAssert.AreEqual(Strings("a", "b", "c"), array.ToArray());
		
		Init(3);
		array.Add("a");
		array.Add("b");
		CollectionAssert.AreEqual(Strings("a", "b"), array.ToArray());		
		array.Insert(2, "c");
		CollectionAssert.AreEqual(Strings("a", "b", "c"), array.ToArray());
	}
	
	[Test]
	public void Insert_WithoutMoveEndItem()
	{
		Init(3);
		array.Add("a");
		array.Add("b");
		array.Add("c");
		CollectionAssert.AreEqual(Strings("a", "b", "c"), array.ToArray());
		array.Insert(3, "d");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d"), array.ToArray());
		array.Insert(3, "e");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "e", "d"), array.ToArray());
		array.Insert(4, "f");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "e", "f", "d"), array.ToArray());
		
		Init(3);
		array.Add("a");
		array.Add("b");
		array.Add("c");
		array.Add("d");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d"), array.ToArray());
		array.Insert(4, "e");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d", "e"), array.ToArray());
		array.Insert(3, "f");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "f", "d", "e"), array.ToArray());
		
		Init(3);
		array.Add("a");
		array.Add("b");
		array.Add("c");
		array.Add("d");
		array.Add("e");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d", "e"), array.ToArray());
		array.Insert(5, "f");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d", "e", "f"), array.ToArray());
	}
	
	[Test]
	public void Insert_WithMoveEndItem0()
	{
		Init(3);
		array.Add("a");
		array.Add("b");
		array.Add("c");
		CollectionAssert.AreEqual(Strings("a", "b", "c"), array.ToArray());
		array.Insert(0, "d");
		CollectionAssert.AreEqual(Strings("d", "a", "b", "c"), array.ToArray());
		array.Insert(1, "e");
		CollectionAssert.AreEqual(Strings("d", "e", "a", "b", "c"), array.ToArray());
		array.Insert(2, "f");
		CollectionAssert.AreEqual(Strings("d", "e", "f", "a", "b", "c"), array.ToArray());
	}
	
	[Test]
	public void Insert_WithMoveEndItem1()
	{
		Init(3);
		array.Add("1");
		array.Add("2");
		array.Add("3");
		array.Add("11");
		array.Add("12");
		array.Add("13");
		array.Add("21");
		array.Add("22");
		CollectionAssert.AreEqual(Strings("1", "2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.Insert(0, "a");
		CollectionAssert.AreEqual(Strings("a", "1", "2", "3", "11", "12", "13", "21", "22"), array.ToArray());
	}
	
	[Test]
	public void Insert_WithMoveEndItem2()
	{
		Init(3);
		array.Add("1");
		array.Add("2");
		array.Add("3");
		array.Add("11");
		array.Add("12");
		array.Add("13");
		array.Add("21");
		array.Add("22");
		CollectionAssert.AreEqual(Strings("1", "2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.Insert(0, "a");
		CollectionAssert.AreEqual(Strings("a", "1", "2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.Insert(3, "b");
		CollectionAssert.AreEqual(Strings("a", "1", "2", "b", "3", "11", "12", "13", "21", "22"), array.ToArray());
	}
	
	[Test]
	public void Insert_WithMoveEndItem3()
	{
		Init(3);
		array.Add("1");
		array.Add("2");
		array.Add("3");
		array.Add("11");
		array.Add("12");
		array.Add("13");
		array.Add("21");
		array.Add("22");
		CollectionAssert.AreEqual(Strings("1", "2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.Insert(0, "a");
		CollectionAssert.AreEqual(Strings("a", "1", "2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.Insert(3, "b");
		CollectionAssert.AreEqual(Strings("a", "1", "2", "b", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.Insert(4, "c");
		CollectionAssert.AreEqual(Strings("a", "1", "2", "b", "c", "3", "11", "12", "13", "21", "22"), array.ToArray());
	}
	
	[Test]
	public void Remove()
	{
		Init(3);
		array.Add("1");
		array.Add("2");
		array.Add("3");
		array.Add("11");
		array.Add("12");
		array.Add("13");
		array.Add("21");
		array.Add("22");
		CollectionAssert.AreEqual(Strings("1", "2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.RemoveAt(0);
		CollectionAssert.AreEqual(Strings("2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.RemoveAt(2);
		CollectionAssert.AreEqual(Strings("2", "3", "12", "13", "21", "22"), array.ToArray());
		array.RemoveAt(1);
		CollectionAssert.AreEqual(Strings("2", "12", "13", "21", "22"), array.ToArray());
		array.RemoveAt(4);
		CollectionAssert.AreEqual(Strings("2", "12", "13", "21"), array.ToArray());
		array.RemoveAt(2);
		CollectionAssert.AreEqual(Strings("2", "12", "21"), array.ToArray());
		array.RemoveAt(0);
		CollectionAssert.AreEqual(Strings("12", "21"), array.ToArray());
		array.RemoveAt(0);
		CollectionAssert.AreEqual(Strings("21"), array.ToArray());
		array.RemoveAt(0);
		CollectionAssert.AreEqual(Strings(), array.ToArray());
	}
	
	[Test]
	public void RemoveAndInsert()
	{
		Init(3);
		array.Add("1");
		array.Add("2");
		array.Add("3");
		array.Add("11");
		array.Add("12");
		array.Add("13");
		array.Add("21");
		array.Add("22");
		CollectionAssert.AreEqual(Strings("1", "2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.RemoveAt(0);
		CollectionAssert.AreEqual(Strings("2", "3", "11", "12", "13", "21", "22"), array.ToArray());
		array.Insert(2, "a");
		CollectionAssert.AreEqual(Strings("2", "3", "a", "11", "12", "13", "21", "22"), array.ToArray());
		array.Insert(6, "b");
		CollectionAssert.AreEqual(Strings("2", "3", "a", "11", "12", "13", "b", "21", "22"), array.ToArray());
		array.Insert(8, "c");
		CollectionAssert.AreEqual(Strings("2", "3", "a", "11", "12", "13", "b", "21", "c", "22"), array.ToArray());
		array.Insert(10, "d");
		CollectionAssert.AreEqual(Strings("2", "3", "a", "11", "12", "13", "b", "21", "c", "22", "d"), array.ToArray());
		array.RemoveAt(9);
		CollectionAssert.AreEqual(Strings("2", "3", "a", "11", "12", "13", "b", "21", "c", "d"), array.ToArray());
		array.RemoveAt(4);
		CollectionAssert.AreEqual(Strings("2", "3", "a", "11", "13", "b", "21", "c", "d"), array.ToArray());
		array.RemoveAt(3);
		CollectionAssert.AreEqual(Strings("2", "3", "a", "13", "b", "21", "c", "d"), array.ToArray());
		array.RemoveAt(7);
		CollectionAssert.AreEqual(Strings("2", "3", "a", "13", "b", "21", "c"), array.ToArray());
		array.RemoveAt(5);
		CollectionAssert.AreEqual(Strings("2", "3", "a", "13", "b", "c"), array.ToArray());
		array.Insert(3, "f");
		CollectionAssert.AreEqual(Strings("2", "3", "a", "f", "13", "b", "c"), array.ToArray());
		array.RemoveAt(1);
		CollectionAssert.AreEqual(Strings("2", "a", "f", "13", "b", "c"), array.ToArray());
		array.RemoveAt(0);
		CollectionAssert.AreEqual(Strings("a", "f", "13", "b", "c"), array.ToArray());
		array.RemoveAt(2);
		CollectionAssert.AreEqual(Strings("a", "f", "b", "c"), array.ToArray());
		array.RemoveAt(3);
		CollectionAssert.AreEqual(Strings("a", "f", "b"), array.ToArray());
		array.RemoveAt(2);
		CollectionAssert.AreEqual(Strings("a", "f"), array.ToArray());
		array.RemoveAt(1);
		CollectionAssert.AreEqual(Strings("a"), array.ToArray());
		array.RemoveAt(0);
		CollectionAssert.AreEqual(Strings(), array.ToArray());
	}
	
	[Test]
	public void CyclicQueue0()
	{
		IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
		queue.Insert("c");
		queue.Insert("b");
		queue.Insert("a");
		CollectionAssert.AreEqual(Strings("a", "b", "c"), queue.ToArray());
	}
	
	[Test]
	public void CyclicQueue1()
	{
		IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
		queue.Insert("c");
		queue.Insert("b");
		queue.Insert("a");
		CollectionAssert.AreEqual(Strings("a", "b", "c"), queue.ToArray());
		Assert.AreEqual("c", queue.Insert("d"));
		CollectionAssert.AreEqual(Strings("d", "a", "b"), queue.ToArray());
		Assert.AreEqual("b", queue.Insert("e"));
		CollectionAssert.AreEqual(Strings("e", "d", "a"), queue.ToArray());
		Assert.AreEqual("a", queue.Insert("f"));
		CollectionAssert.AreEqual(Strings("f", "e", "d"), queue.ToArray());
	}
	
	[Test]
	public void CyclicQueue2()
	{
		IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
		queue.Insert("c");
		queue.Insert("b");
		queue.Insert(1, "a");
		CollectionAssert.AreEqual(Strings("b", "a", "c"), queue.ToArray());
	}
	
	[Test]
	public void CyclicQueue3()
	{
		{
			IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
			queue.Insert("a");
			queue.Insert(1, "b");
			CollectionAssert.AreEqual(Strings("a", "b"), queue.ToArray());
		}
		{
			IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
			queue.Insert("a");
			queue.Insert(0, "b");
			CollectionAssert.AreEqual(Strings("b", "a"), queue.ToArray());
		}
		{
			IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
			queue.Insert(0, "a");
			CollectionAssert.AreEqual(Strings("a"), queue.ToArray());
			queue.Insert(0, "b");
			CollectionAssert.AreEqual(Strings("b", "a"), queue.ToArray());
			queue.Insert(0, "c");
			CollectionAssert.AreEqual(Strings("c", "b", "a"), queue.ToArray());
		}
		{
			IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
			queue.Insert("b");
			queue.Insert("c");
			queue.Insert(2, "a");
			CollectionAssert.AreEqual(Strings("c", "b", "a"), queue.ToArray());
		}
	}
	
	[Test]
	public void CyclicQueue4()
	{
		IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
		queue.Insert("c");
		queue.Insert("b");
		queue.Insert("a");
		CollectionAssert.AreEqual(Strings("a", "b", "c"), queue.ToArray());
		Assert.AreEqual("c", queue.Insert(0, "d"));
		CollectionAssert.AreEqual(Strings("d", "a", "b"), queue.ToArray());
		Assert.AreEqual("b", queue.Insert(2, "e"));
		CollectionAssert.AreEqual(Strings("d", "a", "e"), queue.ToArray());
		Assert.AreEqual("e", queue.Insert(0, "f"));
		CollectionAssert.AreEqual(Strings("f", "d", "a"), queue.ToArray());
		Assert.AreEqual("a", queue.Insert(1, "g"));
		CollectionAssert.AreEqual(Strings("f", "g", "d"), queue.ToArray());
	}
	
	[Test]
	public void CyclicQueue5()
	{
		IgushArray<string>.Block queue = new IgushArray<string>.Block(5);
		queue.Insert("e");
		queue.Insert("d");
		queue.Insert("c");
		queue.Insert("a");
		CollectionAssert.AreEqual(Strings("a", "c", "d", "e"), queue.ToArray());
		queue.Insert(1, "b");
		CollectionAssert.AreEqual(Strings("a", "b", "c", "d", "e"), queue.ToArray());
		Assert.AreEqual("e", queue.Insert(3, "f"));
		CollectionAssert.AreEqual(Strings("a", "b", "c", "f", "d"), queue.ToArray());
		Assert.AreEqual("d", queue.Insert(2, "g"));
		CollectionAssert.AreEqual(Strings("a", "b", "g", "c", "f"), queue.ToArray());
	}
	
	[Test]
	public void CyclicQueue6()
	{
		IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
		queue.Insert("d");
		queue.Insert("c");
		queue.Insert("a");
		CollectionAssert.AreEqual(Strings("a", "c", "d"), queue.ToArray());
		queue.Remove();
		queue.Remove();
		CollectionAssert.AreEqual(Strings("d"), queue.ToArray());
		queue.Insert(1, "c");
		CollectionAssert.AreEqual(Strings("d", "c"), queue.ToArray());
		queue.Remove();
		CollectionAssert.AreEqual(Strings("c"), queue.ToArray());
		queue.Remove();
		CollectionAssert.AreEqual(Strings(), queue.ToArray());
	}
	
	[Test]
	public void CyclicQueue7()
	{
		IgushArray<string>.Block queue = new IgushArray<string>.Block(3);
		queue.Insert("d");
		queue.Insert("c");
		queue.Insert("a");
		CollectionAssert.AreEqual(Strings("a", "c", "d"), queue.ToArray());
		queue.Remove(0);
		CollectionAssert.AreEqual(Strings("c", "d"), queue.ToArray());
		queue.Remove(1);
		CollectionAssert.AreEqual(Strings("c"), queue.ToArray());
		queue.Remove(0);
		CollectionAssert.AreEqual(Strings(), queue.ToArray());
		
		queue.Insert("d");
		queue.Insert("c");
		CollectionAssert.AreEqual(Strings("c", "d"), queue.ToArray());
		queue.Remove(0);
		CollectionAssert.AreEqual(Strings("d"), queue.ToArray());
		queue.Insert("b");
		queue.Insert("a");
		CollectionAssert.AreEqual(Strings("a", "b", "d"), queue.ToArray());
		queue.Remove(2);
		CollectionAssert.AreEqual(Strings("a", "b"), queue.ToArray());
	}
	
	[Test]
	public void BlocksAllocation_Add()
	{
		Init(3);
		
		Assert.AreEqual(0, array.BlocksCount);
		array.Add("1");
		Assert.AreEqual(1, array.BlocksCount);
		array.Add("2");
		array.Add("3");
		Assert.AreEqual(1, array.BlocksCount);
		array.Add("1");
		Assert.AreEqual(2, array.BlocksCount);
		array.Add("2");
		array.Add("3");
		array.Add("1");
		array.Add("2");
		array.Add("3");
		array.Add("1");
		array.Add("2");
		array.Add("3");		
		CollectionAssert.AreEqual(Strings("1", "2", "3", "1", "2", "3", "1", "2", "3", "1", "2", "3"), array.ToArray());
		Assert.AreEqual(4, array.BlocksCount);
		Assert.AreEqual(4, array.BlocksCapacity);
		
		array.Add("1");
		CollectionAssert.AreEqual(Strings("1", "2", "3", "1", "2", "3", "1", "2", "3", "1", "2", "3", "1"), array.ToArray());
		Assert.AreEqual(5, array.BlocksCount);
		Assert.AreEqual(8, array.BlocksCapacity);

		for (int i = 0; i < 4 * 3 - 1; i++)
		{
			array.Add("x");
		}
		Assert.AreEqual(8, array.BlocksCount);
		Assert.AreEqual(8, array.BlocksCapacity);
		array.Add("1");
		Assert.AreEqual(9, array.BlocksCount);
		Assert.AreEqual(16, array.BlocksCapacity);
	}
	
	[Test]
	public void BlocksAllocation_Insert()
	{
		Init(3);
		
		Assert.AreEqual(0, array.BlocksCount);
		array.Insert(0, "1");// 1
		Assert.AreEqual(1, array.BlocksCount);
		array.Add("2");// 1 2
		array.Insert(0, "3");// 3 1 2
		Assert.AreEqual(1, array.BlocksCount);
		array.Add("1");// 3 1 2 1
		Assert.AreEqual(2, array.BlocksCount);
		array.Add("2");// 3 1 2 1 2
		array.Add("3");// 3 1 2 1 2 3
		array.Insert(2, "1");// 3 1 1 2 1 2 3
		array.Add("2");// 3 1 1 2 1 2 3 2
		array.Insert(5, "3");// 3 1 1 2 1 3 2 3 2
		array.Add("1");// 3 1 1 2 1 3 2 3 2 1
		array.Insert(0, "2");// 2 3 1 1 2 1 3 2 3 2 1
		array.Insert(7, "3");// 2 3 1 1 2 1 3 3 2 3 2 1
		CollectionAssert.AreEqual(Strings("2", "3", "1", "1", "2", "1", "3", "3", "2", "3", "2", "1"), array.ToArray());
		Assert.AreEqual(4, array.BlocksCount);
		Assert.AreEqual(4, array.BlocksCapacity);
		
		array.Add("1");
		CollectionAssert.AreEqual(Strings("2", "3", "1", "1", "2", "1", "3", "3", "2", "3", "2", "1", "1"), array.ToArray());
		Assert.AreEqual(5, array.BlocksCount);
		Assert.AreEqual(8, array.BlocksCapacity);

		for (int i = 0; i < 4 * 3 - 1; i++)
		{
			array.Insert(2, "x");
		}
		Assert.AreEqual(8, array.BlocksCount);
		Assert.AreEqual(8, array.BlocksCapacity);
		array.Insert(7, "1");
		Assert.AreEqual(9, array.BlocksCount);
		Assert.AreEqual(16, array.BlocksCapacity);
	}
	
	[Test]
	public void BlocksRelease_RemoveAt()
	{
		Init(3);
		
		for (int i = 0; i < 4 * 3; i++)
		{
			array.Add(i + "");
		};
		CollectionAssert.AreEqual(Strings("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"), array.ToArray());
		Assert.AreEqual(4, array.BlocksCount);
		Assert.AreEqual(4, array.BlocksCapacity);
		for (int i = 0; i < 4 * 3; i++)
		{
			array.Add(i + "");
		};
		CollectionAssert.AreEqual(Strings("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11",
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"), array.ToArray());
		Assert.AreEqual(8, array.BlocksCount);
		Assert.AreEqual(8, array.BlocksCapacity);
		array.Add("0");
		CollectionAssert.AreEqual(Strings("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11",
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "0"), array.ToArray());
		Assert.AreEqual(9, array.BlocksCount);
		Assert.AreEqual(16, array.BlocksCapacity);
		
		array.RemoveAt(0);
		Assert.AreEqual(8, array.BlocksCount);
		Assert.AreEqual(3 * 8, array.Count);
		for (int i = 0; i < 4 * 3 - 1; i++)
		{
			array.RemoveAt(0);
		}
		Assert.AreEqual(3 * 4 + 1, array.Count);
		Assert.AreEqual(5, array.BlocksCount);
		Assert.AreEqual(16, array.BlocksCapacity);
		
		array.RemoveAt(0);
		Assert.AreEqual(3 * 4, array.Count);
		Assert.AreEqual(4, array.BlocksCount);
		Assert.AreEqual(8, array.BlocksCapacity);
	}
}
