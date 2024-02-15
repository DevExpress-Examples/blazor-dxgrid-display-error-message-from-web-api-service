<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/543565561/22.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1118926)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Grid for Blazor - How to display an error message from the Web API Service

This example demonstrates how to display an error message from the Web API Service when the criteria is not met during editing. 

![image](image.png)

In the Web API Service controller, return a bad request error when the edited value does not meet the criteria.

```cs
public async Task<IActionResult> PutProducts(int id, Products products) {
    if (id != products.ProductId) {
        return BadRequest();
    }
    if (products.UnitPrice < 0) {
        return BadRequest("Unit Price cannot be less than 0");
    }
    _context.Entry(products).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return NoContent();
}
```

In the razor page, handle the [DxGrid.EditModelSaving](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGrid.EditModelSaving) event to get the error message from the Web API service. Then, set the event's `e.Cancel` property to `true`.

```cs
private string MyErrorMessage { get; set; }
async Task Grid_EditModelSaving(GridEditModelSavingEventArgs e) {
    MyErrorMessage = null;
    var editedProduct = (Products)e.EditModel;
    var httpContent = ConvertProductToHttpContent(editedProduct);
    var response = e.IsNew == false
        ? await HttpClient.PutAsync(ProductsUrl + editedProduct.ProductId, httpContent)
        : await HttpClient.PostAsync(ProductsUrl, httpContent);
    if (response.IsSuccessStatusCode) 
        Products = await LoadDataAsync();
    else {
        e.Cancel = true;
        MyErrorMessage = await response.Content.ReadAsStringAsync();
    }
}
```

Display the error message in the `EditFormTemplate`.

```razor
<EditFormTemplate>
    @{
        Products prod = context.EditModel as Products;
    }

    <div>Name</div>
    <DxTextBox @bind-Text="prod.ProductName" />

    <div>Price</div>
    <DxSpinEdit @bind-Value="prod.UnitPrice" />

    <div>Category</div>
    <DxSpinEdit @bind-Value="prod.CategoryId" />

    <div>Discontinued</div>
    <DxCheckBox @bind-Checked="prod.Discontinued" />

    @if (String.IsNullOrEmpty(MyErrorMessage) == false) {        
        <div style="color:red">@MyErrorMessage</div>
    }
</EditFormTemplate>
```

## Files to Look At

* [Grid.razor](./CS/DxBlazorApplication1/DxBlazorApplication1/Pages/Grid.razor)
* [ProductsController.cs](./CS/MyTestWebService/MyTestWebService/Controllers/ProductsController.cs)

## Documentation

- [Get Started with Grid - Edit Data](https://docs.devexpress.com/Blazor/403625/grid/get-started-with-grid#edit-data)
- [Tutorial: Create a web API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.2&tabs=visual-studio)
