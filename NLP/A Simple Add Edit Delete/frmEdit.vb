Public Class frmEdit

    Dim intDB_ID_Selected As Integer        'Selected ID from the listview

    Private Sub frmEdit_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        intDB_ID_Selected = CInt(frmMain.lvRec.SelectedItems(0).Text)
        Call dispCaption()
        Call dispInfo()     'Display the info of the selected ID
    End Sub

    Private Sub dispInfo()
        pstrSQL = "Select * from forum " & _
              "where id=" & intDB_ID_Selected & ""
        With comDB
            .CommandText = pstrSQL
            rdDB = .ExecuteReader
        End With
        If rdDB.HasRows = True Then
            rdDB.Read()
            Me.txtFname.Text = rdDB!fldname.ToString.Trim
            Me.txtMname.Text = rdDB!fldmname.ToString.Trim
            Me.txtLname.Text = rdDB!fldlname.ToString.Trim
            Me.txtAge.Text = rdDB!fldage.ToString.Trim
        End If
        rdDB.Close()
    End Sub

    Private Sub dispCaption()
        Me.txtID.Text = intDB_ID_Selected.ToString
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Call disControl()
        'Validation
        If invalidUpdateEntry() = True Then
            Call enaControl()
            Exit Sub
        End If
        'Prompt the user if the record will be updated
        If MsgBox("Are you sure you want to update the selected record?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question, MsgBoxStyle), "Update") = MsgBoxResult.Yes Then
            'Update query
            pstrSQL = "Update tbl_info " & _
                  "set fldname='" & Me.txtFname.Text.Trim & "'," & _
                  "fldmname='" & Me.txtMname.Text.Trim & "'," & _
                  "fldlname='" & Me.txtLname.Text.Trim & "'," & _
                  "fldage=" & CInt(Me.txtAge.Text) & " " & _
                  "where fldid=" & intDB_ID_Selected & ""
            Call execComDB(pstrSQL)     'Execute the query
            Me.Close()
            '*** Refresh the list
            pstrSQL = "Select * from tbl_info " & _
                  "order by fldid desc"
            Call frmMain.dispRec(pstrSQL)
            '--- End of refreshing the list
            Exit Sub
        Else
            Call enaControl()
        End If
    End Sub

    Private Function invalidUpdateEntry() As Boolean
        'Check if all fields have values
        If Me.txtFname.Text.Trim = "" Or Me.txtMname.Text.Trim = "" Or Me.txtLname.Text.Trim = "" Or Me.txtAge.Text.Trim = "" Then
            MsgBox("All fields are required!", MsgBoxStyle.Exclamation, "Insufficient Data")
            Return True
        End If
        'Check if age is numeric
        If IsNumeric(Me.txtAge.Text) = False Then
            MsgBox("Age must be numeric!", MsgBoxStyle.Exclamation, "Invalid Age")
            Return True
        End If
    End Function

    Private Sub disControl()
        Me.txtFname.Enabled = False
        Me.txtMname.Enabled = False
        Me.txtLname.Enabled = False
        Me.txtAge.Enabled = False

        Me.cmdUpdate.Enabled = False
        Me.cmdCancel.Enabled = False
    End Sub

    Private Sub enaControl()
        Me.txtFname.Enabled = True
        Me.txtMname.Enabled = True
        Me.txtLname.Enabled = True
        Me.txtAge.Enabled = True

        Me.cmdUpdate.Enabled = True
        Me.cmdCancel.Enabled = True
    End Sub

    Private Sub txtFname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFname.KeyPress
        If e.KeyChar = "'" Or e.KeyChar = "\" Then
            e.KeyChar = CChar("")
        End If
    End Sub

    Private Sub txtFname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFname.TextChanged

    End Sub

    Private Sub txtMname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMname.KeyPress
        If e.KeyChar = "'" Or e.KeyChar = "\" Then
            e.KeyChar = CChar("")
        End If
    End Sub

    Private Sub txtMname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMname.TextChanged

    End Sub

    Private Sub txtLname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLname.KeyPress
        If e.KeyChar = "'" Or e.KeyChar = "\" Then
            e.KeyChar = CChar("")
        End If
    End Sub

    Private Sub txtLname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLname.TextChanged

    End Sub

    Private Sub txtAge_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge.KeyPress
        If e.KeyChar = "'" Or e.KeyChar = "\" Then
            e.KeyChar = CChar("")
        End If
    End Sub

    Private Sub txtAge_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAge.TextChanged

    End Sub
End Class