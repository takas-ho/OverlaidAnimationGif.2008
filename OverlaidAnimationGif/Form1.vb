Imports SpreadAnimationGifCellType.Ui

Public Class Form1

    Private ReadOnly overlaid As OverlaidAnimationGif

    Public Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        overlaid = New OverlaidAnimationGif(FpSpread1, My.Resources.ajax_loader_pk)

        For row As Integer = 0 To FpSpread1_Sheet1.RowCount - 1
            For column As Integer = 0 To FpSpread1_Sheet1.ColumnCount - 1
                FpSpread1_Sheet1.SetValue(row, column, String.Format("abcdefg{0}:xyzklp{1}", row, column))
            Next
        Next

        Me.Size = New Size(1200, 768)
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        overlaid.StopAnimate()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        overlaid.ShowAnimate()
    End Sub
End Class
