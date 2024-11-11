using System.Diagnostics;
using System.Text;

namespace Demos.Exceptions;

public abstract class MyException : Exception
{
  private const string _innerExceptionPrefix = " ---> ";

  public MyException() { }
  public MyException(string message) : base(message) { }
  public MyException(string message, Exception inner) : base(message, inner) { }

  override public string ToString()
  {
    string className = GetType().ToString();
    string? message = Message;
    string innerExceptionString = InnerException?.ToString() ?? "";
    string endOfInnerExceptionResource = "--- End of inner exception stack trace ---"; // todo: translate
    string? stackTrace = StackTrace;

    // Calculate result string length
    int length = className.Length;
    checked
    {
      if(!string.IsNullOrEmpty(message))
      {
        length += 2 + message.Length;
      }
      if(InnerException != null)
      {
        length += Environment.NewLine.Length + _innerExceptionPrefix.Length + innerExceptionString.Length + Environment.NewLine.Length + 3 + endOfInnerExceptionResource.Length;
      }
      if(stackTrace != null)
      {
        length += Environment.NewLine.Length + stackTrace.Length;
      }
    }

    // Create the string
    var sb = new StringBuilder(length);

    // Fill it in
    sb.Append(className);
    if(!string.IsNullOrEmpty(message))
    {
      sb.Append(": ");
      sb.Append(message);
    }

    // Allows readers to add their own fields
    var fieldInfoString = BuildFieldInfoString();
    if(fieldInfoString != null)
    {
      sb.AppendLine();
      sb.Append(fieldInfoString);
    }

    if(InnerException != null)
    {
      sb.AppendLine();
      sb.Append(_innerExceptionPrefix);
      sb.Append(innerExceptionString);
      sb.AppendLine();
      sb.Append("   ");
      sb.Append(endOfInnerExceptionResource);
    }

    if(stackTrace != null)
    {
      sb.AppendLine();
      sb.Append(stackTrace);
    }

    Debug.Assert(sb.Length == 0);

    // Return it
    return sb.ToString();
  }

  /// <summary>
  /// Allows derived classes to add their own fields to the exception string.
  /// </summary>
  /// <returns><c>null</c> if no data needs to be added, else a string containing one line per field with the fieldname and data seperated by ": ".</returns>
  protected virtual StringBuilder? BuildFieldInfoString()
  {
    return null;
  }
}
