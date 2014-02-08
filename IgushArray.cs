using System;

/*
--------------------------------------------------------------------------------
Declarations of original IgushArray realization
--------------------------------------------------------------------------------
http://igushev.ru/papers/igusharray/
Эдуард Игушев
IgushArray (быстрый массив)
--------------------------------------------------------------------------------
Загрузка. Гарантия и лицензия

Реализация поставляется “как есть”.
Любое некоммерческое и коммерческое использование разрешено.
Сохранение оригинального имени и ссылку на источник обязательно.
Любой отзыв желателен :-)
------------------------------------------------------------------------------
*/

public class IgushArray<T>
{
	public class Block
	{
		public int count = 0;
		
		private readonly int size;
		private readonly T[] array;
		private int offset = 0;
		
		public Block(int size)
		{
			this.size = size;
			array = new T[size];
		}
		
		public T Insert(int index, T value)
		{
			if (count < size)
				count++;
			T removed = array[(size + offset - 1) % size];
			int i = (index + offset) % size;
			if (i < offset)
			{
				Array.Copy(array, i, array, i + 1, offset - i - 1);
			}
			else
			{
				T last = array[size - 1];
				Array.Copy(array, i, array, i + 1, size - i - 1);
				if (offset > 0)
				{
					Array.Copy(array, 0, array, 1, offset - 1);
					array[0] = last;
				}
			}
			array[i] = value;
			return removed;
		}
		
		public T Insert(T value)
		{
			if (count < size)
				count++;
			offset = (size + offset - 1) % size;
			T removed = array[offset];
			array[offset] = value;
			return removed;
		}
		
		public void Remove(int index)
		{
			count--;
			int i = (index + offset) % size;
			if (i < offset)
			{
				Array.Copy(array, i + 1, array, i, offset - i - 1);
			}
			else
			{
				T last = array[0];
				Array.Copy(array, i + 1, array, i, size - i - 1);
				if (offset > 0)
				{
					Array.Copy(array, 1, array, 0, offset - 1);
					array[size - 1] = last;
				}
			}
			array[(size + offset - 1) % size] = default(T);
		}
		
		public void Remove()
		{
			count--;
			array[offset] = default(T);
			offset = (size + offset + 1) % size;
		}
		
		public T this[int index]
		{
			get { return array[(index + offset) % size]; }
			set { array[(index + offset) % size] = value; }
		}
		
		public T[] ToArray()
		{
			T[] array = new T[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this[i];
			}
			return array;
		}
	}

	private int blockSize;
	private int blocksCount;
	private Block[] blocks;
	
	public IgushArray(int blockSize)
	{
		this.blockSize = blockSize;
		blocksCount = 0;
		blocks = new Block[4];
	}

	private int count = 0;
	public int Count { get { return count; } }
	
	public T this[int index]
	{
		get
		{
			int i = index / blockSize;
			return blocks[i][index - i * blockSize];
		}
		set
		{
			int i = index / blockSize;
			blocks[i][index - i * blockSize] = value;
		}
	}
	
	public void Insert(int index, T value)
	{
		count++;
		int i = index / blockSize;
		int j = index - i * blockSize;
		if ((count - 1) / blockSize >= blocksCount)
		{
			AllocateBlocks(blocksCount + 1);
			Block block = new Block(blockSize);
			blocks[blocksCount - 1] = block;
		}
		T last = blocks[i].Insert(j, value);
		for (; i < blocksCount - 1; i++)
		{
			Block blockI = blocks[i + 1];
			last = blockI.Insert(last);
		}
	}
	
	public void RemoveAt(int index)
	{
		count--;
		int i = index / blockSize;
		int j = index - i * blockSize;
		blocks[i].Remove(j);
		for (int ii = i + 1; ii < blocksCount; ii++)
		{
			blocks[ii - 1][blockSize - 1] = blocks[ii][0];
			blocks[ii].Remove();
		}
		if (count % blockSize == 0)
		{
			blocksCount--;
			if (blocksCount <= blocks.Length / 4 && blocks.Length > 4)
			{
				Block[] newBlocks = new Block[blocks.Length / 2];
				Array.Copy(blocks, 0, newBlocks, 0, newBlocks.Length);
				blocks = newBlocks;
			}
		}
	}
	
	public void Add(T value)
	{
		int i = count / blockSize;
		int j = count - i * blockSize;
		count++;
		Block block;
		if (i >= blocksCount)
		{
			AllocateBlocks(blocksCount + 1);
			block = new Block(blockSize);
			blocks[blocksCount - 1] = block;
		}
		else
		{
			block = blocks[i];
		}
		block[j] = value;
	}
	
	private void AllocateBlocks(int blocksCount)
	{
		this.blocksCount = blocksCount;
		if (blocksCount > blocks.Length)
		{
			Block[] newBlocks = new Block[blocks.Length * 2];
			Array.Copy(blocks, 0, newBlocks, 0, blocks.Length);
			blocks = newBlocks;
		}
	}
	
	public T[] ToArray()
	{
		T[] array = new T[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = this[i];
		}
		return array;
	}
	
	public int BlocksCapacity { get { return blocks.Length; } }
	public int BlocksCount { get { return blocksCount; } }
}
