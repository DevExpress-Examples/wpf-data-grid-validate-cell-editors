Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Grid
Imports System.Collections.ObjectModel

Namespace DXGrid_ValidatingEditors

    Public Partial Class Window1
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            DataContext = New MyViewModel()
        End Sub

        Private Sub GridColumn_Validate(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
            Dim discontinued As Boolean = CType(e.Row, Product).Discontinued
            If discontinued Then
                Dim discount As Double = 100 - Convert.ToDouble(e.Value) * 100 / Convert.ToDouble(e.CellValue)
                If Not(discount > 0 AndAlso discount <= 30) Then
                    e.IsValid = False
                    e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical
                    If discount < 0 Then
                        e.ErrorContent = String.Format("The price cannot be greater than ${0}", Convert.ToDouble(e.CellValue))
                        Return
                    End If

                    e.ErrorContent = String.Format("The discount cannot be greater than 30% (${0}). Please correct the price.", Convert.ToDouble(e.CellValue) * 0.7)
                End If
            End If
        End Sub

        Private Sub TableView_HiddenEditor(ByVal sender As Object, ByVal e As EditorEventArgs)
            If Not Equals(e.Column.FieldName, "Discontinued") Then Return
            Me.grid.View.CommitEditing()
        End Sub
    End Class

    Public Class MyViewModel

        Public Sub New()
            CreateList()
        End Sub

        Public Property ProductList As ObservableCollection(Of Product)

        Private Sub CreateList()
            ProductList = New ObservableCollection(Of Product)()
            Dim r As Random = New Random()
            For i As Integer = 0 To 20 - 1
                Dim p As Product = New Product(i)
                p.UnitPrice = r.Next(1, 50)
                ProductList.Add(p)
            Next
        End Sub
    End Class

    Public Class Product

        Public Sub New(ByVal i As Integer)
            ProductName = "Product" & i
            Discontinued = i Mod 5 = 0
        End Sub

        Public Property ProductName As String

        Public Property UnitPrice As Integer

        Public Property Discontinued As Boolean
    End Class
End Namespace
