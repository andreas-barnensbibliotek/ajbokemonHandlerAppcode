Imports Microsoft.VisualBasic

Public Class bokemonInfo
    Public Sub New()
        _userid = 0
        _timetonext = 0
        _status = "Finns inget att visa!"
        _monId = 0
        _monvalue = 0
        _bokemonlist = New List(Of bokemonsterInfo)
    End Sub

    Private _userid As Integer
    Public Property Userid() As Integer
        Get
            Return _userid
        End Get
        Set(ByVal value As Integer)
            _userid = value
        End Set
    End Property

    Private _monId As Integer
    Public Property Monid() As Integer
        Get
            Return _monId
        End Get
        Set(ByVal value As Integer)
            _monId = value
        End Set
    End Property

    Private _timetonext As Integer
    Public Property TimeToNext() As Integer
        Get
            Return _timetonext
        End Get
        Set(ByVal value As Integer)
            _timetonext = value
        End Set
    End Property

    Private _bokemonlist As List(Of bokemonsterInfo)
    Public Property BokemonList() As List(Of bokemonsterInfo)
        Get
            Return _bokemonlist
        End Get
        Set(ByVal value As List(Of bokemonsterInfo))
            _bokemonlist = value
        End Set
    End Property

    Private _monvalue As Integer
    Public Property Monvalue() As Integer
        Get
            Return _monvalue
        End Get
        Set(ByVal value As Integer)
            _monvalue = value
        End Set
    End Property
    Private _status As String
    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property
End Class
