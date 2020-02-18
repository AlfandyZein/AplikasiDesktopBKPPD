Public Class frmUpdate

    Sub KosongkanForm()
        txtNIP.Text = ""
        txtNama.Text = ""
        txtJabatanlama.Text = ""
        txtKreditlama.Text = ""
        txtJabatanbaru.Text = ""
        txtKreditbaru.Text = ""
    End Sub

    Sub MatikanForm()
        txtNIP.Enabled = False
        txtNama.Enabled = False
        txtJabatanlama.Enabled = False
        txtKreditlama.Enabled = False
        txtJabatanbaru.Enabled = False
        txtKreditbaru.Enabled = False
    End Sub

    Sub HidupkanForm()
        txtNIP.Enabled = True
        txtNama.Enabled = True
        txtJabatanlama.Enabled = True
        txtKreditlama.Enabled = True
        txtJabatanbaru.Enabled = True
        txtKreditbaru.Enabled = True
    End Sub

    Sub TampilkanData()
        Call KoneksiDB()
        DA = New OleDb.OleDbDataAdapter("select * from DataPNS", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DGV2.DataSource = DS.Tables(0)
        DGV2.ReadOnly = True
    End Sub

    Private Sub frmUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call MatikanForm()
        Call TampilkanData()
    End Sub

    Private Sub btnKeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub DGV2_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGV2.CellMouseClick
        On Error Resume Next
        txtNIP.Text = DGV2.Rows(e.RowIndex).Cells(3).Value
        txtNama.Text = DGV2.Rows(e.RowIndex).Cells(1).Value
        txtJabatanlama.Text = DGV2.Rows(e.RowIndex).Cells(7).Value
        txtKreditlama.Text = DGV2.Rows(e.RowIndex).Cells(8).Value
        txtJabatanbaru.Text = DGV2.Rows(e.RowIndex).Cells(9).Value
        txtKreditbaru.Text = DGV2.Rows(e.RowIndex).Cells(10).Value
        txtUnit.Text = DGV2.Rows(e.RowIndex).Cells(6).Value

        Call HidupkanForm()
        txtNIP.Enabled = False
        txtNama.Enabled = False
        txtUnit.Enabled = False
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If txtJabatanlama.Text = "" Or txtKreditlama.Text = "" Or txtJabatanbaru.Text = "" Or txtKreditbaru.Text = "" Then
            MsgBox("Data Belum Lengkap!")
            Exit Sub
        Else
            Call KoneksiDB()
            CMD = New OleDb.OleDbCommand("update DataPNS set JABATANLAMA='" & txtJabatanlama.Text & "',KREDITLAMA='" & txtKreditlama.Text & "',JABATANBARU='" & txtJabatanbaru.Text & "',KREDITBARU='" & txtKreditbaru.Text & "' where NIP='" & txtNIP.Text & "'", CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Tambah Data Sukses")
        End If

            Call MatikanForm()
            Call KosongkanForm()
            Call TampilkanData()

    End Sub
    Private Sub txtCari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCari.TextChanged
        Call KoneksiDB()
        CMD = New OleDb.OleDbCommand("select * from DataPNS where NAMA like '%" & txtCari.Text & "%'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            Call KoneksiDB()
            DA = New OleDb.OleDbDataAdapter("select * from DataPNS where NAMA like '%" & txtCari.Text & "%'", CONN)
            DS = New DataSet
            DA.Fill(DS)
            DGV2.DataSource = DS.Tables(0)
        Else
            MsgBox("Data Tidak Ditemukan!")
        End If
    End Sub
End Class