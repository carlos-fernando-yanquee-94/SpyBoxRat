Imports System.Net
''||       AUTHOR Arsium       ||
''||       github : https://github.com/arsium       ||
Public Class Countries

    Public Shared x As Integer = 0
    Public Shared Function countryinfo(ByVal aj As String)

        Try
            Dim wc As New WebClient()

            Dim data = wc.DownloadString("http://ip-api.com/json/" & aj)

            Dim op = data.Replace("""status"":""success""", "")

            Dim l = op.Replace("{", "")
            Dim k = l.Replace("}", "")

            Dim n = k.Replace(Chr(34), " ")

            Dim last = n.Replace(",", Environment.NewLine)

            If data.Contains("status") And data.Contains("success") Then

                For Each j As String In Split(last, Environment.NewLine)


                    If j.Contains("countryCode") Then
                        Dim az As String() = Split(j, ":")


                        Return az(1)



                    End If

                Next

            Else
                Return "LOCALIP"
            End If

        Catch ex As Exception


            Return "NOCONNECTIONORLOCAL"
        End Try

    End Function

    Public Shared Sub GetFlags(ByVal IPP As String, ByVal ImageLi As ImageList, ByVal ListVItem As ListView, ByVal iD As String())
        ''https://datahub.io/core/country-list#resource-data

        Dim IPPV2 As String() = Split(IPP, ":")
        Dim ReadFLG As String() = IO.File.ReadAllLines("FLGS.txt")

        ' MessageBox.Show(IPP)
        Dim test As String = countryinfo(IPPV2(0))
        ' MessageBox.Show(test)
        Dim u As Image
        Dim ja As New ListViewItem

        If test = "LOCALIP" Then
            u = Image.FromFile(Application.StartupPath & "\FLAGSV2\" & "LOCALIP" & ".png")

            ja.Text = IPP

            ImageLi.Images.Add(u)

            ja.ImageIndex = x

            x += 1
            ListVItem.Items.Add(ja)
        ElseIf test = "NOCONNECTIONORLOCAL" Then

            u = Image.FromFile(Application.StartupPath & "\FLAGSV2\" & "NOCONNECTIONORLOCAL" & ".png")

            ja.Text = IPP
            ImageLi.Images.Add(u)

            ja.ImageIndex = x

            x += 1
            ListVItem.Items.Add(ja)
        Else

            For Each i As String In ReadFLG

                If test.ToUpper.Contains(i) Or test.ToLower.Contains(i) Then

                    u = Image.FromFile(Application.StartupPath & "\FLAGSV2\" & i.ToLower & ".png")

                    ja.Text = IPP



                    ImageLi.Images.Add(u)

                    ja.ImageIndex = x

                    x += 1
                    ListVItem.Items.Add(ja)


                End If

            Next
        End If
        For Each h In iD
            '   ja.SubItems.Add("No")
            ja.SubItems.Add(iD(0))
            ja.SubItems.Add(iD(1))

            ja.SubItems.Add(iD(2))
        Next
    End Sub
End Class
