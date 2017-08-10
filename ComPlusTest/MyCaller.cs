using ComPlusTest;

class MyCaller
{
    public static void Main()
    {
        MyComponent component = new MyComponent();

        for (int i = 1; i <= 10; i++)
            component.Call("Calling index: " + i);
    }
}
