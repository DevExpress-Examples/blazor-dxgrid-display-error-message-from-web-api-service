Imports System.Collections.Generic

Namespace MyTestWebService.Models

    Public Partial Class Categories

        Public Sub New()
            Products = New HashSet(Of Products)()
        End Sub

        Public Property CategoryId As Integer

        Public Property CategoryName As String

        Public Property Description As String

        Public Overridable Property Products As ICollection(Of Products)
    End Class
End Namespace
