Imports System.IO

Public Class FileIO

    Private _source_file_path As String = String.Empty
    Private _destination_file_path As String = String.Empty
    Private _file_size As Long
    Private _read_file_size As Long
    ReadOnly Property file_size() As String
        Get
            Return _file_size
        End Get
    End Property
    ReadOnly Property read_file_size() As String
        Get
            Return _read_file_size
        End Get
    End Property

    Public Property source_file_path() As String
        Get
            Return _source_file_path
        End Get
        Set(ByVal filepath As String)
            _source_file_path = filepath
        End Set
    End Property
    Public Property destination_file_path() As String
        Get
            Return _destination_file_path
        End Get
        Set(ByVal filepath As String)
            _destination_file_path = filepath
        End Set
    End Property
    ''' <summary>
    ''' 指定したディレクトリを探索し、ファイル一覧をフルパスで返します。
    ''' </summary>
    ''' <returns>ファイル一覧</returns>
    ''' <remarks>
    ''' ファイル一覧はstring型配列で返します。
    ''' </remarks>
    Public Function make_file_list() As String()

        Try
            Dim files As String() = DirectCast(GetFiles(Me._source_file_path).ToArray(GetType(String)), String())
            Return files
        Catch ex As ApplicationException
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' 指定したディレクトリを探索し、ファイル一覧をフルパスで返します。
    ''' </summary>
    ''' <param name="path">ファイルパス</param>
    ''' <returns>ファイル一覧</returns>
    ''' <remarks>
    ''' ファイル一覧はArrayList型で返します。
    ''' アクセス権が無いファイル、フォルダは無視します。
    ''' </remarks>
    Private Function GetFiles(ByVal path As String)
        Dim _files = New ArrayList
        'パスが存在しない場合は例外発生
        If Directory.Exists(path) = False Then
            Throw New System.IO.DirectoryNotFoundException
            Return Nothing
        End If
        'ディレクトリのすべてのファイルを探索
        Try
            For Each stFilePath As String In System.IO.Directory.GetFiles(path, "*.*")
                _files.Add(stFilePath)
            Next stFilePath
        Catch ex As System.UnauthorizedAccessException
        Catch ex As ApplicationException
            MessageBox.Show(ex.Message)
        End Try
        Try
            'サブディレクトリを探索
            For Each stDirPath As String In System.IO.Directory.GetDirectories(path, "*")
                _files.AddRange(GetFiles(stDirPath))
            Next stDirPath
        Catch ex As System.UnauthorizedAccessException
        Catch ex As ApplicationException
            MessageBox.Show(ex.Message)
        End Try

        Return _files
    End Function

    ''' <summary>
    ''' ２つのファイルを比較します
    ''' </summary>
    ''' <param name="file1">ファイルパスその１</param>
    ''' <param name="file2">ファイルパスその２</param>
    ''' <returns>True:一致 False:不一致</returns>
    Public Function CompareFiles(ByVal file1 As String, ByVal file2 As String) As Boolean
        ' Set to true if the files are equal; false otherwise
        Dim filesAreEqual As Boolean = False

        With My.Computer.FileSystem
            Try
                If (.FileExists(file1) = False) OrElse (.FileExists(file2) = False) Then
                    Throw New System.IO.DirectoryNotFoundException
                End If
            Catch ex As System.IO.DirectoryNotFoundException
                Return False
            Catch ex As System.UnauthorizedAccessException
                Return False
            Catch ex As ApplicationException
                MessageBox.Show(ex.Message)
            End Try
            ' Ensure that the files are the same length before comparing them line by line.
            If .GetFileInfo(file1).Length = .GetFileInfo(file2).Length Then
                _file_size = .GetFileInfo(file1).Length
                Using file1Reader As New FileStream(file1, FileMode.Open, FileAccess.Read), _
                      file2Reader As New FileStream(file2, FileMode.Open, FileAccess.Read)
                    Dim byte1 As Integer = file1Reader.ReadByte()
                    Dim byte2 As Integer = file2Reader.ReadByte()
                    ' If byte1 or byte2 is a negative value, we have reached the end of the file.
                    _read_file_size = 0
                    '0Byteファイル特例処理
                    If (byte1 = -1 And byte2 = -1) Then
                        filesAreEqual = True
                        Return filesAreEqual
                    End If
                    While ((byte1 >= 0) AndAlso (byte2 >= 0))
                        _read_file_size += 1

                        If (byte1 <> byte2) Then
                            filesAreEqual = False
                            Exit While
                        Else
                            filesAreEqual = True
                        End If

                        ' Read the next byte.
                        byte1 = file1Reader.ReadByte()
                        byte2 = file2Reader.ReadByte()
                    End While
                End Using
            End If
        End With

        Return filesAreEqual
    End Function

End Class
