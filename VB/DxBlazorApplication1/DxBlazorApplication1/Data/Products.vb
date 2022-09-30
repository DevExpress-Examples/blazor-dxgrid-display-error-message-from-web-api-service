Namespace DxBlazorApplication1.Data

    Public Class Products

        Public Property productId As Integer

        Public Property productName As String

        Public Property supplierId As Integer?

        Public Property categoryId As Integer?

        Public Property quantityPerUnit As String

        Public Property unitPrice As Decimal?

        Public Property unitsInStock As Short?

        Public Property unitsOnOrder As Short?

        Public Property reorderLevel As Short?

        Public Property discontinued As Boolean

        Public Property ean13 As String

        Public Overridable Property category As Categories
    End Class
End Namespace
