Imports System.Text

Public Class frmMain
    Private nlp As New parseReports.nlp
    Private malWelshVenues As New ArrayList

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Call closeDB()      'Close the connection from database
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call conecDB()      'Open the connection to database
        Call initCMD()      'Initialize the sqlclient command object
        pstrSQL = "Select id,location,trip_date,report from forum"
        Call dispRec(pstrSQL)
        Call dispRecCount() 'Display record count
    End Sub

    Public Sub dispRecCount()
        Call dispStatLB(Me.statLB, "Record count: " & Me.lvRec.Items.Count.ToString)
    End Sub

    Public Sub dispRec(ByVal PstrSQL As String)
        Dim dt As DateTime
        Me.lvRec.Items.Clear()

        With comDB
            .CommandText = PstrSQL
            rdDB = .ExecuteReader
        End With
        Do While rdDB.Read
            Item = Me.lvRec.Items.Add(rdDB!id.ToString)
            Item.SubItems.Add(rdDB!location.ToString.Trim)
            Item.SubItems.Add(IIf(DateTime.TryParse(rdDB!trip_date.ToString.Trim, dt), Format(dt, "yyyy-MMM-dd"), "").ToString)
            Item.SubItems.Add(rdDB!report.ToString.Trim)
            My.Application.DoEvents()
        Loop
        rdDB.Close()
    End Sub

    Private Sub disControl()
        Me.cmdAdd.Enabled = False
        Me.cmdDelete.Enabled = False
        Me.cmdEdit.Enabled = False
        Me.cmdRefresh.Enabled = False
        Me.cmdSearch.Enabled = False
        Me.txtSearch.Enabled = False
        Me.ProgressBar1.Visible = True
    End Sub

    Private Sub enaControl()
        Me.cmdAdd.Enabled = True
        Me.cmdDelete.Enabled = True
        Me.cmdEdit.Enabled = True
        Me.cmdRefresh.Enabled = True
        Me.cmdSearch.Enabled = True
        Me.txtSearch.Enabled = True
        Me.ProgressBar1.Visible = False
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Call disControl()
        frmAdd.ShowDialog()
        Call enaControl()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        Call disControl()
        'Validation
        If invalidEditEntry() = True Then
            Call enaControl()
            Exit Sub
        End If
        frmEdit.ShowDialog()        'Show the edit window
        Call enaControl()
    End Sub

    Private Function invalidEditEntry() As Boolean
        'Check if listview has records
        If Me.lvRec.Items.Count < 1 Then
            MsgBox("Listview has no records!", MsgBoxStyle.Exclamation, "Nothing to Edit/Delete")
            Return True
            Exit Function
        End If
        'Check if listview has selected record
        If Me.lvRec.SelectedIndices.Count < 1 Then
            MsgBox("There is no selected record!", MsgBoxStyle.Exclamation, "Nothing is Selected")
            Return True
            Exit Function
        End If
    End Function

    Private Function invalidDelEntry() As Boolean
        'Check if listview has records
        If Me.lvRec.Items.Count < 1 Then
            MsgBox("Listview has no records!", MsgBoxStyle.Exclamation, "Nothing to Edit/Delete")
            Return True
            Exit Function
        End If
        'Check if listview has selected record
        If Me.lvRec.SelectedIndices.Count < 1 Then
            MsgBox("There is no selected record!", MsgBoxStyle.Exclamation, "Nothing is Selected")
            Return True
            Exit Function
        End If
    End Function

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim intSel_ID As Integer

        Call disControl()
        'Validation
        If invalidDelEntry() = True Then
            Call enaControl()
            Exit Sub
        End If

        intSel_ID = CInt(Me.lvRec.SelectedItems(0).Text)
        'Prompt the user if record should really be deleted
        If MsgBox("Are you sure you want to delete the selected record?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, MsgBoxStyle), "Delete") = MsgBoxResult.Yes Then
            'Delete query
            pstrSQL = "Delete from forum " & _
                  "where id=" & intSel_ID & ""
            Call execComDB(pstrSQL)
            'Remove the record from the list
            Me.lvRec.Items(Me.lvRec.SelectedItems(0).Index).Remove()
            Call dispRecCount()     'Display record count
        End If
        Call enaControl()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call disControl()
        pstrSQL = "Select * from forum " & _
              "order by id desc"
        Call dispRec(pstrSQL)
        Call dispRecCount() 'Display record count
        MsgBox("The list was refreshed successfully!", MsgBoxStyle.Information, "Refresh")
        Call enaControl()
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Call disControl()
        'Validation
        If invalidSearchEntry() = True Then
            Call enaControl()
            Exit Sub
        End If
        'Search query
        pstrSQL = "Select * from forum where report like '%" & Me.txtSearch.Text.Trim & "%' or location like '%" & Me.txtSearch.Text.Trim & "%';"
        Call dispRec(pstrSQL)
        Call dispRecCount() 'Display record count

        Call enaControl()
    End Sub

    Private Function invalidSearchEntry() As Boolean
        'Check if search field has value
        If Me.txtSearch.Text.Trim = "" Then
            'Search field does not have value
            MsgBox("Type text to be searched!", MsgBoxStyle.Exclamation, "Nothing to Search")
            Return True
        End If
    End Function

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.cmdSearch.PerformClick()
        End If
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If e.KeyChar = "'" Or e.KeyChar = "\" Then
            e.KeyChar = CChar("")
        End If
    End Sub


    Private Sub parseSentences()
        Dim sql As String = "", stmp$
        Dim report As String = ""
        Dim id As Integer = 0
        Dim sentences() As String
        Dim dt As New DataTable
        Dim cnt As Integer = 0
        Dim al As New ArrayList
        Dim row As DataRow
        Dim nrRecords& = 0
        Dim bReportHadMeasure As Boolean = False

        Const SEP As String = " // "
        Try
            Me.disControl()
            With comDB
                .CommandText = "select * from forum where report like '%bass%' and forum.id not in (select forumid from forum_sentence) and isnull(location,'') <> '' and sentences_written=0 and is_cleaned=1;"
                rdDB.Close()
                rdDB = .ExecuteReader
            End With
            dt.Load(rdDB)
            Dim i As Integer = dt.Rows.Count
            Me.ProgressBar1.Maximum = i
            Me.ProgressBar1.Minimum = 0
            Me.ProgressBar1.Value = 0
            For Each row In dt.Rows
                bReportHadMeasure = False
                report = row("report").ToString
                id = CInt(row("id"))
                If id = 16369 Then Stop
                sentences = nlp.SplitSentences(report)
                Dim sbSize As New StringBuilder
                Dim sbGear As New StringBuilder
                Dim sbPlat As New StringBuilder
                Dim sbSess As New StringBuilder
                Dim sbBassNr As New StringBuilder
                For Each sentence In sentences
                    sentence = sentence.Replace("'", " ")

                    'Has a bass size estimate
                    If ParseFunctions.SentenceHasTextAndNumber(sentence, "bass") Then
                        If Not sbSize.ToString.Trim.ToLower.Contains(sentence.Trim.ToLower) Then sbSize.Append(sentence & SEP)
                    End If

                    If ParseFunctions.SentenceIsMatchAndHasNumeric(sentence, "bass", pstrWeightLength) Then
                        If Not sbSize.ToString.Trim.ToLower.Contains(sentence.Trim.ToLower) Then sbSize.Append(sentence & SEP)
                        bReportHadMeasure = True
                    End If

                    If ParseFunctions.SentenceHasTextMulti(sentence, pstrGear) Then
                        stmp = GetSurroundingWordsMultiMatches(sentence, pstrGear, 8)
                        If Not sbGear.ToString.Trim.ToLower.Contains(stmp.Trim.ToLower) Then sbGear.Append(stmp & SEP)
                    End If

                    If StringArrayInSentenceGetFirst(sentence, pstrVessel) <> "" Then
                        stmp = GetSurroundingWordsMultiMatches(sentence, pstrVessel, 8)
                        If Not sbPlat.ToString.Trim.ToLower.Contains(stmp.Trim.ToLower) Then sbPlat.Append(stmp & SEP)
                    End If

                    If CheckDistanceAnyNumberMultiText(sentence, pstrVerbsAndTide, 30) Or HasMatchedStringswithTimeMultiText(sentence, pstrVerbsAndTide) Then
                        If Not sbSess.ToString.Trim.ToLower.Contains(sentence.Trim.ToLower) Then sbSess.Append(sentence & SEP)
                    End If

                Next sentence
                If bReportHadMeasure Then 'dont bother recording rest if no length bass info
                    nrRecords& = nrRecords& + 1
                    insert_sentence(sbSize.ToString, "", "bass_size", id)
                    If sbGear.Length > 0 Then insert_sentence(sbGear.ToString, "", "gear_number", id)
                    If sbPlat.Length > 0 Then insert_sentence(sbPlat.ToString, "", "platform", id)
                    If sbSess.Length > 0 Then insert_sentence(sbSess.ToString, "", "session_length", id)
                End If

                modConn.execComDB("update forum set sentences_written=1 where forum.id=" & id.ToString & ";")

                Application.DoEvents()
                Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
            Next row
            'modConn.execComDB("delete from forum_sentence where [id] not in (select max(id) from forum_sentence group by sentence,forumid);")
            Call Me.SetPlatform()
            MessageBox.Show("Wrote sentences for " & nrRecords&.ToString & " forum reports out of " & Me.ProgressBar1.Maximum.ToString, "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.enaControl()
        End Try
    End Sub

    Private Sub insert_sentence(ByVal sentence As String, ByVal originator As String, ByVal sentence_type As String, ByVal id As Integer)
        sentence.Replace("'", " ")
        pstrSQL = "insert into forum_sentence (forumid,sentence_type,sentence,original_sentence) values (" & id.ToString & ",'" + sentence_type + "','" + sentence & "','" & originator & "');"
        modConn.execComDB(pstrSQL)
    End Sub

    Private Sub btnClearSentences_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSentences.Click
        Try
            If Not modConn.rdDB.IsClosed Then modConn.rdDB.Close()
            modConn.execComDB("delete from forum_sentence")
            MessageBox.Show("Done", "Complete", MessageBoxButtons.OK)
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub cmdSentencesGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSentencesGet.Click
        Try
            Me.disControl()
            Me.parseSentences()
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        Finally
            Me.enaControl()
        End Try
    End Sub

    Private Sub fillVenueArrayList()
        With comDB
            .CommandText = "select * from welsh_venue order by id asc"
            If Not rdDB.IsClosed Then rdDB.Close()
            rdDB = .ExecuteReader
        End With
        malWelshVenues.Clear()
        Do While rdDB.Read
            malWelshVenues.Add(rdDB!venue.ToString)
        Loop
    End Sub

    Private Sub cmdLoadVenue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadVenue.Click
        Dim com As New SqlClient.SqlCommand
        Dim dt As New DataTable
        Dim sql$, id$
        Dim row As DataRow
        Dim al As New ArrayList
        Dim cntV& = 0, cntNoV& = 0
        Try
            Me.disControl()
            Me.fillVenueArrayList()

            If Not rdDB.IsClosed Then rdDB.Close()
            With com
                .Connection = connDB
                .CommandType = CommandType.Text
                .CommandTimeout = 0
                .CommandText = "delete from welsh_venue where [id] not in (select min(id) from welsh_venue group by venue);" 'delete duplicates
                com.ExecuteNonQuery()
            End With

            If MessageBox.Show("Do you want to clear ALL locations before proceeding?", "Clear", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                com.CommandText = "update forum set location='',location_checked=0"
                com.ExecuteNonQuery()
            End If

            If MessageBox.Show("Do you only want to process reports containing bass?", "Process", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                sql = "select id,report from forum where location_checked=0 and report like '%bass%'"
            Else
                sql = "select id,report from forum where location_checked=0"
            End If
            If Not rdDB.IsClosed Then rdDB.Close()
            With comDB
                .CommandText = sql
                rdDB = .ExecuteReader
            End With
            dt.Load(rdDB)
            Dim i As Integer = dt.Rows.Count
            Me.ProgressBar1.Maximum = i
            Me.ProgressBar1.Minimum = 0
            Me.ProgressBar1.Value = 0
            For Each row In dt.Rows
                id = row("id").ToString
                al = ParseFunctions.StringArrayInSentenceGetAll(row("report").ToString, Me.malWelshVenues, True)
                If al.Count > 0 Then
                    Dim sb As New StringBuilder
                    For Each ss As String In al
                        sb.Append(ss + ",")
                    Next
                    sql = "update forum set location='" & sb.ToString.Trim(CChar(",")) & "' where id=" & id & ";"
                    com.CommandText = sql
                    com.ExecuteNonQuery()
                    cntV = cntV + 1
                Else
                    cntNoV = cntNoV + 1
                End If

                sql = "update forum set location_checked=1 where id=" & id & ";"
                com.CommandText = sql
                com.ExecuteNonQuery()

                Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
                My.Application.DoEvents()
            Next row
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        Finally
            MessageBox.Show("Set locations for " & cntV.ToString & " records. Location not found for " & cntNoV.ToString & " records.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
            enaControl()
        End Try
    End Sub

    Private Sub cmdWasTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWasTarget.Click
        Dim dt As New DataTable
        Dim com As New SqlClient.SqlCommand
        Dim row As DataRow
        Try
            disControl()
            If Not rdDB.IsClosed Then rdDB.Close()
            With com
                .Connection = connDB
                .CommandType = CommandType.Text
                .CommandTimeout = 0
            End With

            If Not rdDB.IsClosed Then rdDB.Close()
            With comDB
                .CommandText = "select id,report from forum"
                rdDB = .ExecuteReader
            End With
            dt.Load(rdDB)
            Dim i As Integer = dt.Rows.Count
            Me.ProgressBar1.Maximum = i
            Me.ProgressBar1.Minimum = 0
            Me.ProgressBar1.Value = 0
            For Each row In dt.Rows
                If ParseFunctions.SentenceHasTextMulti(row("report").ToString, pstrBassTarget) Then
                    com.CommandText = "update forum set bass_was_target=1 where id=" & row("id").ToString & ";"
                Else
                    com.CommandText = "update forum set bass_was_target=0 where id=" & row("id").ToString & ";"
                End If
                Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
                My.Application.DoEvents()
            Next
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        Finally
            enaControl()
        End Try
    End Sub


    Private Sub SetPlatform()
        Dim sb As New StringBuilder


        sb.Append("update forum set platform='Boat [Charter]' where report like '%Bad Boys%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Bad Ladz%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Happy Hooker%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Lander%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Morgan James%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Volsung%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Jensen%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Susie%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Jenny II%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Jenny 2%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Jenny2%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Gwen-Paul-M%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Susan Jayne%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Mikatcha%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Lady Gwen%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%White Water%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Bilko%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Phat cat%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%JUDY B%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Aldebaran%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Stingray%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Anchorman5%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Anchorman%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Empress%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Spindrift%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%My Way%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Goldilocks%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Starida%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Lady Gwen%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Starida%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Sally%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Bang Tidy%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Seekat%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%ESP%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Sallyport%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Seren y Mor%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Serenymor%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%seren-y-mor%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Blue Thunder%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Solo Sun%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Incentive%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Morio Mon%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Ma Chipe Seabrin%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Outrider%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Conway Star%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Morlo%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Celtic Wildcat%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Benjoma Too%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Gower Ranger%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Panther%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Catfish%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Osprey%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Lady Jue%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Merlin%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Leah James%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Celtic Spirit%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Suveran%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Supreme%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Ebony May%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%3 Fishes%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%three Fishes%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Highlander%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Laura Jane%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%Sarah Louise%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%myway%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%chieftain%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '% cristo %' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%goldielocks%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Charter]' where report like '%hafaled%' and isnull(platform,'')='';")

        sb.Append("update forum set platform='Boat [Kayak]' where report like '%kayak%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%yak%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%prowler%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%tarpon%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%trident%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%scupper%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%hobby%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%paddle%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%paddled%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%paddling%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%fatyak%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%fat yak%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%dorado%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%kaskazi%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Kayak]' where report like '%teksport%' and isnull(platform,'')='';")

        sb.Append("update forum set platform='Boat [Private]' where report like '%boat%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%launch%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%charter%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%skipper%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%dinghy%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%kayak%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%dingy%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%skipper%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%launched%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%yak%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%tub%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%ship%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%inflatable%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%sail%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%sailed %' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%onboard%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%boats%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%seasick%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%sea sick %' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%drift%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%drifting%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%anchored%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%anchor%' and isnull(platform,'')='';")
        sb.Append("update forum set platform='Boat [Private]' where report like '%warrior%' and isnull(platform,'')='';")



        modConn.execComDB(sb.ToString)
    End Sub
End Class
