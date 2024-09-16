Namespace Generics
  Module Runner
    Sub RunMe()
      Dim set1 = New WebSettings With {
        .VDir = "c:\temp\test"
      }

      Save(set1, "c:\temp\demo2.dat")

      Dim set2 = DirectCast(Load("c:\temp\demo2.dat"), WebSettings)

      Console.WriteLine(set2.VDir)
    End Sub
  End Module
End Namespace
