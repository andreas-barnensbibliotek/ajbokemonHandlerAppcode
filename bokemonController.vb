Imports Microsoft.VisualBasic
Imports System.DateTime
Imports AJKontroller.webApiHelpers.bokemonHandler.Dal

'filerna ligger på github:
'git remote add origin https://github.com/andreas-barnensbibliotek/ajbokemonHandlerAppcode.git
'git push -u origin master


Namespace AJKontroller.webApiHelpers.bokemonHandler
    Public Class bokemonController
        Public Function getbaseUserAndMonsterList(ByVal userid As Integer) As bokemonInfo
            Dim retobj As New bokemonInfo
            Dim dalobj As New bokemonDAL
            Try
                retobj = dalobj.getUserbokemon(userid)
                retobj.Status = "All user bokemon"
            Catch ex As Exception
                retobj.Status = "nått blev fel"
            End Try

            Return retobj

        End Function

        Public Function getbaseAllMonsterList(ByVal userid As Integer) As bokemonInfo
            Dim retobj As New bokemonInfo
            Dim dalobj As New bokemonDAL
            Try
                retobj = dalobj.getAllbokemon(userid)
                retobj.Status = "All bokemons"
            Catch ex As Exception
                retobj.Status = "nått blev fel"
            End Try

            Return retobj

        End Function



        Public Function getbaseAllDrakarList(ByVal userid As Integer) As bokemonInfo
            Dim retobj As New bokemonInfo
            Dim dalobj As New bokemonDAL
            Try
                retobj = dalobj.getAllbokdrakar(userid)
                retobj.Status = "Alla bokdrakar"
            Catch ex As Exception
                retobj.Status = "nått blev fel"
            End Try

            Return retobj

        End Function


        Public Function MonsterToUserList(ByVal userid As Integer, ByVal monsterid As Integer) As bokemonInfo
            Dim retobj As New bokemonInfo
            Dim dalobj As New bokemonDAL
            Dim currentMonsterXP As Integer = 0
            Dim currentMonsterScore As Integer = 0
            Dim MonsterMaxPoint As Integer = 0


            If monsterid > 0 Then

                currentMonsterXP = dalobj.isBokemonInUserList(userid, monsterid) 'monsterxp är samma som monsterscore tills monsterscore når maxpoint, sedan ökar bara monsterxp
                MonsterMaxPoint = dalobj.getBokemonmaxpoint(monsterid)

                If currentMonsterXP > 0 Then

                    Dim newmonsXP As Integer = calcMonsterScore("upd", currentMonsterXP)
                    If newmonsXP <= MonsterMaxPoint Then
                        currentMonsterScore = newmonsXP
                    Else
                        currentMonsterScore = MonsterMaxPoint
                    End If

                    Dim newlevel As Integer = calcMonsterLevel(currentMonsterScore)

                    Dim isupdated As Boolean = dalobj.updMonsterToUser(userid, monsterid, newlevel, currentMonsterScore, newmonsXP)
                    retobj = dalobj.getUserbokemon(userid)

                    If isupdated Then
                        retobj.Status = "Monster updated to user: " & userid
                    Else
                        retobj.Status = "Nått blev fel när monster skulle uppdateras till user" & userid
                    End If

                Else
                    Dim isadded As Boolean = dalobj.addMonsterToUser(userid, monsterid)
                    retobj = dalobj.getUserbokemon(userid)
                    If isadded Then
                        retobj.Status = "Monster added to user: " & userid
                    Else
                        retobj.Status = "Nått blev fel när monster skulle läggas till user" & userid
                    End If

                End If
            Else

                retobj.Status = "Nått blev fel när monster skulle läggas till. Monsterid= " & monsterid
            End If
            'addTimetoNext(userid, timetn)

            Return retobj

        End Function


        Private Function calcMonsterScore(ByVal typ As String, ByVal monScore As Integer, Optional ByVal lev As Integer = 1) As Integer
            Dim retval As Integer

            Select Case typ
                Case "upd"
                    retval = monScore + 200 'monScore * 1.2 'öka med 200poäng

                Case "vinn"
                    retval = (200 * lev) / 2 ' öka med 200 gånger bokmalslevel dela sedan med 2
                    retval += monScore ' lägg till det tidigare monsterscorevärdet

                Case "lose"
                    If monScore >= 1000 Then
                        If monScore < 1200 Then
                            retval = 1000
                        Else
                            retval = -200
                        End If
                    Else
                        retval = monScore
                    End If

            End Select

            Return retval
        End Function

        Private Function calcMonsterLevel(ByVal monScore As Integer) As Integer
            Dim retval As Integer
            If monScore >= 1000 Then
                Dim tmplv = monScore / 1000
                retval = Math.Round(tmplv, 0)
            Else
                retval = 1
            End If

            Return retval
        End Function


    End Class
End Namespace