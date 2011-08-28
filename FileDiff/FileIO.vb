Imports System.IO

Public Class FileIO


    Private _source_file_path As String = String.Empty
    Private _destination_file_path As String = String.Empty

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
    Public Function make_file_list() As String()

        Try
            Dim files As String() = DirectCast(GetFiles(Me._source_file_path).ToArray(GetType(String)), String())
            Return files
        Catch ex As ApplicationException
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try

    End Function

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


    Public Function CompareFiles(ByVal file1 As String, ByVal file2 As String) As Boolean
        ' Set to true if the files are equal; false otherwise
        Dim filesAreEqual As Boolean = False

        With My.Computer.FileSystem
            ' Ensure that the files are the same length before comparing them line by line.
            If .GetFileInfo(file1).Length = .GetFileInfo(file2).Length Then
                Using file1Reader As New FileStream(file1, FileMode.Open), _
                      file2Reader As New FileStream(file2, FileMode.Open)
                    Dim byte1 As Integer = file1Reader.ReadByte()
                    Dim byte2 As Integer = file2Reader.ReadByte()
                    ' If byte1 or byte2 is a negative value, we have reached the end of the file.
                    While byte1 >= 0 AndAlso byte2 >= 0
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
