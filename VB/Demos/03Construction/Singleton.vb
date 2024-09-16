Namespace Construction
  Class Singleton
    Shared ReadOnly _instance As New Singleton()

    Private Sub New()
      SingletonCounter = 2
    End Sub

    Public Shared ReadOnly Property Current() As Singleton
      Get
        Return _instance
      End Get
    End Property

    Public Property SingletonCounter() As Integer
  End Class
End Namespace
