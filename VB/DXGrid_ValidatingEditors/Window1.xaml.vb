Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Data
Imports DevExpress.Xpf.Grid
Imports System.Collections.ObjectModel

Namespace DXGrid_ValidatingEditors
    Partial Public Class Window1
        Inherits Window

        Public Sub New()
            InitializeComponent()
            Me.DataContext = New MyViewModel()
        End Sub

        Private Sub GridColumn_Validate(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
            Dim discontinued As Boolean = CType(e.Row, Product).Discontinued
            If discontinued Then
                Dim discount As Double = 100 - (Convert.ToDouble(e.Value) * 100) / Convert.ToDouble(e.CellValue)
                If Not(discount > 0 AndAlso discount <= 30) Then
                    e.IsValid = False
                    e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical
                    If discount < 0 Then
                        e.ErrorContent = String.Format("The price cannot be greater than ${0}", Convert.ToDouble(e.CellValue))
                        Return
                    End If
                    e.ErrorContent = String.Format("The discount cannot be greater than 30% (${0}). Please correct the price.", Convert.ToDouble(e.CellValue)*0.7)
                End If
            End If
        End Sub

        Private Sub TableView_HiddenEditor(ByVal sender As Object, ByVal e As EditorEventArgs)
            If e.Column.FieldName <> "Discontinued" Then
                Return
            End If
            grid.View.CommitEditing()
        End Sub
    End Class


    Public Class MyViewModel
        Public Sub New()
            CreateList()
        End Sub

        Public Property ProductList() As ObservableCollection(Of Product)
        Private Sub CreateList()
            ProductList = New ObservableCollection(Of Product)()
            Dim r As New Random()
            For i As Integer = 0 To 19
                Dim p As New Product(i)
                p.UnitPrice = r.Next(1, 50)
                ProductList.Add(p)
            Next i
        End Sub
    End Class
    Public Class Product
        Public Sub New(ByVal i As Integer)
            ProductName = "Product" & i
            Discontinued = i Mod 5 = 0
        End Sub

        Public Property ProductName() As String
        Public Property UnitPrice() As Integer
        Public Property Discontinued() As Boolean
    End Class
End Namespace
