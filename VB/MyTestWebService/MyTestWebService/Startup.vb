Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports MyTestWebService.Models

Namespace MyTestWebService

    Public Class Startup

        Public Sub New(ByVal configuration As IConfiguration)
            Me.Configuration = configuration
        End Sub

        Public ReadOnly Property Configuration As IConfiguration

        ' This method gets called by the runtime. Use this method to add services to the container.
        Public Sub ConfigureServices(ByVal services As IServiceCollection)
            services.AddDbContext(Of NWINDContext)()
            services.AddControllers()
        End Sub

        ' This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        Public Sub Configure(ByVal app As IApplicationBuilder, ByVal env As IWebHostEnvironment)
            If env.IsDevelopment() Then
                app.UseDeveloperExceptionPage()
            Else
                ' The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts()
            End If

            app.UseHttpsRedirection()
            app.UseRouting()
            app.UseAuthorization()
            app.UseEndpoints(Sub(endpoints) endpoints.MapControllers())
        End Sub
    End Class
End Namespace
