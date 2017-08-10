using System;

static class Program
{
    private static void Main()
    {
        ListNode<int> list = new ListNode<int>
        {
            Data = 1,
            Next = new ListNode<int>
            {
                Data = 2,
                Next = new ListNode<int>
                {
                    Data = 3
                }
            }
        };

        PrintList(list);
        list = ReverseInPlace(list);
        PrintList(list);
    }

    private static ListNode<T> ReverseInPlace<T>(ListNode<T> root)
    {
        if (root == null || root.Next == null)
        {
            return null;
        }

        ListNode<T> previous = null;
        ListNode<T> current = root;
        
        do
        {
            ListNode<T> next = current.Next;
            current.Next = previous;
            previous = current;
            current = next;
        }
        while (current != null);

        return previous;
    }

    private static void PrintList<T>(ListNode<T> root)
    {
        while (root != null)
        {
            Console.Write($"{root.Data} ");
            root = root.Next;
        }

        Console.WriteLine();
    }
}
