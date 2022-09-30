Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc
Imports MyTestWebService.Models
Imports Microsoft.EntityFrameworkCore

Namespace MyTestWebService.Controllers

    <Route("api/[controller]")>
    <ApiController>
    Public Class CategoriesController
        Inherits ControllerBase

        Private ReadOnly _context As NWINDContext

        Public Sub New(ByVal context As NWINDContext)
            _context = context
        End Sub

        <HttpGet>
        Public Async Function [Get]() As Task(Of ActionResult(Of IEnumerable(Of Categories)))
            Return Await _context.Categories.ToListAsync()
        End Function

        ' GET api/<controller>/5
        <HttpGet("{id}")>
        Public Async Function [Get](ByVal id As Integer) As Task(Of ActionResult(Of Categories))
            Dim todoItem = Await _context.Categories.FindAsync(id)
            If todoItem Is Nothing Then
                Return NotFound()
            End If

            Return todoItem
        End Function

        ' POST api/<controller>
        <HttpPost>
        Public Async Function Post(ByVal item As Categories) As Task(Of ActionResult(Of Categories))
            _context.Categories.Add(item)
            Await _context.SaveChangesAsync()
            Return CreatedAtAction(NameOf([Get]), New With {.id = item.CategoryId}, item)
        End Function

        ' PUT api/<controller>/5
        <HttpPut("{id}")>
        Public Async Function Put(ByVal id As Integer, ByVal item As Categories) As Task(Of IActionResult)
            If id <> item.CategoryId Then
                Return BadRequest()
            End If

            _context.Entry(item).State = EntityState.Modified
            Await _context.SaveChangesAsync()
            Return NoContent()
        End Function

        ' DELETE api/<controller>/5
        <HttpDelete("{id}")>
        Public Async Function Delete(ByVal id As Integer) As Task(Of IActionResult)
            Dim todoItem = Await _context.Categories.FindAsync(id)
            If todoItem Is Nothing Then
                Return NotFound()
            End If

            _context.Categories.Remove(todoItem)
            Await _context.SaveChangesAsync()
            Return NoContent()
        End Function
    End Class
End Namespace
