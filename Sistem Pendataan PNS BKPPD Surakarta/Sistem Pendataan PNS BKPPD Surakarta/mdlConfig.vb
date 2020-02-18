Imports System.Data.OleDb
Module mdlConfig
    Public CONN As OleDbConnection
    Public DA As OleDbDataAdapter
    Public DS As DataSet
    Public CMD As OleDbCommand
    Public DR As OleDbDataReader

    Sub KoneksiDB()
        Try
            CONN = New OleDbConnection("provider=microsoft.ace.oledb.12.0; data source=Database PNS.accdb")
            CONN.Open()
            'MsgBox("Koneksi Sukses")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Module
