Imports Microsoft.VisualBasic
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

Namespace DXGrid_ValidatingEditors
	Partial Public Class Window1
		Inherits Window
		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = New nwindDataSetTableAdapters.ProductsTableAdapter().GetData()
		End Sub

		Private Sub GridColumn_Validate(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
			Dim discontinued As Boolean = CBool((CType(e.Row, DataRowView))("Discontinued"))
			If discontinued Then
				Dim discount As Double = 100 - (Convert.ToDouble(e.Value) * 100) / Convert.ToDouble(e.CellValue)
				e.IsValid = discount > 0 AndAlso discount <= 30
				If (Not e.IsValid) Then
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
End Namespace
