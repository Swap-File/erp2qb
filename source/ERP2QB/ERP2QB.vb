
Imports System
Imports System.Net
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports Interop.QBFC12
Imports CookComputing.XmlRpc
Imports Microsoft.Win32

Public Class ERP2QB

    Dim OpenERPserver As String
    Dim OpenERPPassword As String
    Dim OpenERPUsername As String
    Dim OpenERPDatabase As String


    Dim strFileName() As String '// String Array.
    'saves version
    Dim supportedVersion As String

    'the results from the search
    Dim CustomerResultsSearch As Integer()

    'the results from the search
    Dim SupplierResultsSearch As Integer()

    'the erp connection
    Dim erp As OpenErpObject

    Private Sub transfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles transfer.Click

        

        If (IsNothing(CustomerResultsSearch) = False) Then
            Dim start_time As DateTime
            start_time = Now
            customerStatus.Text = "Connecting..."

            'Open Connection to QB
            Dim responseMsgSet As IMsgSetResponse
            'Open Connection to quickbooks 
            Dim booSessionBegun As Boolean
            booSessionBegun = False
            'We want to know if we've begun a session so we can end it if an
            'error sends us to the exception handler
            booSessionBegun = False
            ' Create the session manager object using QBFC, and use this
            ' object to open a connection and begin a session with QuickBooks.
            Dim SessionManager As New QBSessionManager

            Try
                SessionManager.OpenConnection("", "ERP2QB")
                SessionManager.BeginSession("", ENOpenMode.omDontCare)
            Catch ex As Exception
                MsgBox("Error connecting to Quickbooks.  Make sure Quickbooks is open, and that you are an Quickbooks administrator.")
                Exit Sub
            End Try

            booSessionBegun = True
            Dim requestMsgSet As IMsgSetRequest
            For i = 0 To CustomerResultsSearch.Length - 1
                ProgressBar1.Value = ((i + 1) / (CustomerResultsSearch.Length)) * 100
                customerStatus.Text = (i + 1).ToString + " of " + (CustomerResultsSearch.Length).ToString

                customerelapsed.Text = Now.Subtract(start_time).Duration.ToString

                'get Invoice Data From OpenERP
                Dim ErpInvoice As [Object]()
                ErpInvoice = erp.read("account.invoice", {CustomerResultsSearch(i)}, {"origin", "comment", "date_due", "reference", "payment_term", "number", "journal_id", "currency_id", "address_invoice_id", "tax_line", "fiscal_position", "user_id", "partner_bank_id", "partner_id", "company_id", "amount_tax", "state", "type", "invoice_line", "account_id", "payment_ids", "reconciled", "residual", "date_invoice", "period_id", "amount_untaxed", "move_id", "amount_total", "name", "address_contact_id", "__last_update"})
                Dim ErpInvoiceXML As XmlRpcStruct = DirectCast(ErpInvoice(0), XmlRpcStruct)

                'get partner
                Dim ErpPartner As [Object]()
                ErpPartner = erp.read("res.partner", {ErpInvoiceXML("partner_id")(0)}, {"comment", "property_account_position", "property_stock_customer", "property_product_pricelist", "meeting_ids", "phonecall_ids", "user_id", "opt_out", "title", "company_id", "property_account_payable", "parent_id", "last_reconciliation_date", "debit", "property_stock_supplier", "ref", "website", "customer", "bank_ids", "section_id", "opportunity_ids", "supplier", "address", "date", "active", "emails", "lang", "credit_limit", "name", "property_product_pricelist_purchase", "property_account_receivable", "credit", "property_payment_term", "category_id", "__last_update"})
                Dim ErpPartnerXML As XmlRpcStruct = DirectCast(ErpPartner(0), XmlRpcStruct)

                'get partner address
                Dim ErpAddress As [Object]()
                ErpAddress = erp.read("res.partner.address", {ErpInvoiceXML("partner_id")(0)}, {"__last_update", "function", "city", "fax", "name", "zip", "title", "mobile", "street2", "country_id", "phone", "street", "state_id", "type", "email"})
                Dim ErpAddressXML As XmlRpcStruct = DirectCast(ErpAddress(0), XmlRpcStruct)


                'start new QBFC new request to find the customer name
                requestMsgSet = qbfcrequestMsgSet(SessionManager)
                Dim custSearch As ICustomerQuery = requestMsgSet.AppendCustomerQueryRq

                'Lookup full customer name by erp title first , then ref, or erp name last.
                If ErpPartnerXML("title").ToString <> "False" Then
                    custSearch.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains)
                    custSearch.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.Name.SetValue(ErpPartnerXML("title")(1))
                ElseIf ErpPartnerXML("ref").ToString <> "False" Then
                    custSearch.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains)
                    custSearch.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.Name.SetValue(ErpPartnerXML("ref"))
                Else
                    custSearch.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains)
                    custSearch.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.Name.SetValue(ErpPartnerXML("name"))
                End If
                responseMsgSet = SessionManager.DoRequests(requestMsgSet)
                Dim customerfound As String

                If (responseMsgSet.ResponseList.Count = 1) Then
                    'we have one response for our single add request 
                    Dim rs As IResponse = responseMsgSet.ResponseList.GetAt(0)

                    'retrieve some CustomerRet values 
                    'Add and Mod Rq return a single Ret object in rs.Detail 
                    'Query return a RetList object in rs.Detail  

                    'check the status code of the response, 0=ok, >0 is warning 
                    If (rs.StatusCode >= 0) Then
                        'the request-specific response is in the details, make sure we have some 
                        If (Not rs.Detail Is Nothing) Then
                            'make sure the response is the type we're expecting 
                            Dim responseType As ENResponseType
                            responseType = CType(rs.Type.GetValue(), ENResponseType)
                            If (responseType = ENResponseType.rtCustomerQueryRs) Then
                                'upcast to more specific type here, this is safe because we checked with response.Type check above 
                                Dim CustomerRetList As ICustomerRetList
                                CustomerRetList = CType(rs.Detail, ICustomerRetList)
                                If (CustomerRetList Is Nothing) Then
                                    Exit Sub
                                End If

                                Dim customerRet As ICustomerRet = CustomerRetList.GetAt(0)
                                customerfound = customerRet.FullName.GetValue()
                            End If
                        End If
                    End If
                End If

                'done getting customer name



                'new request to add the invoice
                requestMsgSet = qbfcrequestMsgSet(SessionManager)


                ' Add the request to the message set request object
                Dim invoiceAdd As IInvoiceAdd
                invoiceAdd = requestMsgSet.AppendInvoiceAddRq
                invoiceAdd.CustomerRef.FullName.SetValue(customerfound)

                ' parse SO
                Dim SOnumber As String
                SOnumber = ErpInvoiceXML("origin").TrimEnd("]", " ")
                SOnumber = SOnumber.Remove(0, SOnumber.IndexOf("SO"))
                'invoiceAdd.RefNumber.SetValue(SOnumber)
                'leave out so that quickbooks automatically assigns its next internal number

                'parse PO
                Dim POnumber As String
                POnumber = ErpInvoiceXML("name")
                If POnumber.IndexOf(" :") > 0 Then
                    POnumber = POnumber.Remove(POnumber.IndexOf(" :"))
                End If
                invoiceAdd.PONumber.SetValue(POnumber)


                invoiceAdd.Memo.SetValue(ErpInvoiceXML("origin"))
                If ErpInvoiceXML("partner_id").ToString <> "False" Then
                    invoiceAdd.ShipAddress.Addr1.SetValue(ErpInvoiceXML("partner_id")(1))
                End If
                If ErpAddressXML("street").ToString <> "False" Then
                    invoiceAdd.ShipAddress.Addr2.SetValue(ErpAddressXML("street"))
                End If
                If ErpAddressXML("city").ToString <> "False" Then
                    invoiceAdd.ShipAddress.City.SetValue(ErpAddressXML("city"))
                End If
                If ErpAddressXML("state_id").ToString <> "False" Then
                    invoiceAdd.ShipAddress.State.SetValue(ErpAddressXML("state_id")(1))
                End If
                If ErpAddressXML("zip").ToString <> "False" Then
                    invoiceAdd.ShipAddress.PostalCode.SetValue(ErpAddressXML("zip"))
                End If
                If customertobeprinted.Checked = True Then
                    invoiceAdd.IsToBePrinted.SetValue(True)
                End If

                invoiceAdd.TxnDate.SetValue(System.DateTime.Now)

                'get Invoice Lines From OpenERP 
                Dim ErpInvoiceLines As [Object]()
                ErpInvoiceLines = erp.read("account.invoice.line", ErpInvoiceXML("invoice_line"), {"__last_update", "uos_id", "account_id", "price_unit", "price_subtotal", "discount", "product_id", "quantity", "name"})

                'add lines to qbfc invoice 
                Dim invoiceLineAdd As IInvoiceLineAdd

                For j = 0 To ErpInvoiceLines.Length - 1
                    Dim ErpInvoiceLinesXML As XmlRpcStruct = DirectCast(ErpInvoiceLines(j), XmlRpcStruct)

                    'get Product Info From OpenERP 
                    Dim ErpProduct As [Object]()
                    ErpProduct = erp.read("product.product", {ErpInvoiceLinesXML("product_id")(0)}, {"warranty", "property_stock_procurement", "supply_method", "uos_id", "list_price", "weight", "ean13", "incoming_qty", "standard_price", "price_extra", "mes_type", "uom_id", "orderpoint_ids", "description_purchase", "default_code", "property_account_income", "qty_available", "variants", "uos_coeff", "virtual_available", "sale_ok", "purchase_ok", "product_manager", "track_outgoing", "company_id", "active", "state", "loc_rack", "uom_po_id", "type", "property_stock_account_input", "description", "valuation", "track_incoming", "property_stock_production", "supplier_taxes_id", "volume", "outgoing_qty", "description_sale", "procure_method", "property_stock_inventory", "cost_method", "loc_row", "name", "weight_net", "packaging", "sale_delay", "loc_case", "property_stock_account_output", "property_account_expense", "categ_id", "track_production", "product_image", "taxes_id", "produce_delay", "seller_ids", "hr_expense_ok", "price_margin", "__last_update"})
                    Dim ErpProductXML As XmlRpcStruct = DirectCast(ErpProduct(0), XmlRpcStruct)

                    'Add Invoice Lines to Quickbooks Buffer
                    ' Create the first line item for the invoice
                    invoiceLineAdd = invoiceAdd.ORInvoiceLineAddList.Append.InvoiceLineAdd

                    ' Set the values for the invoice line

                    If ErpProductXML("default_code").ToString <> "False" Then
                        invoiceLineAdd.ItemRef.FullName.SetValue(ErpProductXML("default_code"))
                    End If

                    If ErpInvoiceLinesXML("quantity").ToString <> "False" Then
                        invoiceLineAdd.Quantity.SetValue(ErpInvoiceLinesXML("quantity"))
                    End If

                    'first line only of description

                    If ErpProductXML("description").ToString <> "False" Then
                        Dim description As String

                        description = ErpProductXML("description")

                        If description.Contains(vbLf) Then
                            description = description.Substring(0, description.IndexOf(vbLf))
                        End If

                        If description.Length > 140 Then
                            description = description.Substring(0, 140)
                        End If


                        invoiceLineAdd.Desc.SetValue(description)
                    End If

                    If ErpInvoiceLinesXML("price_unit").ToString <> "False" Then
                        invoiceLineAdd.ORRatePriceLevel.Rate.SetValue(ErpInvoiceLinesXML("price_unit"))
                    End If
                Next j


                'Send Data To Quickbooks

                ' Perform the request and obtain a response from QuickBooks
                responseMsgSet = SessionManager.DoRequests(requestMsgSet)

                ' Uncomment the following to see the request and response XML for debugging
                'MsgBox(requestMsgSet.ToXMLString, vbOKOnly, "RequestXML")
                'MsgBox(responseMsgSet.ToXMLString, vbOKOnly, "ResponseXML")

                ' Interpret the response
                Dim response As IResponse

                ' The response list contains only one response,
                ' which corresponds to our single request

                response = responseMsgSet.ResponseList.GetAt(0)
                Dim msg As String

                msg = ErpInvoiceXML("origin") + vbCrLf + vbCrLf + "Status: Code = " & CStr(response.StatusCode) & _
                        vbCrLf + vbCrLf + "Message = " & response.StatusMessage & _
                        vbCrLf + vbCrLf + "Severity = " & response.StatusSeverity & vbCrLf

                Dim invoiceRet As IInvoiceRet
                invoiceRet = response.Detail

                If (invoiceRet Is Nothing) Then

                    customerelapsed.Text = Now.Subtract(start_time).Duration.ToString

                    customerStatus.Text = customerStatus.Text + " - ERROR"
                    MsgBox(msg)
                    SessionManager.EndSession()
                    booSessionBegun = False
                    SessionManager.CloseConnection()

                    Exit Sub

                End If

                'validate 
                If customervalidate.Checked = True Then
                    erp.exec_workflow("account.invoice", "invoice_open", CustomerResultsSearch(i))
                End If

            Next (i)
            ' Close the session and connection with QuickBooks.
            SessionManager.EndSession()
            booSessionBegun = False
            SessionManager.CloseConnection()
            customerStatus.Text = "Done"
            'update status
            customerinvoiceselected.Text = "Customer Invoices Transfered: " + (CustomerResultsSearch.Length).ToString
            'blank selection
            CustomerResultsSearch = {}
            customerStatus.Text = "Done"
            customerelapsed.Text = Now.Subtract(start_time).Duration.ToString
        End If








    End Sub


    Private Sub customerselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles customerselect.Click

        'filters and gets ID numbers of Invoices to later transfer

        erp = New OpenErpObject(OpenERPserver, OpenERPDatabase, OpenERPUsername, OpenERPPassword)

        Dim lstFilters As New ArrayList()
        lstFilters.Add({"type", "=", "out_invoice"})
        lstFilters.Add({"state", "=", "draft"})

        If customercustomercontains.Text <> "" Then
            lstFilters.Add({"partner_id", "ilike", customercustomercontains.Text})
        End If

        If customerdocumentcontains.Text <> "" Then
            lstFilters.Add({"origin", "ilike", customerdocumentcontains.Text})
        End If

        If customersalesmancontains.Text <> "" Then
            lstFilters.Add({"user_id", "ilike", customersalesmancontains.Text})
        End If

        Try
            CustomerResultsSearch = erp.search("account.invoice", lstFilters.ToArray())
        Catch ex As Exception
            MsgBox("Error connecting to OpenERP.  Check username, password, databasename, and servername.")
            Exit Sub
        End Try

        'MessageBox.Show("Found records: " & resSearch.Length)
        customerinvoiceselected.Text = "Customer Invoices Selected: " & CustomerResultsSearch.Length.ToString

        ProgressBar1.Value = 0
        customerelapsed.Text = "00:00:00.0000000"
    End Sub

    Function QBFCLatestVersion(ByVal SessionManager As QBSessionManager) As String
        Dim strXMLVersions() As String
        'Should be able to use this, but there appears to be a bug that may cause 2.0 to be returned
        'when it should not.
        'strXMLVersions = SessionManager.QBXMLVersionsForSession

        Dim msgset As IMsgSetRequest
        'Use oldest version to ensure that we work with any QuickBooks (US)
        msgset = SessionManager.CreateMsgSetRequest("US", 1, 0)
        msgset.AppendHostQueryRq()
        Dim QueryResponse As IMsgSetResponse
        QueryResponse = SessionManager.DoRequests(msgset)
        Dim response As IResponse

        ' The response list contains only one response,
        ' which corresponds to our single HostQuery request
        response = QueryResponse.ResponseList.GetAt(0)
        Dim HostResponse As IHostRet
        HostResponse = response.Detail
        Dim supportedVersions As IBSTRList
        supportedVersions = HostResponse.SupportedQBXMLVersionList

        Dim i As Long
        Dim vers As Double
        Dim LastVers As Double
        LastVers = 0
        For i = 0 To supportedVersions.Count - 1
            vers = Val(supportedVersions.GetAt(i))
            If (vers > LastVers) Then
                LastVers = vers
                QBFCLatestVersion = supportedVersions.GetAt(i)
            End If
        Next i
    End Function

    Private Function qbfcrequestMsgSet(ByVal SessionManager As QBSessionManager) As IMsgSetRequest
        ' Create the message set request object

        If supportedVersion = "" Then
            supportedVersion = QBFCLatestVersion(SessionManager)
        End If

        Dim addr4supported As Boolean
        addr4supported = False
        Dim requestMsgSet As IMsgSetRequest
        If (supportedVersion >= "12.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 12, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "11.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 11, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "10.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 10, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "9.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 9, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "8.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 8, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "7.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 7, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "6.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 6, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "5.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 5, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "4.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 4, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "3.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 3, 0)
            addr4supported = True
        ElseIf (supportedVersion >= "2.0") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 2, 0)
            addr4supported = True
        ElseIf (supportedVersion = "1.1") Then
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 1, 1)
        Else
            MsgBox("You are apparently running QuickBooks 2002 Release 1, we strongly recommend that you use QuickBooks' online update feature to obtain the latest fixes and enhancements", vbExclamation)
            requestMsgSet = SessionManager.CreateMsgSetRequest("US", 1, 0)
        End If
        ' Initialize the message set request's attributes
        requestMsgSet.Attributes.OnError = ENRqOnError.roeStop
        Return requestMsgSet
    End Function

    Private Sub OpenERPServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenERPServerToolStripMenuItem.Click
        Dim NewOpenERPserver As String
        NewOpenERPserver = InputBox("Example: 'http://openerp:8069'", "OpenERP Server", OpenERPserver)
        If NewOpenERPserver.Length <> 0 Then
            Dim ERPConnect As RegistryKey
            ERPConnect = Registry.CurrentUser.CreateSubKey("ERP2QB")
            ERPConnect.SetValue("Server", NewOpenERPserver)
            ERPConnect.Close()
            OpenERPserver = NewOpenERPserver
            Me.Text = "ERP2QB - " + OpenERPserver
        End If
    End Sub

    Private Sub OpenERPUsernameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenERPUsernameToolStripMenuItem.Click
        Dim NewOpenERPUsername As String
        NewOpenERPUsername = InputBox("Example: 'admin'", "OpenERP Username", OpenERPUsername)
        If NewOpenERPUsername.Length <> 0 Then
            Dim ERPConnect As RegistryKey
            ERPConnect = Registry.CurrentUser.CreateSubKey("ERP2QB")
            ERPConnect.SetValue("Username", NewOpenERPUsername)
            ERPConnect.Close()
            OpenERPUsername = NewOpenERPUsername
        End If
    End Sub

    Private Sub OpenERPPasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenERPPasswordToolStripMenuItem.Click
        Dim NewOpenERPPassword As String
        NewOpenERPPassword = InputBox("Example 'admin'", "OpenERP Password", OpenERPPassword)
        If NewOpenERPPassword.Length <> 0 Then
            Dim ERPConnect As RegistryKey
            ERPConnect = Registry.CurrentUser.CreateSubKey("ERP2QB")
            ERPConnect.SetValue("Password", NewOpenERPPassword)
            ERPConnect.Close()
            OpenERPPassword = NewOpenERPPassword
        End If
    End Sub

    Private Sub ERP2QB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ERPConnect As RegistryKey
        If Registry.CurrentUser.OpenSubKey("ERP2QB") Is Nothing Then
            ' Create a subkey named Test9999 under HKEY_CURRENT_USER.
            ERPConnect = Registry.CurrentUser.CreateSubKey("ERP2QB")

            ' Create data for the TestSettings subkey.
            ERPConnect.SetValue("Server", "http://openerp:8069")
            ERPConnect.SetValue("Username", "admin")
            ERPConnect.SetValue("Password", "admin")
            ERPConnect.SetValue("Database", "database")
            ERPConnect.Close()
        End If


        ERPConnect = Registry.CurrentUser.OpenSubKey("ERP2QB")
        OpenERPserver = ERPConnect.GetValue("Server").ToString()
        OpenERPUsername = ERPConnect.GetValue("Username").ToString()
        OpenERPPassword = ERPConnect.GetValue("Password").ToString()
        OpenERPDatabase = ERPConnect.GetValue("Database").ToString()
        ERPConnect.Close()
        Me.Text = "ERP2QB - " + OpenERPserver

    End Sub

    Private Sub OpenERPDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenERPDatabaseToolStripMenuItem.Click
        Dim NewOpenERPdatabase As String
        NewOpenERPdatabase = InputBox("Example: 'admin'", "OpenERP Database", OpenERPDatabase)
        If NewOpenERPdatabase.Length <> 0 Then
            Dim ERPConnect As RegistryKey
            ERPConnect = Registry.CurrentUser.CreateSubKey("ERP2QB")
            ERPConnect.SetValue("Database", NewOpenERPdatabase)
            ERPConnect.Close()
            OpenERPDatabase = NewOpenERPdatabase
        End If
    End Sub




    Private Sub supplierselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles supplierselect.Click

        'filters and gets ID numbers of Invoices to later transfer

        erp = New OpenErpObject(OpenERPserver, OpenERPDatabase, OpenERPUsername, OpenERPPassword)

        Dim lstFilters As New ArrayList()
        lstFilters.Add({"type", "=", "in_invoice"})
        lstFilters.Add({"state", "=", "draft"})

        If suppliersuppliercontains.Text <> "" Then
            lstFilters.Add({"partner_id", "ilike", suppliersuppliercontains.Text})
        End If

        If supplierdocumentcontains.Text <> "" Then
            lstFilters.Add({"origin", "ilike", supplierdocumentcontains.Text})
        End If

        If suppliersalesmancontains.Text <> "" Then
            lstFilters.Add({"user_id", "ilike", suppliersalesmancontains.Text})
        End If

        Try
            SupplierResultsSearch = erp.search("account.invoice", lstFilters.ToArray())
        Catch ex As Exception
            MsgBox("Error connecting to OpenERP.  Check username, password, databasename, and servername.")
            Exit Sub
        End Try

        'MessageBox.Show("Found records: " & resSearch.Length)
        supplierinvoiceselected.Text = "Supplier Invoices Selected: " & SupplierResultsSearch.Length.ToString

        ProgressBar2.Value = 0
        supplierelapsed.Text = "00:00:00.0000000"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        If (IsNothing(SupplierResultsSearch) = False) Then
            Dim start_time As DateTime
            start_time = Now
            supplierStatus.Text = "Connecting..."
            'Open Connection to QB
            Dim responseMsgSet As IMsgSetResponse
            'Open Connection to quickbooks 
            Dim booSessionBegun As Boolean
            booSessionBegun = False
            'We want to know if we've begun a session so we can end it if an
            'error sends us to the exception handler
            booSessionBegun = False
            ' Create the session manager object using QBFC, and use this
            ' object to open a connection and begin a session with QuickBooks.
            Dim SessionManager As New QBSessionManager

            Try
                SessionManager.OpenConnection("", "ERP2QB")
                SessionManager.BeginSession("", ENOpenMode.omDontCare)
            Catch ex As Exception
                MsgBox("Error connecting to Quickbooks.  Make sure Quickbooks is open, and that you are an Quickbooks administrator.")
                Exit Sub
            End Try

            booSessionBegun = True
            Dim requestMsgSet As IMsgSetRequest
            For i = 0 To SupplierResultsSearch.Length - 1

                ProgressBar2.Value = ((i + 1) / (SupplierResultsSearch.Length)) * 100
                supplierStatus.Text = (i + 1).ToString + " of " + (SupplierResultsSearch.Length).ToString

                supplierelapsed.Text = Now.Subtract(start_time).Duration.ToString

                'get Invoice Data From OpenERP
                Dim ErpInvoice As [Object]()
                ErpInvoice = erp.read("account.invoice", {SupplierResultsSearch(i)}, {"origin", "comment", "date_due", "reference", "payment_term", "number", "journal_id", "currency_id", "address_invoice_id", "tax_line", "fiscal_position", "user_id", "partner_bank_id", "partner_id", "company_id", "amount_tax", "state", "type", "invoice_line", "account_id", "payment_ids", "reconciled", "residual", "date_invoice", "period_id", "amount_untaxed", "move_id", "amount_total", "name", "address_contact_id", "__last_update"})
                Dim ErpInvoiceXML As XmlRpcStruct = DirectCast(ErpInvoice(0), XmlRpcStruct)

                'get partner
                Dim ErpPartner As [Object]()
                ErpPartner = erp.read("res.partner", {ErpInvoiceXML("partner_id")(0)}, {"comment", "property_account_position", "property_stock_customer", "property_product_pricelist", "meeting_ids", "phonecall_ids", "user_id", "opt_out", "title", "company_id", "property_account_payable", "parent_id", "last_reconciliation_date", "debit", "property_stock_supplier", "ref", "website", "customer", "bank_ids", "section_id", "opportunity_ids", "supplier", "address", "date", "active", "emails", "lang", "credit_limit", "name", "property_product_pricelist_purchase", "property_account_receivable", "credit", "property_payment_term", "category_id", "__last_update"})
                Dim ErpPartnerXML As XmlRpcStruct = DirectCast(ErpPartner(0), XmlRpcStruct)

                'get partner address
                'Dim ErpAddress As [Object]()
                'ErpAddress = erp.read("res.partner.address", {ErpInvoiceXML("partner_id")(0)}, {"__last_update", "function", "city", "fax", "name", "zip", "title", "mobile", "street2", "country_id", "phone", "street", "state_id", "type", "email"})
                'Dim ErpAddressXML As XmlRpcStruct = DirectCast(ErpAddress(0), XmlRpcStruct)


                'start new QBFC new request to find the customer name
                requestMsgSet = qbfcrequestMsgSet(SessionManager)
                Dim vendSearch As IVendorQuery = requestMsgSet.AppendVendorQueryRq

                'Lookup full customer name by erp title first , then ref, or erp name last.
                If ErpPartnerXML("title").ToString <> "False" Then
                    vendSearch.ORVendorListQuery.VendorListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains)
                    vendSearch.ORVendorListQuery.VendorListFilter.ORNameFilter.NameFilter.Name.SetValue(ErpPartnerXML("title")(1))
                ElseIf ErpPartnerXML("ref").ToString <> "False" Then
                    vendSearch.ORVendorListQuery.VendorListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains)
                    vendSearch.ORVendorListQuery.VendorListFilter.ORNameFilter.NameFilter.Name.SetValue(ErpPartnerXML("ref"))
                Else
                    vendSearch.ORVendorListQuery.VendorListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains)
                    vendSearch.ORVendorListQuery.VendorListFilter.ORNameFilter.NameFilter.Name.SetValue(ErpPartnerXML("name"))
                End If
                responseMsgSet = SessionManager.DoRequests(requestMsgSet)
                Dim vendorfound As String

                If (responseMsgSet.ResponseList.Count = 1) Then
                    'we have one response for our single add request 
                    Dim rs As IResponse = responseMsgSet.ResponseList.GetAt(0)

                    'retrieve some CustomerRet values 
                    'Add and Mod Rq return a single Ret object in rs.Detail 
                    'Query return a RetList object in rs.Detail  

                    'check the status code of the response, 0=ok, >0 is warning 
                    If (rs.StatusCode >= 0) Then
                        'the request-specific response is in the details, make sure we have some 
                        If (Not rs.Detail Is Nothing) Then
                            'make sure the response is the type we're expecting 
                            Dim responseType As ENResponseType
                            responseType = CType(rs.Type.GetValue(), ENResponseType)
                            If (responseType = ENResponseType.rtVendorQueryRs) Then
                                'upcast to more specific type here, this is safe because we checked with response.Type check above 
                                Dim VendorRetList As IVendorRetList
                                VendorRetList = CType(rs.Detail, IVendorRetList)
                                If (VendorRetList Is Nothing) Then
                                    Exit Sub
                                End If

                                Dim vendorRet As IVendorRet = VendorRetList.GetAt(0)
                                vendorfound = vendorRet.Name.GetValue()
                            End If
                        End If
                    End If
                End If

                'done getting customer name


                'new request to add the invoice
                requestMsgSet = qbfcrequestMsgSet(SessionManager)

                ' Add the request to the message set request object
                Dim PurchaseOrderAdd As IPurchaseOrderAdd
                PurchaseOrderAdd = requestMsgSet.AppendPurchaseOrderAddRq
                PurchaseOrderAdd.VendorRef.FullName.SetValue(vendorfound)

                'origin
                PurchaseOrderAdd.Memo.SetValue(ErpInvoiceXML("origin"))
                'salesman name
                PurchaseOrderAdd.Other1.SetValue(ErpInvoiceXML("user_id")(1))

                'date placed
                PurchaseOrderAdd.TxnDate.SetValue(DateTime.Parse(ErpInvoiceXML("date_invoice")))

                'date due
                If ErpInvoiceXML("date_due").ToString <> "False" Then
                    PurchaseOrderAdd.DueDate.SetValue(DateTime.Parse(ErpInvoiceXML("date_due")))
                Else
                    PurchaseOrderAdd.DueDate.SetValue(DateAdd("d", 30, DateTime.Parse(ErpInvoiceXML("date_invoice"))))
                End If

                'printing
                If vendortobeprinted.Checked = True Then
                    PurchaseOrderAdd.IsToBePrinted.SetValue(True)
                End If

                'get Invoice Lines From OpenERP 
                Dim ErpInvoiceLines As [Object]()
                ErpInvoiceLines = erp.read("account.invoice.line", ErpInvoiceXML("invoice_line"), {"__last_update", "uos_id", "account_id", "price_unit", "price_subtotal", "discount", "product_id", "quantity", "name", "note"})

                'add lines to qbfc invoice 
                Dim purchaseOrderLineAdd As IPurchaseOrderLineAdd

                For j = 0 To ErpInvoiceLines.Length - 1
                    Dim ErpInvoiceLinesXML As XmlRpcStruct = DirectCast(ErpInvoiceLines(j), XmlRpcStruct)

                    'get Product Info From OpenERP 
                    Dim ErpProduct As [Object]()
                    ErpProduct = erp.read("product.product", {ErpInvoiceLinesXML("product_id")(0)}, {"warranty", "property_stock_procurement", "supply_method", "uos_id", "list_price", "weight", "ean13", "incoming_qty", "standard_price", "price_extra", "mes_type", "uom_id", "orderpoint_ids", "description_purchase", "default_code", "property_account_income", "qty_available", "variants", "uos_coeff", "virtual_available", "sale_ok", "purchase_ok", "product_manager", "track_outgoing", "company_id", "active", "state", "loc_rack", "uom_po_id", "type", "property_stock_account_input", "description", "valuation", "track_incoming", "property_stock_production", "supplier_taxes_id", "volume", "outgoing_qty", "description_sale", "procure_method", "property_stock_inventory", "cost_method", "loc_row", "name", "weight_net", "packaging", "sale_delay", "loc_case", "property_stock_account_output", "property_account_expense", "categ_id", "track_production", "product_image", "taxes_id", "produce_delay", "seller_ids", "hr_expense_ok", "price_margin", "__last_update"})
                    Dim ErpProductXML As XmlRpcStruct = DirectCast(ErpProduct(0), XmlRpcStruct)

                    'Add Invoice Lines to Quickbooks Buffer
                    ' Create the first line item for the invoice
                    purchaseOrderLineAdd = PurchaseOrderAdd.ORPurchaseOrderLineAddList.Append.PurchaseOrderLineAdd

                    Dim category() As String
                    category = Split(ErpProductXML("categ_id")(1), "/")

                    ' Set the values for the invoice line

                    If ErpProductXML("default_code").ToString <> "False" Then
                        purchaseOrderLineAdd.ItemRef.FullName.SetValue(RTrim(LTrim(category(category.Length - 1))))
                    End If

                    If ErpInvoiceLinesXML("quantity").ToString <> "False" Then
                        purchaseOrderLineAdd.Quantity.SetValue(ErpInvoiceLinesXML("quantity").ToString)
                    End If

                    'first line only of description


                    Dim description As String

                    description = ErpProductXML("default_code") + " " + ErpInvoiceLinesXML("note")

                    If description.Contains(vbLf) Then
                        description = description.Substring(0, description.IndexOf(vbLf))
                    End If

                    If description.Length > 140 Then
                        description = description.Substring(0, 140)
                    End If

                    purchaseOrderLineAdd.Desc.SetValue(description)

                    If ErpInvoiceLinesXML("price_unit").ToString <> "False" Then
                        purchaseOrderLineAdd.Rate.SetValue(ErpInvoiceLinesXML("price_unit"))
                    End If
                Next j


                'Send Data To Quickbooks

                ' Perform the request and obtain a response from QuickBooks
                responseMsgSet = SessionManager.DoRequests(requestMsgSet)

                ' Uncomment the following to see the request and response XML for debugging
                'MsgBox(requestMsgSet.ToXMLString, vbOKOnly, "RequestXML")
                'MsgBox(responseMsgSet.ToXMLString, vbOKOnly, "ResponseXML")

                ' Interpret the response
                Dim response As IResponse

                ' The response list contains only one response,
                ' which corresponds to our single request

                response = responseMsgSet.ResponseList.GetAt(0)
                Dim msg As String

                msg = ErpInvoiceXML("origin") + vbCrLf + vbCrLf + "Status: Code = " & CStr(response.StatusCode) & _
                        vbCrLf + vbCrLf + "Message = " & response.StatusMessage & _
                        vbCrLf + vbCrLf + "Severity = " & response.StatusSeverity & vbCrLf

                Dim invoiceRet As IPurchaseOrderRet
                invoiceRet = response.Detail

                If (invoiceRet Is Nothing) Then

                    supplierelapsed.Text = Now.Subtract(start_time).Duration.ToString

                    supplierStatus.Text = supplierStatus.Text + " - ERROR"
                    MsgBox(msg)
                    SessionManager.EndSession()
                    booSessionBegun = False
                    SessionManager.CloseConnection()

                    Exit Sub

                End If

                'validate 
                If vendorvalidate.Checked = True Then
                    erp.exec_workflow("account.invoice", "invoice_open", SupplierResultsSearch(i))
                End If

            Next (i)
            ' Close the session and connection with QuickBooks.
            SessionManager.EndSession()
            booSessionBegun = False
            SessionManager.CloseConnection()
            'update status
            supplierinvoiceselected.Text = "Supplier Invoices Transfered: " + (SupplierResultsSearch.Length).ToString
            'blank selection
            SupplierResultsSearch = {}
            supplierStatus.Text = "Done"
            supplierelapsed.Text = Now.Subtract(start_time).Duration.ToString
        End If

    End Sub


End Class


