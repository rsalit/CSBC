Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Web
Imports Encore.PayPal.Nvp

<Serializable()> _
Public Class ShoppingCartItem
    Private m_id As Integer
    Public ReadOnly Property ID() As Integer
        Get
            Return m_id
        End Get
    End Property

    Private m_sku As String
    Public ReadOnly Property SKU() As String
        Get
            Return m_sku
        End Get
    End Property

    Private m_name As String
    Public ReadOnly Property Name() As String
        Get
            Return m_name
        End Get
    End Property

    Private m_description As String
    Public Property Description() As String
        Get
            Return m_description
        End Get
        Set(ByVal value As String)
            m_description = value
        End Set
    End Property

    Private m_quantity As Integer
    Public Property Quantity() As Integer
        Get
            Return m_quantity
        End Get
        Set(ByVal value As Integer)
            m_quantity = value
        End Set
    End Property

    Private m_unitPrice As Decimal
    Public ReadOnly Property UnitPrice() As Decimal
        Get
            Return m_unitPrice
        End Get
    End Property

    Private m_salesTax As Decimal
    Public ReadOnly Property SalesTax() As Decimal
        Get
            Return m_salesTax
        End Get
    End Property

    Public Sub New(ByVal id As Integer, ByVal sku As String, ByVal name As String, ByVal description As String, ByVal unitPrice As Decimal, ByVal salesTax As Decimal)
        Me.m_id = id
        Me.m_sku = sku
        Me.m_name = name
        Me.m_description = description
        Me.m_quantity = 1
        Me.m_unitPrice = unitPrice
        Me.m_salesTax = salesTax
    End Sub
End Class

<Serializable()> _
Public Class ShoppingCart
    Private m_items As New Dictionary(Of Integer, ShoppingCartItem)()
    Public ReadOnly Property Items() As ICollection
        Get
            Return m_items.Values
        End Get
    End Property

    ''' <summary> 
    ''' Gets the sum total of the items' prices 
    ''' </summary> 
    Public ReadOnly Property ItemTotal() As Decimal
        Get
            Dim sum As Decimal = 0
            For Each item As ShoppingCartItem In m_items.Values
                sum += item.UnitPrice * item.Quantity
            Next
            Return sum
        End Get
    End Property

    ''' <summary> 
    ''' Gets the sum total of the items' sales tax 
    ''' </summary> 
    Public ReadOnly Property TaxTotal() As Decimal
        Get
            Dim sum As Decimal = 0
            For Each item As ShoppingCartItem In m_items.Values
                sum += item.SalesTax * item.Quantity
            Next
            Return sum
        End Get
    End Property

    ''' <summary> 
    ''' Gets the sum total of the items' prices plus taxes 
    ''' </summary> 
    Public ReadOnly Property GrandTotal() As Decimal
        Get
            Dim sum As Decimal = 0
            For Each item As ShoppingCartItem In m_items.Values
                sum += (item.UnitPrice + item.SalesTax) * item.Quantity
            Next
            Return sum
        End Get
    End Property

    ''' <summary> 
    ''' Adds a new item to the shopping cart 
    ''' </summary> 
    Public Sub InsertItem(ByVal id As Integer, ByVal sku As String, ByVal name As String, ByVal description As String, ByVal unitPrice As Decimal, ByVal salesTax As Decimal)
        If m_items.ContainsKey(id) Then
            m_items(id).Quantity += 1
        Else
            m_items.Add(id, New ShoppingCartItem(id, sku, name, description, unitPrice, salesTax))
        End If
    End Sub

    ''' <summary> 
    ''' Removes an item from the shopping cart 
    ''' </summary> 
    Public Sub DeleteItem(ByVal id As Integer)
        If m_items.ContainsKey(id) Then
            Dim item As ShoppingCartItem = m_items(id)
            item.Quantity -= 1
            If item.Quantity = 0 Then
                m_items.Remove(id)
            End If
        End If
    End Sub

    ''' <summary> 
    ''' Removes all items of a specified product from the shopping cart 
    ''' </summary> 
    Public Sub DeleteProduct(ByVal id As Integer)
        If m_items.ContainsKey(id) Then
            m_items.Remove(id)
        End If
    End Sub

    ''' <summary> 
    ''' Updates the quantity for an item 
    ''' </summary> 
    Public Sub UpdateItemQuantity(ByVal id As Integer, ByVal quantity As Integer)
        If m_items.ContainsKey(id) Then
            Dim item As ShoppingCartItem = m_items(id)
            item.Quantity = quantity
            If item.Quantity <= 0 Then
                m_items.Remove(id)
            End If
        End If
    End Sub

    ''' <summary> 
    ''' Clears the cart 
    ''' </summary> 
    Public Sub Clear()
        m_items.Clear()
    End Sub
End Class

''' <summary> 
''' Static properties and methods that apply to Profile.ShoppingCart 
''' </summary> 
<Serializable()> _
Public NotInheritable Class Cart
    Private Sub New()
    End Sub
    ''' <summary> 
    ''' Gets the collection of items in the current user's shopping cart 
    ''' </summary> 
    Public Shared ReadOnly Property Items() As ICollection
        Get
            Initialize()
            Return TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).Items
        End Get
    End Property

    ''' <summary> 
    ''' Gets the total price of all items plus tax in the current user's shopping cart 
    ''' </summary> 
    Public Shared ReadOnly Property GrandTotal() As Decimal
        Get
            Initialize()
            Return TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).GrandTotal
        End Get
    End Property

    ''' <summary> 
    ''' Gets the total price of all items in the current user's shopping cart 
    ''' </summary> 
    Public Shared ReadOnly Property ItemTotal() As Decimal
        Get
            Initialize()
            Return TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).ItemTotal
        End Get
    End Property

    ''' <summary> 
    ''' Gets the total price of all sales tax in the current user's shopping cart 
    ''' </summary> 
    Public Shared ReadOnly Property TaxTotal() As Decimal
        Get
            Initialize()
            Return TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).TaxTotal
        End Get
    End Property

    ''' <summary> 
    ''' Gets a list of the items in the current user's shopping cart. 
    ''' <para>The list can be added directly to the DoDirectPayment, DoReferenceTransaction,</para> 
    ''' and DoExpressCheckoutPayment objects in the Encore.PayPal.Nvp class library. 
    ''' </summary> 
    Public Shared ReadOnly Property LineItems() As List(Of NvpPayItem)
        Get
            Initialize()
            Dim itemList As New List(Of NvpPayItem)()
            For Each shoppingCartItem As ShoppingCartItem In TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).Items
                Dim item As New NvpPayItem()
                item.Number = shoppingCartItem.SKU
                item.Name = shoppingCartItem.Name
                item.Description = shoppingCartItem.Description
                item.Quantity = shoppingCartItem.Quantity.ToString()
                item.Amount = shoppingCartItem.UnitPrice.ToString("f")
                item.Tax = shoppingCartItem.SalesTax.ToString("f")
                itemList.Add(item)
            Next
            Return itemList
        End Get
    End Property

    ''' <summary> 
    ''' Guarantees that a cart exists in Session State. 
    ''' </summary> 
    Private Shared Sub Initialize()
        If HttpContext.Current.Session("Cart") Is Nothing Then
            HttpContext.Current.Session("Cart") = New ShoppingCart()
        End If
    End Sub

    ''' <summary> 
    ''' Clears all items from the current user's shopping cart 
    ''' </summary> 
    Public Shared Sub Clear()
        Initialize()
        TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).Clear()
    End Sub

    ''' <summary> 
    ''' Updates the quantity of the selected item in the current user's shopping cart 
    ''' </summary> 
    Public Shared Sub Update(ByVal id As Integer, ByVal quantity As Integer)
        Initialize()
        TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).UpdateItemQuantity(id, quantity)
    End Sub

    ''' <summary> 
    ''' Inserts an item into the current user's shopping cart 
    ''' </summary> 
    Public Shared Sub Insert(ByVal id As Integer, ByVal sku As String, ByVal name As String, ByVal description As String, ByVal unitPrice As Decimal, ByVal salesTax As Decimal)
        Initialize()
        TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).InsertItem(id, sku, name, description, unitPrice, salesTax)
    End Sub

    ''' <summary> 
    ''' Deletes an item from the current user's shopping cart 
    ''' </summary> 
    Public Shared Sub Delete(ByVal id As Integer)
        Initialize()
        TryCast(HttpContext.Current.Session("Cart"), ShoppingCart).DeleteItem(id)
    End Sub
End Class

''' <summary> 
''' A sample store inventory for purposes of testing the Encore.PayPal class library. 
''' </summary> 
<DataObject()> _
Public NotInheritable Class Store
    Private Sub New()
    End Sub
    Private Shared store As New ShoppingCart()

    Shared Sub New()
        store.InsertItem(1, "#A1", "ItemOne", "First Item Taxable", 19.95D, 1.6D)
        store.InsertItem(2, "#B2", "ItemTwo", "Second Item Taxable", 149.95D, 12D)
        store.InsertItem(3, "#C3", "ItemThree", "Third Item No Tax", 27.49D, 0D)
        store.InsertItem(4, "#D4", "ItemFour", "Fourth Item Taxable", 1D, 0.08D)
        store.InsertItem(5, "#E5", "ItemFive", "Fifth Item No Tax", 10D, 0D)
    End Sub

    <DataObjectMethodAttribute(DataObjectMethodType.[Select], True)> _
    Public Shared Function GetItems() As ICollection
        Return store.Items
    End Function
End Class