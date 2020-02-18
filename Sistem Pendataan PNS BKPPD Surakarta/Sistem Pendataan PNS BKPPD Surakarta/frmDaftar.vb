Public Class frmDaftar

    Sub kosongkanForm()
        txtUrut.Text = ""
        txtNama.Text = ""
        txtTTL.Text = ""
        txtNIP.Text = ""
        txtPangkat.Text = ""
        txtPendidikan.Text = ""
        txtUnit.Text = ""
        txtNIP.Focus()
    End Sub

    Sub MatikanForm()
        txtUrut.Enabled = False
        txtNama.Enabled = False
        txtTTL.Enabled = False
        txtNIP.Enabled = False
        txtPangkat.Enabled = False
        txtPendidikan.Enabled = False
        txtUnit.Enabled = False
    End Sub

    Sub HidupkanForm()
        txtUrut.Enabled = True
        txtNama.Enabled = True
        txtTTL.Enabled = True
        txtNIP.Enabled = True
        txtPangkat.Enabled = True
        txtPendidikan.Enabled = True
        txtUnit.Enabled = True
    End Sub

    Sub TampilkanData()
        Call KoneksiDB()
        DA = New OleDb.OleDbDataAdapter("select * from DataPNS", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DGV1.DataSource = DS.Tables(0)
        DGV1.ReadOnly = True
    End Sub

    Private Sub frmDaftar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call MatikanForm()
        Call TampilkanData()
    End Sub

    Private Sub btnKeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub btnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        Call HidupkanForm()
        Call kosongkanForm()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Call MatikanForm()
        Call kosongkanForm()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If txtNama.Text = "" Or txtTTL.Text = "" Or txtNIP.Text = "" Or txtPangkat.Text = "" Or txtPendidikan.Text = "" Or txtUnit.Text = "" Then
            MsgBox("Data Belum Lengkap!")
            Exit Sub
        Else
            Call KoneksiDB()
            CMD = New OleDb.OleDbCommand("select * from DataPNS where NIP='" & txtNIP.Text & "'", CONN)
            DR = CMD.ExecuteReader
            DR.Read()
            If Not DR.HasRows Then
                Call KoneksiDB()
                Dim simpan As String
                simpan = "insert into DataPNS (URUT,NAMA,TTL,NIP,PANGKAT,PENDIDIKAN,UNIT) values('" & txtUrut.Text & "','" & txtNama.Text & "','" & txtTTL.Text & "','" & txtNIP.Text & "','" & txtPangkat.Text & "','" & txtPendidikan.Text & "','" & txtUnit.Text & "')"
                CMD = New OleDb.OleDbCommand(simpan, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Input Data Sukses")
            Else
                MsgBox("Nomor Urut atau NIP Tersebut Sudah Ada!")
            End If

            Call MatikanForm()
            Call kosongkanForm()
            Call TampilkanData()
        End If
    End Sub

    Private Sub DGV1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick
        On Error Resume Next
        txtUrut.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        txtNama.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
        txtTTL.Text = DGV1.Rows(e.RowIndex).Cells(2).Value
        txtNIP.Text = DGV1.Rows(e.RowIndex).Cells(3).Value
        txtPangkat.Text = DGV1.Rows(e.RowIndex).Cells(4).Value
        txtPendidikan.Text = DGV1.Rows(e.RowIndex).Cells(5).Value
        txtUnit.Text = DGV1.Rows(e.RowIndex).Cells(6).Value

        Call HidupkanForm()
        txtNIP.Enabled = False
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtNama.Text = "" Or txtTTL.Text = "" Or txtNIP.Text = "" Or txtPangkat.Text = "" Or txtPendidikan.Text = "" Or txtUnit.Text = "" Then
            MsgBox("Data belum lengkap!")
            Exit Sub
        Else
            Call KoneksiDB()
            CMD = New OleDb.OleDbCommand("update DataPNS set URUT='" & txtUrut.Text & "',NAMA='" & txtNama.Text & "',TTL='" & txtTTL.Text & "',PANGKAT='" & txtPangkat.Text & "',PENDIDIKAN='" & txtPendidikan.Text & "',UNIT='" & txtUnit.Text & "' where NIP='" & txtNIP.Text & "'", CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Update data sukses")
        End If

        Call MatikanForm()
        Call kosongkanForm()
        Call TampilkanData()
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        If txtNIP.Text = "" Then
            MsgBox("Tidak ada data yang akan dihapus!")
            Exit Sub
        Else
            If MessageBox.Show("Yakin akan menghapus data?", "konfirmasi", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call KoneksiDB()
                CMD = New OleDb.OleDbCommand("delete from DataPNS where NIP='" & txtNIP.Text & "'", CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Hapus Data Sukses")
                Call MatikanForm()
                Call kosongkanForm()
                Call TampilkanData()
            Else
                Call MatikanForm()
                Call kosongkanForm()
            End If

        End If
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
            DGV1.DataSource = DS.Tables(0)
        Else
            MsgBox("Data Tidak Ditemukan!")
        End If
    End Sub
End Class
