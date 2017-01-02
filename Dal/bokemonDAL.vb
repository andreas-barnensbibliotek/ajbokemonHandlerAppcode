Imports Microsoft.VisualBasic
Imports AJKontroller.webApiHelpers.bokemonHandler.Dal

Namespace AJKontroller.webApiHelpers.bokemonHandler

    Public Class bokemonDAL
        Private _connectionString As String = ConfigurationSettings.AppSettings("SiteSqlServer") 'sätt en hänvisning till connectionstring i webconfig
        Private _linqobj As New bokemonLinqDataContext(_connectionString)

        Public Function getUserbokemon(ByVal userid As Integer) As bokemonInfo
            Dim usermonsters As New bokemonInfo
            Dim totalscore As Integer = 0
            Dim usermonsterlist As New List(Of bokemonsterInfo)

        Dim monsterlist = From m In _linqobj.AJ_getMonsterlistExt(userid)
                          Select m

            For Each monst In monsterlist
                Dim mon As New bokemonsterInfo
                mon.MonId = monst.monId
                mon.BokemonSrc = monst.monstersrc
                mon.Bokemoninfo = monst.monsterinfo
                mon.Monsternamn = monst.monsternamn
                mon.BokemonScore = monst.MonsterScore
                mon.BokemonLevel = monst.monlevel
                mon.BasePoints = monst.basepoint
                mon.Bonus = monst.bonus
                totalscore += monst.MonsterScore
                usermonsterlist.Add(mon)

            Next
            usermonsters.BokemonList = usermonsterlist
            usermonsters.Monvalue = totalscore
            usermonsters.Userid = userid
            usermonsters.TimeToNext = getmonstertimetonext(userid)

            Return usermonsters

        End Function

        Public Function getAllbokemon(ByVal userid As Integer) As bokemonInfo
            Dim usermonsters As New bokemonInfo
            Dim usermonsterlist As New List(Of bokemonsterInfo)

        Dim monsterlist = From m In _linqobj.tblmonsterLists
                          Select m

            For Each monst In monsterlist
                Dim mon As New bokemonsterInfo
                mon.MonId = monst.monId
                mon.BokemonSrc = monst.monstersrc
                mon.Bokemoninfo = monst.monsterinfo
                mon.Monsternamn = monst.monsternamn
                mon.BokemonScore = getbokemonscore(monst.basepoint, monst.bonus)
                mon.BasePoints = monst.basepoint
                mon.Bonus = monst.bonus
                usermonsterlist.Add(mon)

            Next
            usermonsters.BokemonList = usermonsterlist
            usermonsters.Userid = userid
            usermonsters.TimeToNext = getmonstertimetonext(userid)

            Return usermonsters

        End Function

        Public Function getAllbokdrakar(ByVal userid As Integer) As bokemonInfo
            Dim bokdrakar As New bokemonInfo
            Dim bokdraklistan As New List(Of bokemonsterInfo)

        Dim monsterlist = From m In _linqobj.tblmonsterDrakelists
                          Select m

            For Each monst In monsterlist
                Dim mon As New bokemonsterInfo
                mon.MonId = monst.drakId
                mon.BokemonSrc = monst.draksrc
                mon.Bokemoninfo = monst.drakinfo
                mon.Monsternamn = monst.draknamn
                mon.BokemonScore = getbokemonscore(monst.drakbasepoint, monst.drakbonus)
                mon.BasePoints = monst.drakbasepoint
                mon.Bonus = monst.drakbonus
                bokdraklistan.Add(mon)

            Next
            bokdrakar.BokemonList = bokdraklistan
            bokdrakar.Userid = userid

            Return bokdrakar

        End Function

        Private Function getbokemonscore(ByVal basepoint As Integer, ByVal bonus As Integer) As Integer
            Dim ret As Integer
            If bonus > 0 Then
                ret = (bonus / 100) * basepoint
                ret = basepoint + ret
            Else
                ret = basepoint
            End If
            Return ret

        End Function
        Public Function getmonstertimetonext(ByVal userid As Integer) As Integer
            Dim ret As Integer = 0

        Dim ntn = From c In _linqobj.tblmonsters
                  Where c.userid = userid
                  Select c

            For Each usr In ntn
                ret = usr.timetonext
            Next

            Return ret
        End Function


        Public Function updMonsterTimeToNext(ByVal userid As Integer, ByVal timetn As Integer) As Boolean
            Dim ret As Boolean = False

        Dim utm = (From t In _linqobj.tblmonsters
                   Where t.userid = userid
                   Select t).First()

            Try
                utm.timetonext = timetn
                _linqobj.SubmitChanges()

                ret = True
            Catch ex As Exception
                ret = False
            End Try

            Return ret
        End Function


        Public Function addMonsterToUser(ByVal userid As Integer, ByVal monsterid As Integer) As Boolean
            Dim Inlagd As Boolean = False

            Try
                Dim utm As New tblmonsterToUser
                utm.monid = monsterid
                utm.userid = userid
                utm.monlevel = 1
                utm.MonsterScore = 1000

                _linqobj.tblmonsterToUsers.InsertOnSubmit(utm)
                _linqobj.SubmitChanges()

                Inlagd = True

            Catch ex As Exception
                Inlagd = False
            End Try

            Return Inlagd
        End Function


        Public Function updMonsterToUser(ByVal userid As Integer, ByVal monsterid As Integer, ByVal monlev As Integer, ByVal monscore As Integer) As Boolean
            Dim ret As Boolean = False

        Dim utm = (From t In _linqobj.tblmonsterToUsers
                   Where t.userid = userid And t.monid = monsterid
                   Select t).First()
            Try

                utm.monlevel = monlev
                utm.MonsterScore = monscore
                _linqobj.SubmitChanges()

                ret = True
            Catch ex As Exception
                ret = False
            End Try

            Return ret

        End Function


        

#Region "Bokemon HELPER"

        Public Function isBokemonInUserList(ByVal userid As Integer, ByVal monsterid As Integer) As Integer

            Dim ret As Integer = 0

        Dim utm = From t In _linqobj.tblmonsterToUsers
                  Where t.userid = userid And t.monid = monsterid
                  Select t
            Try
                If utm.Count > 0 Then
                    For Each mon In utm
                        ret = mon.MonsterScore
                    Next
                End If

            Catch ex As Exception
                ret = -99999
            End Try

            Return ret

        End Function

#End Region
    End Class
End Namespace