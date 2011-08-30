Imports System.Threading.Tasks
Imports System.Collections.ObjectModel
Imports System.Threading
Imports System.IO
Imports System.Data




Public Class Form1
    Dim dataTable As New DataTable
    Dim f As FileIO
    ''' <summary>
    ''' 初期化処理
    ''' </summary>
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
    ''' <summary>
    ''' ディレクトリパス指定チェック
    ''' </summary>
    ''' <param name="path">ファイルパス</param>
    ''' <returns>True:チェックOK False:チェックNG</returns>
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
    ''' <summary>
    ''' ぼたん１
    ''' </summary>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim i As Integer = 0
        f = New FileIO()
        f.source_file_path = Me.Txt_Path_Source.Text
        f.destination_file_path = Me.Txt_Path_Dest.Text
        If (_chk(f.source_file_path) = True AndAlso _chk(f.destination_file_path) = True) Then
            ' MessageBox.Show(f.make_file_list.Count)
            ListView1.BeginUpdate() '描画ストップ



            ' CustomerID 列を作成して追加します
            Dim colWork As New DataColumn("NO", GetType(String))
            DataTable.Columns.Add(colWork)

            ' CustomerID 列をキー配列に追加し、DataTable にバインドします
            Dim Keys(0) As DataColumn
            Keys(0) = colWork
            DataTable.PrimaryKey = Keys

            ' CustomerName 列を作成して追加します
            colWork = New DataColumn("比較元パス", GetType(String))
            DataTable.Columns.Add(colWork)

            ' LastOrderDate 列を作成して追加します
            colWork = New DataColumn("比較先パス", GetType(String))
            DataTable.Columns.Add(colWork)

            ' LastOrderDate 列を作成して追加します
            colWork = New DataColumn("結果", GetType(String))
            DataTable.Columns.Add(colWork)

            For Each file As String In f.make_file_list
                i += 1

                ' 行を追加します
                Dim row As DataRow = dataTable.NewRow
                row("NO") = i
                row("比較元パス") = file
                row("比較先パス") = file.Replace(f.source_file_path, f.destination_file_path)
                row("結果") = String.Empty
                DataTable.Rows.Add(row)




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


            '' Iterate through a collection
            'For Each myRow As DataRow In DataTable.Rows
            '    MessageBox.Show(myRow.Item("比較元パス").ToString)
            'Next




        End If

    End Sub
    Delegate Sub SetExpensiveProcessDelegate(ByVal lst As ListViewItem)

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        'ListView1.BeginUpdate() '描画ストップ

        'Parallel.ForEach(dataTable.Rows.OfType(Of DataRow)(), AddressOf ExpensiveProcess)
        'ListView1.Clear()
        'ListView1.Tag = dataTable.Rows
        Timer1.Enabled = True
        For Each myRow As ListViewItem In ListView1.Items

            MessageBox.Show(myRow.SubItems(0).Text)
            If (f.CompareFiles(myRow.SubItems(1).Text, myRow.SubItems(2).Text) = True) Then
                myRow.SubItems(3).Text = "OK"
            Else
                myRow.SubItems(3).Text = "NG"
            End If

        Next
        Timer1.Enabled = False
        ' ListView1.EndUpdate()
    End Sub
    Sub ExpensiveProcess(ByVal lst As DataRow)

        'MessageBox.Show(lst.Text)
        'If ListView1.InvokeRequired Then
        '    Invoke(New SetExpensiveProcessDelegate(AddressOf ExpensiveProcess))
        '    Return
        '    'tmp = f.CompareFiles(lst.SubItems(1).Text, lst.SubItems(2).Text)

        '    'MessageBox.Show(lst.SubItems(3).Text)
        '    'MessageBox.Show(lst.Text)
        'End If
        'Interlocked.Exchange(lst.SubItems(3).Text, "a")
        'System.Threading.Monitor.Enter(Me)
        'lst.SubItems(3).Text = "s"
        'System.Threading.Monitor.Exit(Me)
        'MessageBox.Show(lst("NO"))
        ' lst("結果").update = "OK"

    End Sub
    Private Sub SetText(ByVal [text] As String)

        ' InvokeRequired required compares the thread ID of the' calling thread to the thread ID of the creating thread.' If these threads are different, it returns true.
        'If Me.ListView1.InvokeRequired Then
        '    Dim d As New SetTextCallback(AddressOf SetText)
        '    Me.Invoke(d, New Object() {[text]})
        'Else
        '    Me.ListView1.Text = [text]
        'End If
    End Sub

    'Private Function MakeCustomersDataTable() As DataTable
    '' プログラムが生成したデータを格納する DataTable を宣言します
    'Dim dataTable As New DataTable

    '' CustomerID 列を作成して追加します
    'Dim colWork As New DataColumn("NO", GetType(String))
    'dataTable.Columns.Add(colWork)

    '' CustomerID 列をキー配列に追加し、DataTable にバインドします
    'Dim Keys(0) As DataColumn
    'Keys(0) = colWork
    'dataTable.PrimaryKey = Keys

    '' CustomerName 列を作成して追加します
    'colWork = New DataColumn("比較元パス", GetType(String))
    'dataTable.Columns.Add(colWork)

    '' LastOrderDate 列を作成して追加します
    'colWork = New DataColumn("比較先パス", GetType(String))
    'dataTable.Columns.Add(colWork)

    '' LastOrderDate 列を作成して追加します
    'colWork = New DataColumn("結果", GetType(String))
    'dataTable.Columns.Add(colWork)

    'Return dataTable
    'End Function


    Function Show_FolderBrowserDialog() As String
        Dim FolderBrowserDialog1 As FolderBrowserDialog

        Try
            ' FolderBrowserDialog の新しいインスタンスを生成する (デザイナから追加している場合は必要ない)
            FolderBrowserDialog1 = New FolderBrowserDialog()

            ' ダイアログの説明を設定する
            FolderBrowserDialog1.Description = "フォルダを指定してください。"

            ' ルートになる特殊フォルダを設定する (初期値 SpecialFolder.Desktop)
            FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer

            ' 初期選択するパスを設定する
            'FolderBrowserDialog1.SelectedPath = "C:\Program Files\"

            ' [新しいフォルダ] ボタンを表示する (初期値 True)
            FolderBrowserDialog1.ShowNewFolderButton = False

            ' ダイアログを表示し、戻り値が [OK] の場合は、選択したディレクトリを表示する
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Return FolderBrowserDialog1.SelectedPath.ToString
            Else
                Return String.Empty
            End If

        Finally
            If Not FolderBrowserDialog1 Is Nothing Then
                ' 不要になった時点で破棄する (正しくは オブジェクトの破棄を保証する を参照)
                FolderBrowserDialog1.Dispose()
            End If
        End Try
    End Function
    ''' <summary>
    ''' 比較元ディレクトリ選択ボタン
    ''' </summary>
    Private Sub Bth_Source_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bth_Source.Click
        Dim ret As String
        ret = Show_FolderBrowserDialog()
        If (ret <> String.Empty) Then
            Txt_Path_Source.Text = ret
        End If
    End Sub
    ''' <summary>
    ''' 比較先ディレクトリ選択ボタン
    ''' </summary>
    Private Sub Bth_Dest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bth_Dest.Click
        Dim ret As String
        ret = Show_FolderBrowserDialog()
        If (ret <> String.Empty) Then
            Txt_Path_Dest.Text = ret
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        initialize()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = f.file_size

        ProgressBar1.Value = f.read_file_dize
        ProgressBar1.Update()
    End Sub
End Class
