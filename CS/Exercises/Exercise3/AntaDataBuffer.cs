namespace Exercises.Exercise3;

class AntaDataBuffer<T> : ICollection<T>
  where T : AntaRow
{
  private List<T> _rows = new List<T>();

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

  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  {
    return _rows.GetEnumerator();
  }

  public T? this[System.Guid index]
  {
    get
    {
      foreach (var row in _rows)
      {
        if (row.Id.Value == index)
        {
          return row;
        }
      }

      return default(T);
    }
  }
}
