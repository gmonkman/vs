Public Class frmAdd

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmAdd_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub clearTxtBox()
        Me.txtAge.Text = ""
        Me.txtFname.Text = ""
        Me.txtLname.Text = ""
        Me.txtMname.Text = ""
    End Sub

    Private Sub disControl()
        Me.txtAge.Enabled = False
        Me.txtFname.Enabled = False
        Me.txtLname.Enabled = False
        Me.txtMname.Enabled = False

        Me.cmdCancel.Enabled = False
        Me.cmdSave.Enabled = False
    End Sub

    Private Sub enaControl()
        Me.txtAge.Enabled = True
        Me.txtFname.Enabled = True
        Me.txtLname.Enabled = True
        Me.txtMname.Enabled = True

        Me.cmdCancel.Enabled = True
        Me.cmdSave.Enabled = True
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Call disControl()
        'Validation
        If invalidSaveEntry() = True Then
            Call enaControl()
            Exit Sub
        End If
        'Start Save
        pstrSQL = "Insert into tbl_info(fldname," & _
              "fldmname," & _
              "fldlname," & _
              "fldage," & _
              "ent_dt) values('" & Me.txtFname.Text.Trim & "'," & _
              "'" & Me.txtMname.Text.Trim & "'," & _
              "'" & Me.txtLname.Text.Trim & "'," & _
              "" & Val(Me.txtAge.Text) & "," & _
              "'" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "')"
        Call execComDB(pstrSQL)     'Execute the insert query
        Me.Close()    'Close the form
        '*** Display new record
        pstrSQL = "Select * from tbl_info " & _
              "order by fldid desc"
        Call frmMain.dispRec(pstrSQL)
        Call frmMain.dispRecCount() 'Display record count
        '--- End of displaying new record
    End Sub

    Private Function invalidSaveEntry() As Boolean
        'Make sure that all fields have values
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

    Private Sub txtFname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFname.KeyDown

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
End Class