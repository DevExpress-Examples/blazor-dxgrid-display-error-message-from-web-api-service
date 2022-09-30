# Grid for Blazor - How to display an error message from the Web API Service error

This example demonstrates how to display an error message from the Web API Service when the criteria is not met during editing. 

![image](https://user-images.githubusercontent.com/69251191/193251341-5e538b39-7308-43db-9e52-d6a77966232f.png)

In the Web API Service controller, return a bad request error when the edited value does not meet the criteria.

```cs
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
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

In the razor page, handle the [DxGrid.EditModelSaving](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGrid.EditModelSaving) event to get the error message from the Web API service. Then, set the the event's `e.Cancel` property to "true".

```cs
    private string MyErrorMessage { get; set; }
    async Task Grid_EditModelSavingHandler(GridEditModelSavingEventArgs e)
    {
        MyErrorMessage = "";
        HttpResponseMessage reply;
        string content = JsonSerializer.Serialize<Products>(e.EditModel as Products);
        StringContent httpContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
        if (e.IsNew == false)
        {
            reply = await Client.PutAsync("https://localhost:5001/api/Products/" + (e.EditModel as Products).productId, httpContent);

        }
        else
        {
            HttpClient client = new HttpClient();
            reply = await Client.PostAsync("https://localhost:5001/api/Products", httpContent);
        }
        if (reply.IsSuccessStatusCode)
        {
            var stream = await Client.GetStreamAsync("https://localhost:5001/api/Products");
            Products = await JsonSerializer.DeserializeAsync
                    <List<Products>>(stream);
        }
        else
        {
            e.Cancel = true;
            MyErrorMessage = await reply.Content.ReadAsStringAsync();
        }
    }
```

Display the error message in the EditFormTemplate.

```razor
    <EditFormTemplate>
        @{
            Products prod = context.EditModel as Products;
        }
        <div>Name</div>
        <DxTextBox @bind-Text="@((context.EditModel as Products).productName)"></DxTextBox>
        <div>Price</div>
        <DxSpinEdit @bind-Value="@((context.EditModel as Products).unitPrice)"></DxSpinEdit>
        <div>Category</div>
        <DxSpinEdit @bind-Value="@((context.EditModel as Products).categoryId)"></DxSpinEdit>
        <div>Discontinued</div>
        <DxCheckBox @bind-Checked="@((context.EditModel as Products).discontinued)"></DxCheckBox>
        <div style="color:red">@MyErrorMessage</div>
    </EditFormTemplate>
```

## Files to Look At

- link.cs (VB: link.vb)
- link.js
- ...

## Documentation

- link
- link
- ...

## More Examples

- link
- link
- ...
