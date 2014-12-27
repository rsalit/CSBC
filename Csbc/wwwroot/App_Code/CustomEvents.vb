Imports System 
Imports System.Web 
Imports System.Web.Management 
Imports System.Web.UI 

Public Enum Activity 
    None 
    [New] 
    Edit 
    Delete 
    Insert 
    Update 
    Cancel 
    Open 
    Close 
    [Select] 
    Success 
    Failure 
    Refresh 
End Enum 

Public Class ActionEventArgs 
    Inherits EventArgs 
    Public Sub New(ByVal action As Activity) 
        Me.m_action = action 
    End Sub 
    
    Public Sub New() 
        Me.m_action = Activity.None 
    End Sub 
    
    Private m_action As Activity 
    Public ReadOnly Property Action() As Activity 
        Get 
            Return m_action 
        End Get 
    End Property 
End Class 

Public Delegate Sub ActionEventHandler(ByVal sender As Object, ByVal e As ActionEventArgs) 