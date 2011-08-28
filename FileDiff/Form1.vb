Imports System.Threading.Tasks
Imports System.Collections.ObjectModel
Imports System.Threading
Imports System.IO



Public Class Form1

    Private Sub initialize()
        With ListView1
            .View = View.Details    '詳細表示にする。
            .FullRowSelect = True   '行選択を有効化する。
            .MultiSelect = True     '複数選択を可能にする。
            .Columns.Add("NO", 100, HorizontalAlignment.Left)   'ヘッダを追加する。
            .Columns.Add("比較元パス", 100, HorizontalAlignment.Left)
            .Columns.Add("比較先パス", 100, HorizontalAlignment.Left)
            .Columns.Add("結果", 100, HorizontalAlignment.Left)
            .GridLines = True
        End With
    End Sub
    
    Private Function _chk(ByVal path As String) As Boolean

        If System.IO.File.Exists(path) Then
            Dim uAttribute As System.IO.FileAttributes = System.IO.File.GetAttributes(path)
            If (uAttribute And System.IO.FileAttributes.ReadOnly) <> System.IO.FileAttributes.Directory Then
                MessageBox.Show("ディレクトリを指定してください")
                Return False
            End If
        End If
        If (Directory.Exists(path) = False) Then
            MessageBox.Show(path & "が開けません")
            Return False
        End If
        If (System.IO.Path.IsPathRooted(path) = False) Then
            MessageBox.Show("絶対パスで指定してください")
            Return False
        End If
        Return True
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f = New FileIO
        Dim i As Integer = 0
        f.source_file_path = "c:\support"
        f.destination_file_path = "D:\"
        If (_chk(f.source_file_path) = True AndAlso _chk(f.destination_file_path) = True) Then
            ' MessageBox.Show(f.make_file_list.Count)
            ListView1.BeginUpdate() '描画ストップ
            For Each file As String In f.make_file_list
                i += 1
                Dim item As New ListViewItem
                item.Text = i
                item.SubItems.Add(file)
                item.SubItems.Add(file.Replace(f.source_file_path, f.destination_file_path))
                item.SubItems.Add(String.Empty)
                ListView1.Items.Add(item)

                'True の場合、1行全体が対象になる
                ListView1.Items(i - 1).UseItemStyleForSubItems = True
                If (i Mod 2 = 0) Then
                    ListView1.Items(i - 1).BackColor = Color.AntiqueWhite
                Else
                    ListView1.Items(i - 1).BackColor = Color.AliceBlue
                End If
            Next
            ListView1.EndUpdate()
            '列ヘッダーの幅を自動的にサイズ変更する
            Dim colHed As ColumnHeader
            For Each colHed In ListView1.Columns
                colHed.Width = -2
            Next
        End If

    End Sub
    Delegate Sub SetExpensiveProcessDelegate(ByVal lst As ListViewItem)
 

    Sub ExpensiveProcess(ByVal lst As ListViewItem)
        Dim f = New FileIO
        Dim tmp As Boolean
        'MessageBox.Show(lst.Text)
        If ListView1.InvokeRequired Then
            Invoke(New SetExpensiveProcessDelegate(AddressOf ExpensiveProcess))
            Return
            'tmp = f.CompareFiles(lst.SubItems(1).Text, lst.SubItems(2).Text)

            'MessageBox.Show(lst.SubItems(3).Text)
            'MessageBox.Show(lst.Text)
        End If
        Interlocked.Exchange(lst.SubItems(3).Text, "a")
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        initialize()
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        ListView1.BeginUpdate() '描画ストップ
        Parallel.ForEach(ListView1.Items.OfType(Of ListViewItem)(), AddressOf ExpensiveProcess)
        ListView1.EndUpdate()
    End Sub
End Class
