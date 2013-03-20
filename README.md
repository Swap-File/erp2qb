erp2qb
======
erp2qb takes Customer Invoices from OpenERP and import them into QuickBooks as invoices. 

It also has preliminary support for importing Supplier Invoices from OpenERP into QuickBooks as Purchase Orders.

Using erp2qb
======

1: First, go into Customer Invoices in OpenERP (Listed under Accounting, Customers, Customer Invoices) and look at the list of draft invoices.  These are the invoices that will be imported into Quickbooks.

2: Launch erp2qb and click on the “Select Invoices” button.   Make sure that the number of invoices selected matches the number you saw that you wanted to import in OpenERP.
You can limit the range of Invoices you will be importing by entering a Partner Name or a Source Document.

3:  Once the Invoices are selected, the right panel will be enabled.  You can now select if you want the Imported items to be listed as “to be printed” in Quickbooks and also if you want to Validate the Invoices in OpenERP to mark them as done.  You will normally want to leave both boxes checked. 

4:  Make sure that Quickbooks is open, and press “Copy Invoices”.  This will start transferring Invoices one at a time into Quickbooks.

How it works
======

erp2qb first needs to find a matching customer in Quickbooks.  It will try to look up a customer by OpenERP’s Customer “Partner Firm”, then “Reference”, and finally “Name”. 
It then needs to match up the product names (listed as Product Reference in OpenERP) with product names in QuickBooks.  These must be identical.
The rest of the fields are imported directly from OpenERP.

If there is an problem, erp2qb will pop up an error message with the invoice number and will say if there was a problem matching the customer name or product name.  The invoice that generated the error, along with all other non-imported invoice, will not be Validated in OpenERP.   After you correct the customer name or product name in OpenERP or QB, you will want to then Select Invoices and Copy Invoices again.
