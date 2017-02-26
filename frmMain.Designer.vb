<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ibMain = New Emgu.CV.UI.ImageBox()
        Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.lblChosenFile = New System.Windows.Forms.Label()
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.ofdOpenFile = New System.Windows.Forms.OpenFileDialog()
        CType(Me.ibMain,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tableLayoutPanel.SuspendLayout
        Me.SuspendLayout
        '
        'ibMain
        '
        Me.ibMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tableLayoutPanel.SetColumnSpan(Me.ibMain, 2)
        Me.ibMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ibMain.Enabled = false
        Me.ibMain.Location = New System.Drawing.Point(3, 44)
        Me.ibMain.Name = "ibMain"
        Me.ibMain.Size = New System.Drawing.Size(999, 616)
        Me.ibMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ibMain.TabIndex = 2
        Me.ibMain.TabStop = false
        '
        'tableLayoutPanel
        '
        Me.tableLayoutPanel.ColumnCount = 2
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tableLayoutPanel.Controls.Add(Me.ibMain, 0, 1)
        Me.tableLayoutPanel.Controls.Add(Me.btnOpenFile, 0, 0)
        Me.tableLayoutPanel.Controls.Add(Me.lblChosenFile, 1, 0)
        Me.tableLayoutPanel.Controls.Add(Me.txtInfo, 0, 2)
        Me.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.tableLayoutPanel.Name = "tableLayoutPanel"
        Me.tableLayoutPanel.RowCount = 3
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87!))
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13!))
        Me.tableLayoutPanel.Size = New System.Drawing.Size(1005, 757)
        Me.tableLayoutPanel.TabIndex = 3
        '
        'btnOpenFile
        '
        Me.btnOpenFile.AutoSize = true
        Me.btnOpenFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOpenFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnOpenFile.Location = New System.Drawing.Point(3, 3)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(107, 35)
        Me.btnOpenFile.TabIndex = 3
        Me.btnOpenFile.Text = "Open File"
        Me.btnOpenFile.UseVisualStyleBackColor = true
        '
        'lblChosenFile
        '
        Me.lblChosenFile.AutoSize = true
        Me.lblChosenFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblChosenFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblChosenFile.Location = New System.Drawing.Point(116, 0)
        Me.lblChosenFile.Name = "lblChosenFile"
        Me.lblChosenFile.Size = New System.Drawing.Size(886, 41)
        Me.lblChosenFile.TabIndex = 4
        Me.lblChosenFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInfo
        '
        Me.tableLayoutPanel.SetColumnSpan(Me.txtInfo, 2)
        Me.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInfo.Font = New System.Drawing.Font("Courier New", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtInfo.Location = New System.Drawing.Point(3, 666)
        Me.txtInfo.Multiline = true
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtInfo.Size = New System.Drawing.Size(999, 88)
        Me.txtInfo.TabIndex = 5
        Me.txtInfo.WordWrap = false
        '
        'ofdOpenFile
        '
        Me.ofdOpenFile.FileName = "OpenFileDialog1"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1005, 757)
        Me.Controls.Add(Me.tableLayoutPanel)
        Me.Name = "frmMain"
        Me.Text = "Form1"
        CType(Me.ibMain,System.ComponentModel.ISupportInitialize).EndInit
        Me.tableLayoutPanel.ResumeLayout(false)
        Me.tableLayoutPanel.PerformLayout
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents ibMain As Emgu.CV.UI.ImageBox
    Friend WithEvents tableLayoutPanel As TableLayoutPanel
    Friend WithEvents btnOpenFile As Button
    Friend WithEvents lblChosenFile As Label
    Friend WithEvents txtInfo As TextBox
    Friend WithEvents ofdOpenFile As OpenFileDialog
End Class
