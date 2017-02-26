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
    Dim SCALAR_YELLOW As New MCvScalar(0.0, 255.0, 255.0)

    Const MIN_PIXEL_WIDTH As Integer = 10
    Const MIN_PIXEL_HEIGHT As Integer = 10
    Const MAX_ASPECT_RATIO As Double = 0.8
    Const MIN_PIXEL_AREA As Integer = 80

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        
        'declare and show an OpenFileDialog
        Dim drChosenFile As DialogResult
        drChosenFile = ofdOpenFile.ShowDialog()                 'open file dialog

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
    
        'declare and find traffic cones
        Dim trafficCones As New VectorOfVectorOfPoint()
        trafficCones = findTrafficCones(imgOriginal)              'function call

        'clone original image so we don't have to alter original image
        Dim imgOriginalWithCones As Mat = imgOriginal.Clone()

        'draw yellow convex hull around outside of cones
        CvInvoke.DrawContours(imgOriginalWithCones, trafficCones, -1, SCALAR_YELLOW)

        'for each traffic cone, draw a small green dot at the center of mass of the cone
        For i As Integer = 0 To trafficCones.Size - 1
            drawGreenDotAtConeCenter(trafficCones(i), imgOriginalWithCones)
        Next

        'update image on form
        ibMain.Image = imgOriginalWithCones
        CvInvoke.Imshow("imgOriginalWithCones", imgOriginalWithCones)

        'write applicable info to text box
        txtInfo.AppendText("---------------------------------------" + vbCrLf)
        If (trafficCones Is Nothing) Then
            txtInfo.AppendText("no traffic cones were found" + vbCrLf)
        ElseIf (trafficCones.Size <= 0) Then
            txtInfo.AppendText("no traffic cones were found" + vbCrLf)
        ElseIf (trafficCones.Size = 1) Then
            txtInfo.AppendText("1 traffic cone was found" + vbCrLf)
        ElseIf (trafficCones.Size > 1) Then
            txtInfo.AppendText(trafficCones.Size.ToString() + " traffic cones were found" + vbCrLf)
        End If
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function findTrafficCones(imgOriginal As Mat) As VectorOfVectorOfPoint
        'declare images
        Dim imgHSV As New Mat()
        Dim imgThreshLow As New Mat()
        Dim imgThreshHigh As New Mat()
        Dim imgThresh As New Mat()
        Dim imgThreshSmoothed As New Mat()
        Dim imgCanny As New Mat()
        Dim imgContours As New Mat()
        Dim imgAllConvexHulls As New Mat()
        Dim imgTrafficCones As New Mat()

        'declare contours
        Dim contours As New VectorOfVectorOfPoint()
        Dim trafficCones As New VectorOfVectorOfPoint()     'this will be the return value

        'convert to HSV color space
        CvInvoke.CvtColor(imgOriginal, imgHSV, ColorConversion.Bgr2Hsv)
        CvInvoke.Imshow("imgHSV", imgHSV)
        
        'threshold on low range of HSV red
        CvInvoke.InRange(imgHSV, New ScalarArray(New MCvScalar(0, 135, 135)), New ScalarArray(New MCvScalar(15, 255, 255)), imgThreshLow)
        'threshold on high range of HSV red
        CvInvoke.InRange(imgHSV, New ScalarArray(New MCvScalar(159, 135, 135)), New ScalarArray(New MCvScalar(179, 255, 255)), imgThreshHigh)
        'combine (i.e. add) low and high thresh images
        CvInvoke.Add(imgThreshLow, imgThreshHigh, imgThresh)
        CvInvoke.Imshow("imgThresh", imgThresh)
        
        'open image (erode, then dilate)
        imgThreshSmoothed = imgThresh.Clone()        
        Dim structuringElement3x3 As Mat = CvInvoke.GetStructuringElement(ElementShape.Rectangle, New Size(3, 3), New Point(-1, -1))
        CvInvoke.Erode(imgThreshSmoothed, imgThreshSmoothed, structuringElement3x3, New Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue)
        CvInvoke.Dilate(imgThreshSmoothed, imgThreshSmoothed, structuringElement3x3, New Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue)
        
        'smooth image (i.e. Gaussian blur)
        CvInvoke.GaussianBlur(imgThreshSmoothed, imgThreshSmoothed, New Size(3, 3), 0)

        'find Canny edges
        CvInvoke.Canny(imgThreshSmoothed, imgCanny, 80, 160)
        CvInvoke.Imshow("imgCanny", imgCanny)

        'find and draw contours
        CvInvoke.FindContours(imgCanny.Clone(), contours, Nothing, RetrType.External, ChainApproxMethod.ChainApproxSimple)
        imgContours = New Mat(imgOriginal.Size, DepthType.Cv8U, 3)
        CvInvoke.DrawContours(imgContours, contours, -1, SCALAR_WHITE)
        CvInvoke.Imshow("imgContours", imgContours)
        
        'find convex hulls
        Dim allConvexHulls As New VectorOfVectorOfPoint()        
        For i As Integer = 0 To contours.Size - 1            
            Dim convexHull As New VectorOfPoint()
            CvInvoke.ConvexHull(contours(i), convexHull)
            allConvexHulls.Push(convexHull)
        Next

        'show convex hulls
        imgAllConvexHulls = New Mat(imgOriginal.Size, DepthType.Cv8U, 3)
        CvInvoke.DrawContours(imgAllConvexHulls, allConvexHulls, -1, SCALAR_WHITE)
        CvInvoke.Imshow("imgAllConvexHulls", imgAllConvexHulls)
        
        'loop through convex hulls, check if each is a traffic cone, add to vector of traffic cones if it is
        For i As Integer = 0 To allConvexHulls.Size - 1
            Dim currentConvexHull As VectorOfPoint = allConvexHulls(i)
            If (isTrafficCone(currentConvexHull)) Then
                trafficCones.Push(currentConvexHull)
            End If
        Next

        'draw the traffic cone contours
        imgTrafficCones = New Mat(imgOriginal.Size, DepthType.Cv8U, 3)
        CvInvoke.DrawContours(imgTrafficCones, trafficCones, -1, SCALAR_WHITE)
        CvInvoke.Imshow("imgTrafficCones", imgTrafficCones)
        
        Return trafficCones
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function isTrafficCone(convexHull As VectorOfPoint) As Boolean

        'first get dimensional information for the convex hull (bounding rect, bounding rect area, bounding rect aspect ratio, and y center
        Dim boundingRect As Rectangle = CvInvoke.BoundingRectangle(convexHull)
        Dim area As Integer = boundingRect.Width * boundingRect.Height
        Dim aspectRatio As Double = CDbl(boundingRect.Width) / CDbl(boundingRect.Height)
        Dim yCenter = boundingRect.Y + CInt(CDbl(boundingRect.Height) / 2.0)

        'first do a gross dimensional check, return false if convex hull does not pass
        If (area < MIN_PIXEL_AREA OrElse boundingRect.Width < MIN_PIXEL_WIDTH OrElse boundingRect.Height < MIN_PIXEL_HEIGHT OrElse aspectRatio > MAX_ASPECT_RATIO) Then
            Return False
        End If
        
        'now check if the convex Hull is pointing up

        'declare and populate a vector of all points above the y center, and all points below the y center
        Dim pointsAboveCenter As New List(Of Point)
        Dim pointsBelowCenter As New List(Of Point)
        For i As Integer = 0 To convexHull.Size - 1
            Dim currentPoint As Point = convexHull(i)
            If (currentPoint.Y < yCenter) Then
                pointsAboveCenter.Add(currentPoint)
            Else
                pointsBelowCenter.Add(currentPoint)
            End If
        Next
        
        'find the left most point below the y center
        Dim leftMostPointBelowCenter As Integer = pointsBelowCenter(0).X
        For Each point As Point In pointsBelowCenter
            If (point.X < leftMostPointBelowCenter) Then leftMostPointBelowCenter = point.X
        Next

        'find the right most point below the y center
        Dim rightMostPointBelowCenter As Integer = pointsBelowCenter(0).X
        For Each point As Point In pointsBelowCenter
            If (point.X > rightMostPointBelowCenter) Then rightMostPointBelowCenter = point.X
        Next

        'step through all the points above the y center
        For Each pointAboveCenter As Point In pointsAboveCenter
            'if any point above the y center is farther left or right than the extreme left and right below y center points, then the convex hull is not pointing up, so return false            
            If (pointAboveCenter.X <= leftMostPointBelowCenter OrElse pointAboveCenter.X >= rightMostPointBelowCenter) Then Return False
        Next        
        'if we get here, the convex hull has passed the gross dimensional check and the pointing up check, so we're convinced its a cone, so return true        
        Return True
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub drawGreenDotAtConeCenter(trafficCone As VectorOfPoint, image As Mat)
        'find the contour moments
        Dim moments As MCvMoments = CvInvoke.Moments(trafficCone)

        'using the moments, find the center of mass
        Dim centerAsDouble As MCvPoint2D64f = moments.GravityCenter
        Dim xCenter As Integer = CInt(centerAsDouble.X)
        Dim yCenter As Integer = CInt(centerAsDouble.Y)

        'draw the small green circle
        CvInvoke.Circle(image, New Point(xCenter, yCenter), 3, SCALAR_GREEN, -1)        
    End Sub

End Class

