using System;

internal static class Program
{
	private static readonly int[] _a = new int[50]; //{7, 4, 3, 1, 9, 6, 2, 5, 8};

	private static void Main()
	{
		Random r = new Random((int) DateTime.Now.Ticks);

		for (int i = 0; i < _a.Length; ++i)
		{
			_a[i] = r.Next(100);
		}

		PrintArray();

		QuickSort(0, _a.Length - 1);

		PrintArray();
	}

	private static void QuickSort(int p, int r)
	{
		if (p >= r)
		{
			return;
		}

		int q = Partition(p, r);

		QuickSort(p, q - 1);
		QuickSort(q + 1, r);
	}

	private static int Partition(int p, int r)
	{
		int x = _a[r];

		int i = p - 1;

		for (int j = p; j < r; ++j)
		{
			if (_a[j] < x)
			{
				Swap(++i, j);
			}
		}

		Swap(++i, r);

		return i;
	}

	private static void Swap(int s, int t)
	{
		int tmp = _a[s];
		_a[s] = _a[t];
		_a[t] = tmp;
	}

	private static void PrintArray()
	{
		Console.WriteLine(String.Join(", ", _a));
	}
}