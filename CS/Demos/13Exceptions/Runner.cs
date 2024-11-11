using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Exceptions;

internal class Runner
{
  public static void RunMe()
  {
    ThrowException();
  }

  private static void ThrowException()
  {
    throw new FileNotFoundException("This is an exception");
  }
}
