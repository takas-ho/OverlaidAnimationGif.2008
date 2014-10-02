Namespace Ui
    ''' <summary>
    ''' アニメーションGIFの重ね表示/停止を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OverlaidAnimationGif

        Private ReadOnly animationGif As Bitmap
        Private ReadOnly overlayee As Control
        Private ReadOnly location As Point
        Private ReadOnly isDisplayMiddle As Boolean

        ''' <summary>
        ''' アニメーションGIFを重ねて表示する
        ''' </summary>
        ''' <param name="overlayee">アニメーションGIFを重ねるコントロール</param>
        ''' <param name="animationGif">アニメーションGIF</param>
        ''' <remarks></remarks>
        Public Shared Sub Bind(ByVal overlayee As Control, ByVal animationGif As Bitmap)
            Bind(overlayee, animationGif, Nothing)
        End Sub

        ''' <summary>
        ''' アニメーションGIFを重ねて表示する
        ''' </summary>
        ''' <param name="overlayee">アニメーションGIFを重ねるコントロール</param>
        ''' <param name="animationGif">アニメーションGIF</param>
        ''' <param name="location">GIFの表示位置 ※省略時はoverlayeeの中央</param>
        ''' <remarks></remarks>
        Public Shared Sub Bind(ByVal overlayee As Control, ByVal animationGif As Bitmap, ByVal location As Point?)
            Dim overlaid As New OverlaidAnimationGif(overlayee, animationGif, location)
            overlaid.ShowAnimate()
        End Sub

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="overlayee">アニメーションGIFを重ねるコントロール</param>
        ''' <param name="animationGif">アニメーションGIF</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal overlayee As Control, ByVal animationGif As Bitmap)
            Me.New(overlayee, animationGif, Nothing)
        End Sub

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="overlayee">アニメーションGIFを重ねるコントロール</param>
        ''' <param name="animationGif">アニメーションGIF</param>
        ''' <param name="location">GIFの表示位置 ※省略時はoverlayeeの中</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal overlayee As Control, ByVal animationGif As Bitmap, ByVal location As Point?)
            Me.animationGif = animationGif
            Me.overlayee = overlayee
            Me.location = If(location, New Point(0, 0))
            Me.isDisplayMiddle = Not location.HasValue
        End Sub

        ''' <summary>
        ''' 表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ShowAnimate()
            AddHandler overlayee.Paint, AddressOf Me.Overlayee_Paint
            ImageAnimator.Animate(animationGif, AddressOf Me.Image_FrameChanged)
        End Sub

        ''' <summary>
        ''' 表示を止める
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StopAnimate()
            ImageAnimator.StopAnimate(animationGif, AddressOf Me.Image_FrameChanged)
            RemoveHandler overlayee.Paint, AddressOf Me.Overlayee_Paint
            overlayee.Invalidate()
        End Sub

        Private Sub Overlayee_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
            ImageAnimator.UpdateFrames(animationGif)
            If isDisplayMiddle Then
                e.Graphics.DrawImage(animationGif, _
                                     CInt((overlayee.Size.Width - animationGif.Size.Width) / 2), _
                                     CInt((overlayee.Size.Height - animationGif.Size.Height) / 2))
            Else
                e.Graphics.DrawImage(animationGif, location.X, location.Y)
            End If
        End Sub

        Private Sub Image_FrameChanged(ByVal o As Object, ByVal e As EventArgs)
            overlayee.Invalidate()
        End Sub
    End Class
End Namespace