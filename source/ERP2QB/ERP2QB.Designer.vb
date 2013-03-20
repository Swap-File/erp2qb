<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ERP2QB
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.transfer = New System.Windows.Forms.Button()
        Me.customerselect = New System.Windows.Forms.Button()
        Me.customercustomercontains = New System.Windows.Forms.TextBox()
        Me.Source = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.customersalesmancontains = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.customerdocumentcontains = New System.Windows.Forms.TextBox()
        Me.Customers = New System.Windows.Forms.Label()
        Me.Destination = New System.Windows.Forms.GroupBox()
        Me.customertobeprinted = New System.Windows.Forms.CheckBox()
        Me.customervalidate = New System.Windows.Forms.CheckBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.customerinvoiceselected = New System.Windows.Forms.Label()
        Me.customerelapsed = New System.Windows.Forms.Label()
        Me.customerStatus = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenERPServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenERPUsernameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenERPPasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenERPDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.vendortobeprinted = New System.Windows.Forms.CheckBox()
        Me.vendorvalidate = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.suppliersalesmancontains = New System.Windows.Forms.TextBox()
        Me.supplierselect = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.supplierdocumentcontains = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.suppliersuppliercontains = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.supplierinvoiceselected = New System.Windows.Forms.Label()
        Me.supplierelapsed = New System.Windows.Forms.Label()
        Me.supplierStatus = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.Source.SuspendLayout()
        Me.Destination.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'transfer
        '
        Me.transfer.Location = New System.Drawing.Point(104, 44)
        Me.transfer.Name = "transfer"
        Me.transfer.Size = New System.Drawing.Size(120, 34)
        Me.transfer.TabIndex = 4
        Me.transfer.Text = "Transfer"
        Me.transfer.UseVisualStyleBackColor = True
        '
        'customerselect
        '
        Me.customerselect.Location = New System.Drawing.Point(9, 176)
        Me.customerselect.Name = "customerselect"
        Me.customerselect.Size = New System.Drawing.Size(139, 34)
        Me.customerselect.TabIndex = 5
        Me.customerselect.Text = "Select Invoices"
        Me.customerselect.UseVisualStyleBackColor = True
        '
        'customercustomercontains
        '
        Me.customercustomercontains.Location = New System.Drawing.Point(9, 93)
        Me.customercustomercontains.Name = "customercustomercontains"
        Me.customercustomercontains.Size = New System.Drawing.Size(135, 20)
        Me.customercustomercontains.TabIndex = 10
        '
        'Source
        '
        Me.Source.Controls.Add(Me.Label2)
        Me.Source.Controls.Add(Me.customersalesmancontains)
        Me.Source.Controls.Add(Me.customerselect)
        Me.Source.Controls.Add(Me.Label1)
        Me.Source.Controls.Add(Me.customerdocumentcontains)
        Me.Source.Controls.Add(Me.Customers)
        Me.Source.Controls.Add(Me.customercustomercontains)
        Me.Source.Location = New System.Drawing.Point(6, 19)
        Me.Source.Name = "Source"
        Me.Source.Size = New System.Drawing.Size(185, 227)
        Me.Source.TabIndex = 12
        Me.Source.TabStop = False
        Me.Source.Text = "Source Filters (OpenERP)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Salesman Contains:"
        '
        'customersalesmancontains
        '
        Me.customersalesmancontains.Location = New System.Drawing.Point(9, 47)
        Me.customersalesmancontains.Name = "customersalesmancontains"
        Me.customersalesmancontains.Size = New System.Drawing.Size(135, 20)
        Me.customersalesmancontains.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 121)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Source Document Contains:"
        '
        'customerdocumentcontains
        '
        Me.customerdocumentcontains.Location = New System.Drawing.Point(9, 137)
        Me.customerdocumentcontains.Name = "customerdocumentcontains"
        Me.customerdocumentcontains.Size = New System.Drawing.Size(135, 20)
        Me.customerdocumentcontains.TabIndex = 12
        '
        'Customers
        '
        Me.Customers.AutoSize = True
        Me.Customers.Location = New System.Drawing.Point(6, 77)
        Me.Customers.Name = "Customers"
        Me.Customers.Size = New System.Drawing.Size(98, 13)
        Me.Customers.TabIndex = 11
        Me.Customers.Text = "Customer Contains:"
        '
        'Destination
        '
        Me.Destination.Controls.Add(Me.customertobeprinted)
        Me.Destination.Controls.Add(Me.customervalidate)
        Me.Destination.Location = New System.Drawing.Point(197, 19)
        Me.Destination.Name = "Destination"
        Me.Destination.Size = New System.Drawing.Size(185, 227)
        Me.Destination.TabIndex = 13
        Me.Destination.TabStop = False
        Me.Destination.Text = "Destination Options (Quickbooks)"
        '
        'customertobeprinted
        '
        Me.customertobeprinted.AutoSize = True
        Me.customertobeprinted.Checked = True
        Me.customertobeprinted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.customertobeprinted.Location = New System.Drawing.Point(6, 70)
        Me.customertobeprinted.Name = "customertobeprinted"
        Me.customertobeprinted.Size = New System.Drawing.Size(115, 17)
        Me.customertobeprinted.TabIndex = 15
        Me.customertobeprinted.Text = "to be Printed in QB"
        Me.customertobeprinted.UseVisualStyleBackColor = True
        '
        'customervalidate
        '
        Me.customervalidate.AutoSize = True
        Me.customervalidate.Checked = True
        Me.customervalidate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.customervalidate.Location = New System.Drawing.Point(6, 93)
        Me.customervalidate.Name = "customervalidate"
        Me.customervalidate.Size = New System.Drawing.Size(100, 17)
        Me.customervalidate.TabIndex = 14
        Me.customervalidate.Text = "Validate in ERP"
        Me.customervalidate.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(39, 109)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(284, 31)
        Me.ProgressBar1.TabIndex = 14
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.customerinvoiceselected)
        Me.GroupBox2.Controls.Add(Me.customerelapsed)
        Me.GroupBox2.Controls.Add(Me.customerStatus)
        Me.GroupBox2.Controls.Add(Me.transfer)
        Me.GroupBox2.Controls.Add(Me.ProgressBar1)
        Me.GroupBox2.Location = New System.Drawing.Point(22, 296)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(391, 170)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Progress"
        '
        'customerinvoiceselected
        '
        Me.customerinvoiceselected.AutoSize = True
        Me.customerinvoiceselected.Location = New System.Drawing.Point(73, 28)
        Me.customerinvoiceselected.Name = "customerinvoiceselected"
        Me.customerinvoiceselected.Size = New System.Drawing.Size(151, 13)
        Me.customerinvoiceselected.TabIndex = 17
        Me.customerinvoiceselected.Text = "Customer Invoices Selected: 0"
        '
        'customerelapsed
        '
        Me.customerelapsed.AutoSize = True
        Me.customerelapsed.Location = New System.Drawing.Point(36, 143)
        Me.customerelapsed.Name = "customerelapsed"
        Me.customerelapsed.Size = New System.Drawing.Size(94, 13)
        Me.customerelapsed.TabIndex = 16
        Me.customerelapsed.Text = "00:00:00.0000000"
        '
        'customerStatus
        '
        Me.customerStatus.AutoSize = True
        Me.customerStatus.Location = New System.Drawing.Point(36, 83)
        Me.customerStatus.Name = "customerStatus"
        Me.customerStatus.Size = New System.Drawing.Size(24, 13)
        Me.customerStatus.TabIndex = 15
        Me.customerStatus.Text = "Idle"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(831, 24)
        Me.MenuStrip1.TabIndex = 17
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenERPServerToolStripMenuItem, Me.OpenERPUsernameToolStripMenuItem, Me.OpenERPPasswordToolStripMenuItem, Me.OpenERPDatabaseToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'OpenERPServerToolStripMenuItem
        '
        Me.OpenERPServerToolStripMenuItem.Name = "OpenERPServerToolStripMenuItem"
        Me.OpenERPServerToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.OpenERPServerToolStripMenuItem.Text = "OpenERP Server"
        '
        'OpenERPUsernameToolStripMenuItem
        '
        Me.OpenERPUsernameToolStripMenuItem.Name = "OpenERPUsernameToolStripMenuItem"
        Me.OpenERPUsernameToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.OpenERPUsernameToolStripMenuItem.Text = "OpenERP Username"
        '
        'OpenERPPasswordToolStripMenuItem
        '
        Me.OpenERPPasswordToolStripMenuItem.Name = "OpenERPPasswordToolStripMenuItem"
        Me.OpenERPPasswordToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.OpenERPPasswordToolStripMenuItem.Text = "OpenERP Password"
        '
        'OpenERPDatabaseToolStripMenuItem
        '
        Me.OpenERPDatabaseToolStripMenuItem.Name = "OpenERPDatabaseToolStripMenuItem"
        Me.OpenERPDatabaseToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.OpenERPDatabaseToolStripMenuItem.Text = "OpenERP Database"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Destination)
        Me.GroupBox1.Controls.Add(Me.Source)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 37)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(391, 253)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Customer Invoices"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox5)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(419, 37)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(390, 253)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Supplier Invoices"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.vendortobeprinted)
        Me.GroupBox5.Controls.Add(Me.vendorvalidate)
        Me.GroupBox5.Location = New System.Drawing.Point(197, 20)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(185, 227)
        Me.GroupBox5.TabIndex = 16
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Destination Options (Quickbooks)"
        '
        'vendortobeprinted
        '
        Me.vendortobeprinted.AutoSize = True
        Me.vendortobeprinted.Checked = True
        Me.vendortobeprinted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.vendortobeprinted.Location = New System.Drawing.Point(6, 69)
        Me.vendortobeprinted.Name = "vendortobeprinted"
        Me.vendortobeprinted.Size = New System.Drawing.Size(115, 17)
        Me.vendortobeprinted.TabIndex = 15
        Me.vendortobeprinted.Text = "to be Printed in QB"
        Me.vendortobeprinted.UseVisualStyleBackColor = True
        '
        'vendorvalidate
        '
        Me.vendorvalidate.AutoSize = True
        Me.vendorvalidate.Checked = True
        Me.vendorvalidate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.vendorvalidate.Location = New System.Drawing.Point(6, 92)
        Me.vendorvalidate.Name = "vendorvalidate"
        Me.vendorvalidate.Size = New System.Drawing.Size(100, 17)
        Me.vendorvalidate.TabIndex = 14
        Me.vendorvalidate.Text = "Validate in ERP"
        Me.vendorvalidate.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.suppliersalesmancontains)
        Me.GroupBox4.Controls.Add(Me.supplierselect)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.supplierdocumentcontains)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.suppliersuppliercontains)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 20)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(185, 226)
        Me.GroupBox4.TabIndex = 16
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Source Filters (OpenERP)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Salesman Contains:"
        '
        'suppliersalesmancontains
        '
        Me.suppliersalesmancontains.Location = New System.Drawing.Point(9, 46)
        Me.suppliersalesmancontains.Name = "suppliersalesmancontains"
        Me.suppliersalesmancontains.Size = New System.Drawing.Size(135, 20)
        Me.suppliersalesmancontains.TabIndex = 14
        '
        'supplierselect
        '
        Me.supplierselect.Location = New System.Drawing.Point(9, 175)
        Me.supplierselect.Name = "supplierselect"
        Me.supplierselect.Size = New System.Drawing.Size(139, 34)
        Me.supplierselect.TabIndex = 5
        Me.supplierselect.Text = "Select Invoices"
        Me.supplierselect.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Source Document Contains:"
        '
        'supplierdocumentcontains
        '
        Me.supplierdocumentcontains.Location = New System.Drawing.Point(9, 136)
        Me.supplierdocumentcontains.Name = "supplierdocumentcontains"
        Me.supplierdocumentcontains.Size = New System.Drawing.Size(135, 20)
        Me.supplierdocumentcontains.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Supplier Contains:"
        '
        'suppliersuppliercontains
        '
        Me.suppliersuppliercontains.Location = New System.Drawing.Point(9, 92)
        Me.suppliersuppliercontains.Name = "suppliersuppliercontains"
        Me.suppliersuppliercontains.Size = New System.Drawing.Size(135, 20)
        Me.suppliersuppliercontains.TabIndex = 10
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.supplierinvoiceselected)
        Me.GroupBox6.Controls.Add(Me.supplierelapsed)
        Me.GroupBox6.Controls.Add(Me.supplierStatus)
        Me.GroupBox6.Controls.Add(Me.Button1)
        Me.GroupBox6.Controls.Add(Me.ProgressBar2)
        Me.GroupBox6.Location = New System.Drawing.Point(419, 296)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(391, 170)
        Me.GroupBox6.TabIndex = 19
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Progress"
        '
        'supplierinvoiceselected
        '
        Me.supplierinvoiceselected.AutoSize = True
        Me.supplierinvoiceselected.Location = New System.Drawing.Point(79, 28)
        Me.supplierinvoiceselected.Name = "supplierinvoiceselected"
        Me.supplierinvoiceselected.Size = New System.Drawing.Size(145, 13)
        Me.supplierinvoiceselected.TabIndex = 18
        Me.supplierinvoiceselected.Text = "Supplier Invoices Selected: 0"
        '
        'supplierelapsed
        '
        Me.supplierelapsed.AutoSize = True
        Me.supplierelapsed.Location = New System.Drawing.Point(36, 143)
        Me.supplierelapsed.Name = "supplierelapsed"
        Me.supplierelapsed.Size = New System.Drawing.Size(94, 13)
        Me.supplierelapsed.TabIndex = 16
        Me.supplierelapsed.Text = "00:00:00.0000000"
        '
        'supplierStatus
        '
        Me.supplierStatus.AutoSize = True
        Me.supplierStatus.Location = New System.Drawing.Point(36, 83)
        Me.supplierStatus.Name = "supplierStatus"
        Me.supplierStatus.Size = New System.Drawing.Size(24, 13)
        Me.supplierStatus.TabIndex = 15
        Me.supplierStatus.Text = "Idle"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(104, 44)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 34)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Transfer"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(39, 109)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(284, 31)
        Me.ProgressBar2.TabIndex = 14
        '
        'ERP2QB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 487)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "ERP2QB"
        Me.Text = "ERP2QB"
        Me.Source.ResumeLayout(False)
        Me.Source.PerformLayout()
        Me.Destination.ResumeLayout(False)
        Me.Destination.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents transfer As System.Windows.Forms.Button
    Friend WithEvents customerselect As System.Windows.Forms.Button
    Friend WithEvents customercustomercontains As System.Windows.Forms.TextBox
    Friend WithEvents Source As System.Windows.Forms.GroupBox
    Friend WithEvents Destination As System.Windows.Forms.GroupBox
    Friend WithEvents Customers As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents customerdocumentcontains As System.Windows.Forms.TextBox
    Friend WithEvents customertobeprinted As System.Windows.Forms.CheckBox
    Friend WithEvents customervalidate As System.Windows.Forms.CheckBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents customerStatus As System.Windows.Forms.Label
    Friend WithEvents customerelapsed As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenERPServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenERPUsernameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenERPPasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenERPDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents customersalesmancontains As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents customerinvoiceselected As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents vendortobeprinted As System.Windows.Forms.CheckBox
    Friend WithEvents vendorvalidate As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents suppliersalesmancontains As System.Windows.Forms.TextBox
    Friend WithEvents supplierselect As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents supplierdocumentcontains As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents suppliersuppliercontains As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents supplierinvoiceselected As System.Windows.Forms.Label
    Friend WithEvents supplierelapsed As System.Windows.Forms.Label
    Friend WithEvents supplierStatus As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar2 As System.Windows.Forms.ProgressBar


End Class
