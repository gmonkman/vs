<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvRec = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.statLB = New System.Windows.Forms.ToolStripStatusLabel
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnClearSentences = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.cmdSentencesGet = New System.Windows.Forms.Button
        Me.cmdLoadVenue = New System.Windows.Forms.Button
        Me.cmdWasTarget = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvRec
        '
        Me.lvRec.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lvRec.FullRowSelect = True
        Me.lvRec.GridLines = True
        Me.lvRec.Location = New System.Drawing.Point(12, 19)
        Me.lvRec.MultiSelect = False
        Me.lvRec.Name = "lvRec"
        Me.lvRec.Size = New System.Drawing.Size(658, 309)
        Me.lvRec.TabIndex = 0
        Me.lvRec.UseCompatibleStateImageBehavior = False
        Me.lvRec.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        Me.ColumnHeader1.Width = 68
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Location"
        Me.ColumnHeader2.Width = 159
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "trip_date"
        Me.ColumnHeader3.Width = 140
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "report"
        Me.ColumnHeader4.Width = 156
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lvRec)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(682, 338)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statLB})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 435)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(705, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'statLB
        '
        Me.statLB.Name = "statLB"
        Me.statLB.Size = New System.Drawing.Size(38, 17)
        Me.statLB.Text = "Ready"
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(10, 358)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(91, 27)
        Me.cmdAdd.TabIndex = 3
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(107, 358)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(91, 27)
        Me.cmdEdit.TabIndex = 4
        Me.cmdEdit.Text = "&Edit"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(204, 358)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(91, 27)
        Me.cmdDelete.TabIndex = 5
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Location = New System.Drawing.Point(301, 358)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(91, 27)
        Me.cmdRefresh.TabIndex = 6
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(451, 362)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(144, 21)
        Me.txtSearch.TabIndex = 7
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(602, 358)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(91, 27)
        Me.cmdSearch.TabIndex = 8
        Me.cmdSearch.Text = "&Search"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(448, 344)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(173, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Search in report and location"
        '
        'btnClearSentences
        '
        Me.btnClearSentences.Location = New System.Drawing.Point(15, 391)
        Me.btnClearSentences.Name = "btnClearSentences"
        Me.btnClearSentences.Size = New System.Drawing.Size(86, 23)
        Me.btnClearSentences.TabIndex = 10
        Me.btnClearSentences.Text = "ClearSents"
        Me.btnClearSentences.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(451, 391)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(242, 23)
        Me.ProgressBar1.TabIndex = 11
        '
        'cmdSentencesGet
        '
        Me.cmdSentencesGet.Location = New System.Drawing.Point(107, 390)
        Me.cmdSentencesGet.Name = "cmdSentencesGet"
        Me.cmdSentencesGet.Size = New System.Drawing.Size(124, 23)
        Me.cmdSentencesGet.TabIndex = 12
        Me.cmdSentencesGet.Text = "Write Sentences"
        Me.cmdSentencesGet.UseVisualStyleBackColor = True
        '
        'cmdLoadVenue
        '
        Me.cmdLoadVenue.Location = New System.Drawing.Point(237, 390)
        Me.cmdLoadVenue.Name = "cmdLoadVenue"
        Me.cmdLoadVenue.Size = New System.Drawing.Size(59, 21)
        Me.cmdLoadVenue.TabIndex = 13
        Me.cmdLoadVenue.Text = "Venues"
        Me.cmdLoadVenue.UseVisualStyleBackColor = True
        '
        'cmdWasTarget
        '
        Me.cmdWasTarget.Location = New System.Drawing.Point(317, 390)
        Me.cmdWasTarget.Name = "cmdWasTarget"
        Me.cmdWasTarget.Size = New System.Drawing.Size(75, 23)
        Me.cmdWasTarget.TabIndex = 14
        Me.cmdWasTarget.Text = "Target?"
        Me.cmdWasTarget.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 457)
        Me.Controls.Add(Me.cmdWasTarget)
        Me.Controls.Add(Me.cmdLoadVenue)
        Me.Controls.Add(Me.cmdSentencesGet)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnClearSentences)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parse Reports"
        Me.GroupBox1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvRec As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents statLB As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClearSentences As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents cmdSentencesGet As System.Windows.Forms.Button
    Friend WithEvents cmdLoadVenue As System.Windows.Forms.Button
    Friend WithEvents cmdWasTarget As System.Windows.Forms.Button

End Class
