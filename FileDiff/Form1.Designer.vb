<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Bth_Dest = New System.Windows.Forms.Button()
        Me.Txt_Path_Dest = New System.Windows.Forms.TextBox()
        Me.Bth_Source = New System.Windows.Forms.Button()
        Me.Txt_Path_Source = New System.Windows.Forms.TextBox()
        Me.DataSet1 = New System.Data.DataSet()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(77, 62)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Location = New System.Drawing.Point(34, 97)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(485, 222)
        Me.ListView1.TabIndex = 5
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(363, 62)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Bth_Dest
        '
        Me.Bth_Dest.Location = New System.Drawing.Point(444, 35)
        Me.Bth_Dest.Name = "Bth_Dest"
        Me.Bth_Dest.Size = New System.Drawing.Size(23, 23)
        Me.Bth_Dest.TabIndex = 10
        Me.Bth_Dest.Text = "..."
        Me.Bth_Dest.UseVisualStyleBackColor = True
        '
        'Txt_Path_Dest
        '
        Me.Txt_Path_Dest.Location = New System.Drawing.Point(51, 37)
        Me.Txt_Path_Dest.Name = "Txt_Path_Dest"
        Me.Txt_Path_Dest.Size = New System.Drawing.Size(387, 19)
        Me.Txt_Path_Dest.TabIndex = 9
        '
        'Bth_Source
        '
        Me.Bth_Source.Location = New System.Drawing.Point(444, 10)
        Me.Bth_Source.Name = "Bth_Source"
        Me.Bth_Source.Size = New System.Drawing.Size(23, 23)
        Me.Bth_Source.TabIndex = 8
        Me.Bth_Source.Text = "..."
        Me.Bth_Source.UseVisualStyleBackColor = True
        '
        'Txt_Path_Source
        '
        Me.Txt_Path_Source.Location = New System.Drawing.Point(51, 12)
        Me.Txt_Path_Source.Name = "Txt_Path_Source"
        Me.Txt_Path_Source.Size = New System.Drawing.Size(387, 19)
        Me.Txt_Path_Source.TabIndex = 7
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(541, 344)
        Me.Controls.Add(Me.Bth_Dest)
        Me.Controls.Add(Me.Txt_Path_Dest)
        Me.Controls.Add(Me.Bth_Source)
        Me.Controls.Add(Me.Txt_Path_Source)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Bth_Dest As System.Windows.Forms.Button
    Friend WithEvents Txt_Path_Dest As System.Windows.Forms.TextBox
    Friend WithEvents Bth_Source As System.Windows.Forms.Button
    Friend WithEvents Txt_Path_Source As System.Windows.Forms.TextBox
    Friend WithEvents DataSet1 As System.Data.DataSet

End Class
