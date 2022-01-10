using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GemBox.Pdf;
using GemBox.Pdf.Forms;
using GemBox.Pdf.Security;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;

namespace AWSCloudHSM_POC
{

    public partial class Main : System.Windows.Forms.Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult res = fdFile.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtFile.Text = fdFile.FileName;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string documentPath = txtFile.Text;
            if (!File.Exists(documentPath))
            {
                MessageBox.Show($"PDF file does not exist {documentPath}");
                return;
            }
            
            string pkcs11LibraryPath = txt_pkcs.Text;
            //default for SoftHSM
            //string pkcs11LibraryPath = @"C:\SoftHSM2\lib\softhsm2-x64.dll";
            //default for AWS HSM
            //string pkcs11LibraryPath = @"C:\Program Files\Amazon\CloudHSM\lib\cloudhsm_pkcs11.dll";
            if (!File.Exists(pkcs11LibraryPath))
            {
                MessageBox.Show($"PKCS11 file does not exist {pkcs11LibraryPath}");
                return;
            }
            string pin = txtPin.Text;
            if (string.IsNullOrWhiteSpace(pin))
            {
                MessageBox.Show($"You must enter a pin");
                return;
            }

            string certPath = txt_certificate.Text;
            if (!File.Exists(certPath))
            {
                MessageBox.Show($"Certificate file does not exist {certPath}");
                return;
            }

            //gembox code
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            using (var pkcs11Module = new PdfPkcs11Module(pkcs11LibraryPath))
            {
                // Get a token from PKCS#11/Cryptoki device.
                string name = "";
                foreach (var tokeni in pkcs11Module.Tokens)
                {
                    name += name + "\r\ntoken label:" + tokeni.TokenLabel
                        + "\r\ntoken model:" + tokeni.Model
                        + "\r\ntoken manufacturerid:" + tokeni.ModuleManufacturerId
                        + "\r\ntoken serial number:" + tokeni.SerialNumber;
                }
                MessageBox.Show("ModuleInfo\r\n:" +
                    $"Cryptoki Version:{pkcs11Module.CryptokiVersion}\r\n"  +
                    $"Library Version:{pkcs11Module.LibraryVersion}\r\n" +
                    $"Library Path:{pkcs11Module.LibraryPath}\r\n" +
                    $"Description:{pkcs11Module.ModuleDescription}\r\n" +
                    $"ManufacturerId:{pkcs11Module.ModuleManufacturerId}\r\n" +
                    "Token count = " + pkcs11Module.Tokens.Count + $"\r\n Names={name}");

                var token = pkcs11Module.Tokens.First();
                // Login to the token to get access to protected cryptographic functions.
                token.Login(pin);

                // Get a digital ID from PKCS#11/Cryptoki device token.
                var digitalId = token.DigitalIds.First();
                MessageBox.Show($"digitalId info:{digitalId.Id}");

                digitalId.Certificate = new PdfCertificate(certPath);
                using (var document = PdfDocument.Load(documentPath))
                {
                    // Add a visible signature field to the first page of the PDF document.
                    var signatureField = document.Form.Fields.AddSignature(document.Pages[0], 300, 500, 250, 50);

                    // Create a PDF signer that will create the digital signature.
                    var signer = new PdfSigner(digitalId);

                    // Adobe Acrobat Reader currently doesn't download certificate chain
                    // so we will also embed certificate of intermediate Certificate Authority in the signature.
                    // (see https://community.adobe.com/t5/acrobat/signature-validation-using-aia-extension-not-enabled-by-default/td-p/10729647)
                    //var intermediateCA = token.DigitalIds.Single(id => id.Certificate.SubjectCommonName == "GemBoxECDsa").Certificate;
                    //signer.ValidationInfo = new PdfSignatureValidationInfo(new PdfCertificate[] { intermediateCA }, null, null);

                    // Initiate signing of a PDF file with the specified signer.
                    signatureField.Sign(signer);

                    // Finish signing of a PDF file.
                    document.Save("Digital Signature PKCS11.pdf");
                }

                token.Logout();
            }
            return;

            //use aws library to generate a signature for a file
            //try
            //{

            //    Pkcs11InteropFactories factories = new Pkcs11InteropFactories();

            //    using (IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, pkcs11LibraryPath, AppType.MultiThreaded))
            //    {
            //        ISlot slot = Helpers.GetUsableSlot(pkcs11Library);
            //        using (ISession session = slot.OpenSession(SessionType.ReadWrite))
            //        {
            //            session.Login(CKU.CKU_USER, pin);
            //            // Generate key pair - other options can set
            //            /* List<IObjectAttribute> publicTemplate = new List<IObjectAttribute>();
            //               List<IObjectAttribute> privateTemplate = new List<IObjectAttribute>();
            //               publicTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_VERIFY, true));
            //               publicTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_MODULUS_BITS, (ulong)2048));
            //               publicTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_PUBLIC_EXPONENT, new byte[3] { 0x01, 0x00, 0x01 }));
            //               privateTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_SIGN, true));
            //               IMechanism mechanism = session.Factories.MechanismFactory.Create(CKM.CKM_RSA_X9_31_KEY_PAIR_GEN); 
            //               session.GenerateKeyPair(mechanism, publicTemplate, privateTemplate, out publicKey, out privateKey); */
            //            Helpers.GenerateKeyPair(session, out IObjectHandle publicKey, out IObjectHandle privateKey);

            //            // Specify signing mechanism
            //            IMechanism mechanismSign = session.Factories.MechanismFactory.Create(CKM.CKM_SHA1_RSA_PKCS);

            //            byte[] sourceData = File.ReadAllBytes(documentPath);
            //            if (sourceData != null)
            //            {
            //                // Sign data
            //                byte[] signedFile = session.Sign(mechanismSign, privateKey, sourceData);
            //                File.WriteAllBytes(Path.Combine(documentPath,".sign"), sourceData);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error Signing document {documentPath}: " + ex.ToString());
            //}
        }

        private void btn_pkcs_Click(object sender, EventArgs e)
        {
            DialogResult res = fdFile.ShowDialog();
            if (res == DialogResult.OK)
            {
                txt_pkcs.Text = fdFile.FileName;
            }
        }

        private void btn_certificate_Click(object sender, EventArgs e)
        {
            DialogResult res = fdFile.ShowDialog();
            if (res == DialogResult.OK)
            {
                txt_certificate.Text = fdFile.FileName;
            }
        }
    }

    public static class Helpers
    {
        /// <summary>
        /// Finds slot containing the token that matches criteria specified in Settings class
        /// </summary>
        /// <param name='pkcs11Library'>Initialized PKCS11 wrapper</param>
        /// <returns>Slot containing the token that matches criteria</returns>
        public static ISlot GetUsableSlot(IPkcs11Library pkcs11Library)
        {
            // Get list of available slots with token present
            List<ISlot> slots = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent);
            // First slot with token present is OK...
            ISlot matchingSlot = slots[0];
            // ...unless there are matching criteria specified in Settings class
            /*            if (Settings.TokenSerial != null || Settings.TokenLabel != null)
                        {
                            matchingSlot = null;
                            foreach (ISlot slot in slots)
                            {
                                ITokenInfo tokenInfo = null;
                                try
                                {
                                    tokenInfo = slot.GetTokenInfo();
                                }
                                catch (Pkcs11Exception ex)
                                {
                                    if (ex.RV != CKR.CKR_TOKEN_NOT_RECOGNIZED && ex.RV != CKR.CKR_TOKEN_NOT_PRESENT)
                                        throw;
                                }
                                if (tokenInfo == null)
                                    continue;
                                if (!string.IsNullOrEmpty(Settings.TokenSerial))
                                    if (0 != string.Compare(Settings.TokenSerial, tokenInfo.SerialNumber, StringComparison.Ordinal))
                                        continue;
                                if (!string.IsNullOrEmpty(Settings.TokenLabel))
                                    if (0 != string.Compare(Settings.TokenLabel, tokenInfo.Label, StringComparison.Ordinal))
                                        continue;
                                matchingSlot = slot;
                                break;
                            }
                        }
                        Assert.IsTrue(matchingSlot != null, "Token matching criteria specified in Settings class is not present"); */
            return matchingSlot;
        }
        /// <summary>
        /// Creates the data object.
        /// </summary>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <returns>Object handle</returns>
        public static IObjectHandle CreateDataObject(ISession session)
        {
            // Prepare attribute template of new data object
            List<IObjectAttribute> objectAttributes = new List<IObjectAttribute>();
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_DATA));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_APPLICATION, "AWSCloudHSM_POC"));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_LABEL, "AWSCloudHSM_POC"));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_VALUE, "Data object content"));

            // Create object
            return session.CreateObject(objectAttributes);
        }
        /// <summary>
        /// Generates symetric key.
        /// </summary>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <returns>Object handle</returns>
        public static IObjectHandle GenerateKey(ISession session)
        {
            // Prepare attribute template of new key
            List<IObjectAttribute> objectAttributes = new List<IObjectAttribute>();
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_KEY_TYPE, CKK.CKK_DES3));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_ENCRYPT, true));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_DECRYPT, true));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_DERIVE, true));
            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_EXTRACTABLE, true));

            // Specify key generation mechanism
            IMechanism mechanism = session.Factories.MechanismFactory.Create(CKM.CKM_DES3_KEY_GEN);

            // Generate key
            return session.GenerateKey(mechanism, objectAttributes);
        }
        /// <summary>
        /// Generates asymetric key pair.
        /// </summary>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <param name='publicKeyHandle'>Output parameter for public key object handle</param>
        /// <param name='privateKeyHandle'>Output parameter for private key object handle</param>
        public static void GenerateKeyPair(ISession session, out IObjectHandle publicKeyHandle, out IObjectHandle privateKeyHandle)
        {
            // The CKA_ID attribute is intended as a means of distinguishing multiple key pairs held by the same subject
            byte[] ckaId = session.GenerateRandom(20);

            // Prepare attribute template of new public key
            List<IObjectAttribute> publicKeyAttributes = new List<IObjectAttribute>();
            //            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true));
            //            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_PRIVATE, false));
            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_LABEL, "AWSCloudHSM_POC"));
            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_ID, ckaId));
            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_ENCRYPT, true));
            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_VERIFY, true));
            //            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_VERIFY_RECOVER, true));
            //            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_WRAP, true));
            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_MODULUS_BITS, 2048));
            publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_PUBLIC_EXPONENT, new byte[] { 0x01, 0x00, 0x01 }));

            // Prepare attribute template of new private key
            List<IObjectAttribute> privateKeyAttributes = new List<IObjectAttribute>();
            //           privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true));
            //            privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_PRIVATE, true));
            privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_LABEL, "AWSCloudHSM_POC"));
            privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_ID, ckaId));
            //           privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_SENSITIVE, true));
            privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_DECRYPT, true));
            privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_SIGN, true));
            //          privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_SIGN_RECOVER, true));
            //           privateKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_UNWRAP, true));

            // Specify key generation mechanism
            IMechanism mechanism = session.Factories.MechanismFactory.Create(CKM.CKM_RSA_PKCS_KEY_PAIR_GEN);

            // Generate key pair
            session.GenerateKeyPair(mechanism, publicKeyAttributes, privateKeyAttributes, out publicKeyHandle, out privateKeyHandle);
        }
    }
}
