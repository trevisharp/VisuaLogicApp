LogicApp.Run(25, 100);

void logic(VisualArray array)
{
    bubblesort(array);
}

void bubblesort(VisualArray array)
{
    bool sorted = false;
    array[array.Length / 2] = 50;
    while (!sorted)
    {
        sorted = true;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > array[i - 1])
            {
                sorted = false;
                var temp = array[i - 1];
                array[i - 1] = array[i];
                array[i] = temp;
            }
        }
    }
}