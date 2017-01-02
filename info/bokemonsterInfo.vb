Imports Microsoft.VisualBasic

Public Class bokemonsterInfo
    Public Sub New()
        _bokmonID = 0
        _bokemonSrc = ""
        _bokemonInfo = ""
        _monstername = ""
        _bokemonScore = 0
        _bokemonXP = 0
        _bokemonlevel = 1
        _basepoint = 0
        _bonus = 0
    End Sub
    Private _bokmonID As Integer
    Public Property MonId() As Integer
        Get
            Return _bokmonID
        End Get
        Set(ByVal value As Integer)
            _bokmonID = value
        End Set
    End Property
    Private _monstername As String
    Public Property Monsternamn() As String
        Get
            Return _monstername
        End Get
        Set(ByVal value As String)
            _monstername = value
        End Set
    End Property
    Private _bokemonSrc As String
    Public Property BokemonSrc() As String
        Get
            Return _bokemonSrc
        End Get
        Set(ByVal value As String)
            _bokemonSrc = value
        End Set
    End Property
    Private _bokemonInfo As String
    Public Property Bokemoninfo() As String
        Get
            Return _bokemonInfo
        End Get
        Set(ByVal value As String)
            _bokemonInfo = value
        End Set
    End Property
    Private _bokemonScore As Integer
    Public Property BokemonScore() As Integer
        Get
            Return _bokemonScore
        End Get
        Set(ByVal value As Integer)
            _bokemonScore = value
        End Set
    End Property
    Private _bokemonXP As String
    Public Property BokemonXP() As String
        Get
            Return _bokemonXP
        End Get
        Set(ByVal value As String)
            _bokemonXP = value
        End Set
    End Property

    Private _bokemonlevel As Integer
    Public Property BokemonLevel() As Integer
        Get
            Return _bokemonlevel
        End Get
        Set(ByVal value As Integer)
            _bokemonlevel = value
        End Set
    End Property
    Private _basepoint As Integer
    Public Property BasePoints() As Integer
        Get
            Return _basepoint
        End Get
        Set(ByVal value As Integer)
            _basepoint = value
        End Set
    End Property
    Private _bonus As Integer
    Public Property Bonus() As Integer
        Get
            Return _bonus
        End Get
        Set(ByVal value As Integer)
            _bonus = value
        End Set
    End Property
End Class
