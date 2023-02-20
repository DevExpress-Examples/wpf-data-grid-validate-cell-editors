Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace ValidateCell_MVVM

    Public Class Product

        Public Property ProductName As String

        Public Property UnitPrice As Double

        Public Property Discontinued As Boolean

        Public Sub New(ByVal i As Integer)
            ProductName = "Product" & i
            Discontinued = i Mod 5 = 0
        End Sub
    End Class

    Public Class MainViewModel
        Inherits ViewModelBase

        Public ReadOnly Property ProductList As ObservableCollection(Of Product)

        Public Sub New()
            ProductList = New ObservableCollection(Of Product)(GetProductList())
        End Sub

        Private Shared Function GetProductList() As IEnumerable(Of Product)
            Dim r As Random = New Random()
            Return Enumerable.Range(0, 20).[Select](Function(i) New Product(i) With {.UnitPrice = r.Next(1, 50)})
        End Function

        <Command>
        Public Sub ValidateCell(ByVal args As RowCellValidationArgs)
            If Not Equals(args.FieldName, NameOf(Product.UnitPrice)) OrElse Not CType(args.Item, Product).Discontinued Then Return
            Dim cellValue = CDbl(args.OldValue)
            Dim discount = 100 - Convert.ToDouble(args.NewValue) / cellValue * 100
            If discount > 0 AndAlso discount <= 30 Then Return
            Dim [error] = If(discount < 0, $"The price cannot be greater than {cellValue}", $"The discount cannot be greater than 30% ({cellValue * 0.7}). Please correct the price.")
            args.Result = New ValidationErrorInfo([error], ValidationErrorType.Critical)
        End Sub
    End Class
End Namespace
