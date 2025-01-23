using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Demos.DelegatesAndEvents;

namespace Demos.Parallel;

static class Runner
{
  public static async void RunMe()
  {
    var winClient = new WpfClient();
    winClient.ShowDialog();
  }
}
