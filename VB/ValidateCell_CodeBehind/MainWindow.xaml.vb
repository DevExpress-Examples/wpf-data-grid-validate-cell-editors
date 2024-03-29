Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows

Namespace ValidateCell_CodeBehind

    Public Class Product

        Public Property ProductName As String

        Public Property UnitPrice As Double

        Public Property Discontinued As Boolean

        Public Sub New(ByVal i As Integer)
            ProductName = "Product" & i
            Discontinued = i Mod 5 = 0
        End Sub
    End Class

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Me.grid.ItemsSource = GetProductList().ToList()
        End Sub

        Private Shared Function GetProductList() As IEnumerable(Of Product)
            Dim r As Random = New Random()
            Return Enumerable.Range(0, 20).[Select](Function(i) New Product(i) With {.UnitPrice = r.Next(1, 50)})
        End Function

        Private Sub OnValidateCell(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
            If Not Equals(e.Column.FieldName, NameOf(Product.UnitPrice)) OrElse Not CType(e.Row, Product).Discontinued Then Return
            Dim cellValue = CDbl(e.CellValue)
            Dim discount = 100 - Convert.ToDouble(e.Value) / cellValue * 100
            If discount > 0 AndAlso discount <= 30 Then Return
            e.IsValid = False
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical
            e.ErrorContent = If(discount < 0, $"The price cannot be greater than {cellValue}", $"The discount cannot be greater than 30% ({cellValue * 0.7}). Please correct the price.")
        End Sub
    End Class
End Namespace
