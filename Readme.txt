This is a demo app designed to show how to use an HSM and sign a PDF using a third party tool.  This working example uses GemBox and SoftHSM.

If you do not have an HSM setup, you will need to configure one.  SoftHSM works with this demo.

Included is a private key and certificate (localhost.key and .crt).  You only need to import the key into the HSM.  The certificate can be specified at runtime.

By default, the app will use first token and first digitalId it discovers in the HSM.

When running the app you should get 2 dialog boxes.  The first will show the desription of your HSM and it's tokens.  The second should show the id of the digitalid
that it will use to sign the pdf.

It will output a file called Digital Signature PKCS11.pdf to the current directory.  You can tell it worked by opening the pdf in Acrobat Reader.
At the top you should get a blue line with Signature Panel on the left hand side.  When you click on Signature Panel you should see on the left a list
of the signatures and the certificates.  If you used the key and cert in this example, it should show Signed by L321.local and say the validity is 
unknown because it was a self signed certificate.