'Before you run this code please change all the value for the variables inside
'..conecDB procedure.  Set it to your server

'All public variable declarations are here
'All public connection procedures are here
'All public procedures and functions are here
Module modConn
    Public connDB As New SqlClient.SqlConnection
    Public comDB As New SqlClient.SqlCommand
    Public rdDB As SqlClient.SqlDataReader

    Public Item As ListViewItem

    Public pstrSQL As String

    Public pstrWeightLength() As String = {"pound", "pounds", "kilos", "kilo", "kilogram", "kilograms", "grams", "gram", "ounce", "ounces", "lbs", "ozs", "kg", "kgs", "cm", "centimetres", "centimetre", "centimeters", "centimeter", "inch", "inches", "foot", "feet"} 'some units ommited - anglers never express fish length in meters or yards for eg.
    Public pstrVessel() As String = {"boat", "launch", "charter", "dinghy", "kayak", "dingy", "skipper", "launched", "yak", "tub", "ship", "inflatable", "sail", "sailed", "onboard", "boats", "seasick", "sea sick", "drift", "drifting", "anchored", "anchor", "prowler", "tarpon", "warrior", "trident", "scupper", "hobby", "paddle"}
    Public pstrKayak() As String = {"kayak", "yak", "prowler", "tarpon", "trident", "scupper", "hobby", "paddle", "fatyak", "fat yak", "dorado", "kaskazi", "teksport", "emotion"}
    Public pstrVerbsAndTide() As String = {"arrived", "started", "fished", "fishing", "before low", "after low", "to low", "after high", "to high", "before high", "either side", "around high", "around low", "hour", "hours", "p.m.", "a.m", "flood", "ebb", "tide out", "tide down", "tide in", "tide up"}
    Public pstrVerbs() As String = {"fished", "fishing", "arrived", "started", "packed up", "went home", "stopped", "ended", "left"}
    Public pstrWeight() As String = {"pound", "pounds", "kilos", "kilo", "kilogram", "kilograms", "grams", "gram", "ounce", "ounces", "lbs", "ozs", "kg", "kgs"}
    Public pstrLength() As String = {"meter", "meters", "metre", "metres", "cm", "cms", "centimeters", "centimeter", "centimetres", "centimetre", "inch", "inches", "foot", "feet"}
    Public pstrTime() As String = {"hour", "mins", "minutes", "hrs", "minute", "hours", "min"}
    Public pstrGear() As String = {"rod", "spear", "spear gun", "longline", "longlines", "long line", "long lines", "seine", "gill net", "purse net", "beachcaster", "beachcasters", "beach caster", "beach casters", "paravane", "para vane", "rods"}
    Public pstrBassTarget() As String = {"bass hunt", "bass trip", "to catch bass", "target bass", "targetting bass", "fish for bass", "hoping for a bass", "hope for bass", "to catch a bass", "fishing for bass", "wanted bass", "bass targetted", "target was bass", "bass session", "hunting bass", "hunting for bass", "fish for bass", "fishing for bass", "plugging for bass", "spinning for bass", "plugs for bass", "rod for bass", "rods for bass", "plugged for bass", "plug for bass", "freelining sandeel", "freelining mackerel", "livebait", "live bait", "freelined mackerel", "freelined sandeel", "free lining sandeel", "free lining mackerel", "bass mark"}

#Region "DB Stuff"
    Public Sub conecDB()
        'This is the connection for your MS SQL Server
        Dim strServer As String = "127.0.0.1"    'This is the server IP/Server name.  If server is intalled on your local machine, your IP should be 127.0.0.1 or you may use localhost
        Dim strDbase As String = "surveys"   'Database name
        Dim strUser As String = "sa"                'Database user
        Dim strPass As String = "GGM290471"     'Database password

        If connDB.State <> ConnectionState.Open Then connDB.ConnectionString = "Data Source=" & strServer.Trim & ";Initial Catalog=" & strDbase.Trim & ";MultipleActiveResultSets=False;User ID=" & strUser.Trim & ";Password=" & strPass
        If connDB.State <> ConnectionState.Open Then connDB.Open()
    End Sub

    'Close the connection from database
    Public Sub closeDB()
        If connDB.State <> ConnectionState.Closed Then connDB.Close()
    End Sub

    'Initialize the sql command object
    Public Sub initCMD()
        With comDB
            .Connection = connDB
            .CommandType = CommandType.Text
            .CommandTimeout = 0
        End With
    End Sub

    Public Sub execComDB(ByVal PstrSQL As String)
        With comDB
            .CommandText = PstrSQL
            .ExecuteNonQuery()
        End With
    End Sub
#End Region

    Public Sub colorLV(ByVal Plv As ListView, ByVal PintIx As Integer, ByVal Pcolo As System.Drawing.Color, Optional ByVal PintFlgRefresh As Integer = 0)
        Plv.Items(PintIx).ForeColor = Pcolo
        'Refresh if flag is 1
        If PintFlgRefresh = 1 Then
            Plv.Refresh()
        End If
    End Sub

    Public Sub dispStatLB(ByVal PstatLbl As System.Windows.Forms.ToolStripStatusLabel, ByVal PstrMsg As String)
        PstatLbl.Text = PstrMsg
    End Sub


End Module
