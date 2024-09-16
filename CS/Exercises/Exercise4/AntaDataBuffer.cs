using System.Collections;

namespace Exercises.Exercise4;

class AntaDataBuffer<T> : ICollection<T>
{
  private List<T> _rows = new List<T>();
  private Func<T, Guid> _idSelector;

  public AntaDataBuffer(Func<T, Guid> idSelector)
  {
    _idSelector = idSelector;
  }

  public void Add(T item)
  {
    _rows.Add(item);
  }

  public void Clear()
  {
    _rows.Clear();
  }

  public bool Contains(T item)
  {
    return _rows.Contains(item);
  }

  public void CopyTo(T[] array, int arrayIndex)
  {
    _rows.CopyTo(array, arrayIndex);
  }

  public int Count
  {
    get
    {
      return _rows.Count;
    }
  }

  public bool IsReadOnly
  {
    get
    {
      return false;
    }
  }

  public bool Remove(T item)
  {
    return _rows.Remove(item);
  }

  public IEnumerator<T> GetEnumerator()
  {
    return _rows.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return _rows.GetEnumerator();
  }

  public T? this[Guid index]
  {
    get
    {
      foreach (var row in _rows)
      {
        if (_idSelector(row) == index)
        {
          return row;
        }
      }

      return default;
    }
  }
}
