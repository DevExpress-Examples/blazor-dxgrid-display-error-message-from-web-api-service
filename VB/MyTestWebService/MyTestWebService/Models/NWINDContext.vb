Imports Microsoft.EntityFrameworkCore

Namespace MyTestWebService.Models

    Public Partial Class NWINDContext
        Inherits DbContext

        Public Sub New()
        End Sub

        Public Sub New(ByVal options As DbContextOptions(Of NWINDContext))
            MyBase.New(options)
        End Sub

        Public Overridable Property Categories As DbSet(Of Categories)

        Public Overridable Property Products As DbSet(Of Products)

        Protected Overrides Sub OnConfiguring(ByVal optionsBuilder As DbContextOptionsBuilder)
            If Not optionsBuilder.IsConfigured Then
                optionsBuilder.UseSqlServer("Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NWIND;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            End If
        End Sub

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As ModelBuilder)
            modelBuilder.Entity(Of Categories)(Sub(entity)
                entity.HasKey(Function(e) e.CategoryId)
                entity.[Property](Function(e) e.CategoryId).HasColumnName("CategoryID")
                entity.Property(Function(e) e.CategoryName).IsRequired().HasMaxLength(15)
                entity.[Property](Function(e) e.Description).HasColumnType("ntext")
            End Sub)
            modelBuilder.Entity(Of Products)(Sub(entity)
                entity.HasKey(Function(e) e.ProductId)
                entity.[Property](Function(e) e.ProductId).HasColumnName("ProductID")
                entity.[Property](Function(e) e.CategoryId).HasColumnName("CategoryID")
                entity.[Property](Function(e) e.Ean13).HasColumnName("EAN13").HasColumnType("text")
                entity.Property(Function(e) e.ProductName).IsRequired().HasMaxLength(40)
                entity.Property(Function(e) e.QuantityPerUnit).HasMaxLength(20)
                entity.[Property](Function(e) e.SupplierId).HasColumnName("SupplierID")
                entity.[Property](Function(e) e.UnitPrice).HasColumnType("smallmoney")
                entity.HasOne(Function(d) d.Category).WithMany(Function(p) p.Products).HasForeignKey(Function(d) d.CategoryId).HasConstraintName("FK_Products_Categories")
            End Sub)
            OnModelCreatingPartial(modelBuilder)
        End Sub

        Partial Private Sub OnModelCreatingPartial(ByVal modelBuilder As ModelBuilder)
        End Sub
    End Class
End Namespace
