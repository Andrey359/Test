using System.Linq;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ArrayList
{
    private int[] numbers;
    private int size = 0;
    private object LookN = new object();
    public ArrayList(int capacity)
    {
        numbers = new int[capacity];
    }
    // метод добавления числа в список
    public void add(int x)
    {
        lock (LookN)
        {
            numbers[size++] = x;
        }

    }
    // метод получения элемента списка по индексу
    public int get(int index)
    {
        lock (LookN)
        {
            return numbers[index];
        }
    }
    // метод очистки списка
    public void clear()
    {
        lock (LookN)
        {
            size = 0;
        }
    }
    // метод выдающий размер списка
    public int getSize()
    {
        return size;
    }



}

public class MainClass
{
    static ArrayList f = new ArrayList(1000);

    public static void test()
    {
        for (int i = 0; i < 1000; i++)
        {
            f.add(i);
        }

        for (int i = 0; i < f.getSize(); i++)
        {
            Console.WriteLine(f.get(i));
        }

        f.clear();
    }

    public static void Main()
    {
        Thread f = new(new ThreadStart(test));
        f.Start();
        f.Join();
    }
}

