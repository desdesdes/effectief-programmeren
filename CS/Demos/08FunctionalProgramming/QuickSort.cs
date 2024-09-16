namespace Demos.FunctionalProgramming;

class QuickSort<T> where T : IComparable<T>
{
  public IList<T> Sort(IList<T> list)
  {
    var newData = new List<T>(list.Count);

    var pos = 0;

    while (pos < list.Count)
    {
      var item = list[pos];

      for (var index = 0; index <= pos; index++)
      {
        if (index == pos)
        {
          newData.Add(item);
          break;
        }
        else if(item.CompareTo(newData[index]) < 0)
        {
          newData.Insert(index, item);
          break;
        }
      }

      pos++;
    }

    return newData;
  }
}
