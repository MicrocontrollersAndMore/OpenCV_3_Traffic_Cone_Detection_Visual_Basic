'frmMain.vb

Option Explicit On      'require explicit declaration of variables, this is NOT Python !!
Option Strict On        'restrict implicit data type conversions to only widening conversions

'using EmguCv 3.1.0.1 NuGet package
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure
Imports Emgu.CV.UI
Imports Emgu.CV.Util

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Class frmMain

    ' member variables ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim SCALAR_BLACK As New MCvScalar(0.0, 0.0, 0.0)
    Dim SCALAR_WHITE As New MCvScalar(255.0, 255.0, 255.0)
    Dim SCALAR_BLUE As New MCvScalar(255.0, 0.0, 0.0)
    Dim SCALAR_GREEN As New MCvScalar(0.0, 200.0, 0.0)
    Dim SCALAR_RED As New MCvScalar(0.0, 0.0, 255.0)
    
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Dim drChosenFile As DialogResult

        If (drChosenFile <> System.Windows.Forms.DialogResult.OK Or ofdOpenFile.FileName = "") Then    'if user chose Cancel or filename is blank . . .
            lblChosenFile.Text = "file not chosen"              'show error message on label
            Return                                              'and exit function
        End If

        Dim imgOriginal As New Mat()

        Try
            imgOriginal = New Mat(ofdOpenFile.FileName, LoadImageType.Color)
        Catch ex As Exception                                                       'if error occurred
            lblChosenFile.Text = "unable to open image, error: " + ex.Message       'show error message on label
            Return                                                                  'and exit function
        End Try

        If (imgOriginal Is Nothing) Then                                  'if image could not be opened
            lblChosenFile.Text = "unable to open image"                 'show error message on label
            Return                                                      'and exit function
        End If

        If (imgOriginal.IsEmpty()) Then                                    'if image opened as empty
            lblChosenFile.Text = "unable to open image, image was empty"        'show error message on label
            Return                                                        'and exit function
        End If

        lblChosenFile.Text = ofdOpenFile.FileName           'update label with file name

        CvInvoke.DestroyAllWindows()                        'close any windows that are open from previous button press

        ibMain.Image = imgOriginal                 'show original image on main form

        Dim trafficCones As New VectorOfVectorOfPoint()
        trafficCones = findTrafficCones(imgOriginal)



    End Sub








End Class

