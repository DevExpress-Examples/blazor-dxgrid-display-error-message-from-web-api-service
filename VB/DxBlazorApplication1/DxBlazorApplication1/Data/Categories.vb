Namespace DxBlazorApplication1.Data

    Public Class Categories

        Public Sub New()
            Products = New HashSet(Of Products)()
        End Sub

        Public Property categoryId As Integer

        Public Property categoryName As String

        Public Property description As String

        Public Overridable Property Products As ICollection(Of Products)
    End Class
End Namespace
