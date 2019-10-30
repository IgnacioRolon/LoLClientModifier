
Public Class frm_Runes
    Private runesDirectory As String
    Private keyArray, firstRuneArray, secondRuneArray, thirdRuneArray As List(Of String)
    Private runeArray(), keystoneArray() As Rune
    Private pathArray As List(Of SelectedPath)
    Private newArray(5) As SelectedPath
    Private Sub frm_Runes_Load(sender As Object, e As EventArgs) Handles Me.Load
        runesDirectory = My.Application.Info.DirectoryPath + "\Resources\Runes\"



        Dim currentRune As Rune = New Rune()
        Dim currentPath As SelectedPath = New SelectedPath()


        Dim Paths() As String = {"Precision", "Domination", "Resolve", "Sorcery", "Inspiration"}
        For i = 0 To 4
            newArray(i) = New SelectedPath()
            currentPath.RunePath = runesDirectory + Paths(i) + ".png"
            currentPath.RuneName = Paths(i)
            currentPath.RuneImage = Image.FromFile(currentPath.RunePath)
            newArray(i) = currentPath
        Next




        'For Each itemPath As SelectedPath In newArray
        '    For i = 1 To 4 'Loading all Keystones
        '        currentRune.RunePath = runesDirectory + itemPath.RuneName + "_Rune" + i.ToString + "_Key.png"
        '        If System.IO.File.Exists(currentRune.RunePath) Then
        '            currentRune.RuneSelectedPath = itemPath
        '            currentRune.RuneImage = Image.FromFile(currentRune.RunePath)
        '            keystoneArray.Add(currentRune)
        '        End If
        '    Next
        '    For i = 1 To 3
        '        For j = 1 To 4 'Loading all runes
        '            currentRune.RunePath = runesDirectory + itemPath.RuneName + "_Rune" + j.ToString + "_P" + i.ToString + ".png"
        '            If System.IO.File.Exists(currentRune.RunePath) Then
        '                currentRune.RuneImage = Image.FromFile(currentRune.RunePath)
        '                currentRune.RuneSelectedPath = itemPath
        '                runeArray.Add(currentRune)
        '            End If
        '        Next
        '    Next
        'Next
        'Dim flag As Boolean = True
    End Sub

    Private Sub ShowMainRunePath(selectedRune As PictureBox) 'Show the runes in the main path based on the chosen rune path
        Dim imagePath As String
        keyArray = New List(Of String)
        firstRuneArray = New List(Of String)
        secondRuneArray = New List(Of String)
        thirdRuneArray = New List(Of String)

        For i = 1 To 4 'Loading all Keystones
            imagePath = runesDirectory + selectedRune.Tag + "_Rune" + i.ToString + "_Key.png"
            If System.IO.File.Exists(imagePath) Then
                keyArray.Add(imagePath)
            End If
        Next

        For i = 1 To 3 'Loading all other Runes
            For j = 1 To 4 'Loading each rune array
                imagePath = runesDirectory + selectedRune.Tag + "_Rune" + j.ToString + "_P" + i.ToString + ".png"
                If System.IO.File.Exists(imagePath) Then
                    Select Case i
                        Case 1
                            firstRuneArray.Add(imagePath)
                        Case 2
                            secondRuneArray.Add(imagePath)
                        Case 3
                            thirdRuneArray.Add(imagePath)
                    End Select
                End If
            Next
        Next

        Dim currentIndex As Integer
        Dim item As Control
        Dim pictureItem As PictureBox

        For i = 0 To pan_keyRunes.Controls.Count - 1 'Take the panel in reverse and add the items
            currentIndex = (pan_keyRunes.Controls.Count - 1) - i
            item = pan_keyRunes.Controls.Item(currentIndex)
            pictureItem = item
            If keyArray.Count > 0 Then
                pictureItem.Image = Image.FromFile(keyArray.Item(0))
                item.Tag = keyArray.Item(0)
                keyArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For i = 0 To pan_runes1.Controls.Count - 1
            currentIndex = (pan_runes1.Controls.Count - 1) - i
            item = pan_runes1.Controls.Item(currentIndex)
            pictureItem = item
            If firstRuneArray.Count > 0 Then
                pictureItem.Image = Image.FromFile(firstRuneArray.Item(0))
                item.Tag = firstRuneArray.Item(0)
                firstRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For i = 0 To pan_runes2.Controls.Count - 1
            currentIndex = (pan_runes2.Controls.Count - 1) - i
            item = pan_runes2.Controls.Item(currentIndex)
            pictureItem = item
            If secondRuneArray.Count > 0 Then
                pictureItem.Image = Image.FromFile(secondRuneArray.Item(0))
                item.Tag = secondRuneArray.Item(0)
                secondRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For i = 0 To pan_runes3.Controls.Count - 1
            currentIndex = (pan_runes3.Controls.Count - 1) - i
            item = pan_runes3.Controls.Item(currentIndex)
            pictureItem = item
            If thirdRuneArray.Count > 0 Then
                pictureItem.Image = Image.FromFile(thirdRuneArray.Item(0))
                item.Tag = thirdRuneArray.Item(0)
                thirdRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For Each control As Control In Controls 'Show the full chosen path
            control.Show()
        Next

        For Each image As PictureBox In pan_selectedRunes.Controls 'Empty the selected runes images.
            image.Image = Nothing
            image.Tag = Nothing
        Next
        pan_mainPath.Hide()

        Dim Runes As New List(Of Image)
        Runes = New List(Of Image)
        For Each currentRune As PictureBox In pan_mainPath.Controls
            If currentRune.Tag <> pcb_selectedPath.Tag Then
                Runes.Add(currentRune.Image)
            End If
        Next


    End Sub
    Private Sub ChangeRune(runeBox As PictureBox, selectedRune As PictureBox, panel As Panel)
        runeBox.Image = Image.FromFile(selectedRune.Tag)
        panel.Hide()
    End Sub

    Private Sub Pcb_selectedKey_Click(sender As Object, e As EventArgs) Handles pcb_selectedKey.Click
        If pan_keyRunes.Visible = True Then
            pan_keyRunes.Hide()
        Else
            pan_keyRunes.Show()
        End If
    End Sub

    Private Sub Pcb_key1_Click(sender As Object, e As EventArgs) Handles pcb_key1.Click
        ChangeRune(pcb_selectedKey, pcb_key1, pan_keyRunes)
    End Sub

    Private Sub Pcb_key2_Click(sender As Object, e As EventArgs) Handles pcb_key2.Click
        ChangeRune(pcb_selectedKey, pcb_key2, pan_keyRunes)
    End Sub

    Private Sub Pcb_key3_Click(sender As Object, e As EventArgs) Handles pcb_key3.Click
        ChangeRune(pcb_selectedKey, pcb_key3, pan_keyRunes)
    End Sub

    Private Sub Pcb_key4_Click(sender As Object, e As EventArgs) Handles pcb_key4.Click
        ChangeRune(pcb_selectedKey, pcb_key4, pan_keyRunes)
    End Sub

    Private Sub Pcb_precisionPath_Click(sender As Object, e As EventArgs) Handles pcb_precisionPath.Click
        ShowMainRunePath(pcb_precisionPath)
        pcb_selectedPath.Image = pcb_precisionPath.Image
        pcb_selectedPath.Tag = pcb_precisionPath.Tag
    End Sub

    Private Sub Pcb_dominationPath_Click(sender As Object, e As EventArgs) Handles pcb_dominationPath.Click
        ShowMainRunePath(pcb_dominationPath)
        pcb_selectedPath.Image = pcb_dominationPath.Image
        pcb_selectedPath.Tag = pcb_dominationPath.Tag
    End Sub

    Private Sub Pcb_rune1P1_Click(sender As Object, e As EventArgs) Handles pcb_rune1P1.Click
        ChangeRune(pcb_selectedRune1, pcb_rune1P1, pan_runes1)
    End Sub

    Private Sub Pcb_rune2P1_Click(sender As Object, e As EventArgs) Handles pcb_rune2P1.Click
        ChangeRune(pcb_selectedRune1, pcb_rune2P1, pan_runes1)
    End Sub

    Private Sub Pcb_rune3P1_Click(sender As Object, e As EventArgs) Handles pcb_rune3P1.Click
        ChangeRune(pcb_selectedRune1, pcb_rune3P1, pan_runes1)
    End Sub

    Private Sub Pcb_rune4P1_Click(sender As Object, e As EventArgs) Handles pcb_rune4P1.Click
        ChangeRune(pcb_selectedRune1, pcb_rune4P1, pan_runes1)
    End Sub

    Private Sub Pcb_rune1P2_Click(sender As Object, e As EventArgs) Handles pcb_rune1P2.Click
        ChangeRune(pcb_selectedRune2, pcb_rune1P2, pan_runes2)
    End Sub

    Private Sub Pcb_rune2P2_Click(sender As Object, e As EventArgs) Handles pcb_rune2P2.Click
        ChangeRune(pcb_selectedRune2, pcb_rune2P2, pan_runes2)
    End Sub

    Private Sub Pcb_rune3P2_Click(sender As Object, e As EventArgs) Handles pcb_rune3P2.Click
        ChangeRune(pcb_selectedRune2, pcb_rune3P2, pan_runes2)
    End Sub

    Private Sub Pcb_rune4P2_Click(sender As Object, e As EventArgs) Handles pcb_rune4P2.Click
        ChangeRune(pcb_selectedRune2, pcb_rune4P2, pan_runes2)
    End Sub

    Private Sub Pcb_rune1P3_Click(sender As Object, e As EventArgs) Handles pcb_rune1P3.Click
        ChangeRune(pcb_selectedRune3, pcb_rune1P3, pan_runes3)
    End Sub

    Private Sub Pcb_rune2P3_Click(sender As Object, e As EventArgs) Handles pcb_rune2P3.Click
        ChangeRune(pcb_selectedRune3, pcb_rune2P3, pan_runes3)
    End Sub

    Private Sub Pcb_rune3P3_Click(sender As Object, e As EventArgs) Handles pcb_rune3P3.Click
        ChangeRune(pcb_selectedRune3, pcb_rune3P3, pan_runes3)
    End Sub

    Private Sub Pcb_rune4P3_Click(sender As Object, e As EventArgs) Handles pcb_rune4P3.Click
        ChangeRune(pcb_selectedRune3, pcb_rune4P3, pan_runes3)
    End Sub

    Private Sub Pcb_selectedPath_Click(sender As Object, e As EventArgs) Handles pcb_selectedPath.Click
        If pan_mainPath.Visible = True Then
            pan_mainPath.Hide()
        Else
            pan_mainPath.Show()
        End If
    End Sub

    Private Sub Pcb_selectedRune1_Click(sender As Object, e As EventArgs) Handles pcb_selectedRune1.Click
        If pan_runes1.Visible = True Then
            pan_runes1.Hide()
        Else
            pan_runes1.Show()
        End If
    End Sub

    Private Sub Pcb_selectedRune2_Click(sender As Object, e As EventArgs) Handles pcb_selectedRune2.Click
        If pan_runes2.Visible = True Then
            pan_runes2.Hide()
        Else
            pan_runes2.Show()
        End If
    End Sub

    Private Sub Pcb_selectedRune3_Click(sender As Object, e As EventArgs) Handles pcb_selectedRune3.Click
        If pan_runes3.Visible = True Then
            pan_runes3.Hide()
        Else
            pan_runes3.Show()
        End If
    End Sub

    Private Sub Pcb_resolvePath_Click(sender As Object, e As EventArgs) Handles pcb_resolvePath.Click
        ShowMainRunePath(pcb_resolvePath)
        pcb_selectedPath.Image = pcb_resolvePath.Image
        pcb_selectedPath.Tag = pcb_resolvePath.Tag
    End Sub

    Private Sub Pcb_sorceryPath_Click(sender As Object, e As EventArgs) Handles pcb_sorceryPath.Click
        ShowMainRunePath(pcb_sorceryPath)
        pcb_selectedPath.Image = pcb_sorceryPath.Image
        pcb_selectedPath.Tag = pcb_sorceryPath.Tag
    End Sub

    Private Sub Pcb_inspirationPath_Click(sender As Object, e As EventArgs) Handles pcb_inspirationPath.Click
        ShowMainRunePath(pcb_inspirationPath)
        pcb_selectedPath.Image = pcb_inspirationPath.Image
        pcb_selectedPath.Tag = pcb_inspirationPath.Tag
    End Sub
End Class

Public Class Rune
    Public RuneImage As Image
    Public RunePath As String
    Public RuneSelectedPath As SelectedPath
End Class

Public Class SelectedPath
    Public RuneImage As Image
    Public RuneName As String
    Public RunePath As String
End Class