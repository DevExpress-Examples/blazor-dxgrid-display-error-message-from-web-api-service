Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.EntityFrameworkCore
Imports MyTestWebService.Models

Namespace MyTestWebService.Controllers

    <Route("api/[controller]")>
    <ApiController>
    Public Class ProductsController
        Inherits ControllerBase

        Private ReadOnly _context As NWINDContext

        Public Sub New(ByVal context As NWINDContext)
            _context = context
        End Sub

        ' GET: api/Products
        <HttpGet>
        Public Async Function GetProducts() As Task(Of ActionResult(Of IEnumerable(Of Products)))
            Return Await _context.Products.ToListAsync()
        End Function

        ' GET: api/Products/5
        <HttpGet("{id}")>
        Public Async Function GetProducts(ByVal id As Integer) As Task(Of ActionResult(Of Products))
            Dim products = Await _context.Products.FindAsync(id)
            If products Is Nothing Then
                Return NotFound()
            End If

            Return products
        End Function

        <HttpGet("getByCategory/{id}")>
        Public Async Function GetProductsByCategoryID(ByVal id As Integer) As Task(Of ActionResult(Of IEnumerable(Of Products)))
            Return Await _context.Products.Where(Function(m) m.CategoryId = id).ToListAsync()
        End Function

        ' PUT: api/Products/5
        <HttpPut("{id}")>
        Public Async Function PutProducts(ByVal id As Integer, ByVal products As Products) As Task(Of IActionResult)
            If id <> products.ProductId Then
                Return BadRequest()
            End If

            If products.UnitPrice < 0 Then
                Return BadRequest("Unit Price cannot be less than 0")
            End If

            _context.Entry(products).State = EntityState.Modified
            Await _context.SaveChangesAsync()
            Return NoContent()
        End Function

        ' POST: api/Products
        <HttpPost>
        Public Async Function PostProducts(ByVal products As Products) As Task(Of ActionResult(Of Products))
            _context.Products.Add(products)
            Await _context.SaveChangesAsync()
            Return CreatedAtAction("GetProducts", New With {.id = products.ProductId}, products)
        End Function

        ' DELETE: api/Products/5
        <HttpDelete("{id}")>
        Public Async Function DeleteProducts(ByVal id As Integer) As Task(Of ActionResult(Of Products))
            Dim products = Await _context.Products.FindAsync(id)
            If products Is Nothing Then
                Return NotFound()
            End If

            _context.Products.Remove(products)
            Await _context.SaveChangesAsync()
            Return products
        End Function
    End Class
End Namespace
