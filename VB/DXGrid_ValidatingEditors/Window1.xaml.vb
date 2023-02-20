Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data
Imports DevExpress.Xpf.Grid

Namespace DXGrid_ValidatingEditors

    Public Partial Class Window1
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Me.grid.ItemsSource = New nwindDataSetTableAdapters.ProductsTableAdapter().GetData()
        End Sub

        Private Sub GridColumn_Validate(ByVal sender As Object, ByVal e As GridCellValidationEventArgs)
            Dim discontinued As Boolean = CBool(CType(e.Row, DataRowView)("Discontinued"))
            If discontinued Then
                Dim discount As Double = 100 - Convert.ToDouble(e.Value) * 100 / Convert.ToDouble(e.CellValue)
                If Not CSharpImpl.__Assign(e.IsValid, discount > 0 AndAlso discount <= 30) Then
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

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
